using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Security.Cryptography;
using System.IO;
using System.Runtime.InteropServices;

namespace ECE549ProjectFall2014
{
    public partial class FormEC : Form
    {
        string fileName = "Select file";
        FileInfo fInfo;
        byte[] key = null;
        byte[] IV;
        int readerHandle = 0;

        const string encryptFolder = @"C:\Encrypt\"; // Encryption Folder
        const string decryptFolder = @"C:\Decrypt\"; // Decryption folder
                

        public FormEC()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void selectfileToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog oFD = new OpenFileDialog();
            oFD.Title = "Select file";
            if (oFD.ShowDialog() == DialogResult.OK)
            {
                fileName = oFD.FileName;
                if (fileName != null)
                {
                    fInfo = new FileInfo(fileName);
                }
            }
            richTextBoxFileSelected.Text = fileName;
            if (!(fileName.Equals("Select file")))
            {
                richTextBoxFileSelected.BackColor = DefaultBackColor;
            }
        }

        private void FormEC_Load(object sender, EventArgs e)
        {
            SetMyButtonIcon();

            CreateFolders();
        }

        private static void CreateFolders()
        {
            // logic to create the encryption and decryption folder. This folder is created just once
            if (File.Exists(encryptFolder))
            {
                // file already exists
            }
            else
            {
                Directory.CreateDirectory(encryptFolder);
            }

            if (File.Exists(decryptFolder))
            {
                // file already exists
            }
            else
            {
                Directory.CreateDirectory(decryptFolder);
            }
        }

        private void SetMyButtonIcon() // To set the icon in the Get Key button
        {
            buttonGetKey.TextAlign = ContentAlignment.MiddleLeft;
            buttonGetKey.ImageAlign = ContentAlignment.MiddleRight;

            buttonDecrypt.TextAlign = ContentAlignment.MiddleLeft;
            buttonDecrypt.ImageAlign = ContentAlignment.MiddleRight;

            buttonEncrypt.TextAlign = ContentAlignment.MiddleLeft;
            buttonEncrypt.ImageAlign = ContentAlignment.MiddleRight;
        }

        private void buttonGetKey_Click(object sender, EventArgs e)
        {
            //OLD CODE
            //key = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5 }; // Key fixed
            //IV = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5 }; // IV fixed
            //MessageBox.Show("Key retrieved from the \"smart card\"", "Information", MessageBoxButtons.OK);


            // NEW CODE
            IV = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5 };
            readerHandle = 0;

            try
            {
                readerHandle = wpx.CLX_OpenUSBreader(0, wpx.CLX_RTypePCSC, 1);
                byte[] str1 = new byte[16];
                bool retB;
                int retCode;
                retB = selectFile(0x30, 0x5d);      // Linear Record 305d
                if (retB == false)
                {
                    MessageBox.Show("An error occurred.\nFile could not be selected.", "ERROR", MessageBoxButtons.OK);
                    return;
                }
                else
                {
                    MessageBox.Show("Success.\nFile selected!", "GOOD", MessageBoxButtons.OK);
                    //successful. Do nothing
                }
                
                retCode = wpx.CLX_LinearRecordRead(readerHandle, 1, 16, str1);  // Read Record #1 (16 Bytes)
                
                if (retCode == -1) // not successful
                {
                    MessageBox.Show("Error occurred.\nNo key read.", "ERROR", MessageBoxButtons.OK);
                    return;
                }

                else // successful
                {
                    int count = 0;
                    foreach (var item in str1)
                    {
                        if (item == (byte)0xFF)
                        {
                            count++;
                        }
                    }

                    if (count == 16) // key not stored yet
                    {
                        MessageBox.Show("No key found!\nA random key will be generated and stored.",
                            "Information", MessageBoxButtons.OK);

                        //store key on the card
                        for (int i = 0; i < 14; i++)
                        {
                            str1[i] = (byte)0x1F;
                        }
                        for (int i = 14; i < 16; i++)
                        {
                            str1[i] = (byte)0xBB;
                        }
                        retCode = wpx.CLX_LinearRecordUpdate(readerHandle, 1, 16, str1);

                        MessageBox.Show("Cryptographically secure key was stored on the card.\n\nProgram will use this key!",
                            "Information", MessageBoxButtons.OK);

                        key = str1;
                    }

                    else
                    {
                        retCode = wpx.CLX_LinearRecordRead(readerHandle, 1, 16, str1);  // Read Record #1 (16 Bytes)
                        key = str1;
                        MessageBox.Show("Key retrieved from smart card.", "Information", MessageBoxButtons.OK);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error occurred!\nPlease make sure the right card is correctly inserted.",
                    "Information", MessageBoxButtons.OK);
                key = null;
            }
            wpx.CLX_CloseReader(readerHandle);
        }

        private void buttonEncrypt_Click(object sender, EventArgs e)
        {
            CreateFolders();
            if (key == null)
            {
                MessageBox.Show("Key not set.");
            }
            else
            {
                if (fileName.Equals("Select file"))
                {
                    MessageBox.Show("You have to select a file!");
                    richTextBoxFileSelected.BackColor = Color.Red;
                }
                else
                {
                    try
                    {
                        EncryptFile(fInfo.FullName);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("An error occurred!\nPlease select a correct file.");
                        string f = fInfo.Name;
                        int startFileName = f.LastIndexOf("\\") + 1;
                        string n = encryptFolder +
                            f.Substring(startFileName, f.LastIndexOf(".") - startFileName) + ".enc";
                        File.Delete(n);
                    }
                }
            }
        }

        private void EncryptFile(string inFile) // function to encrypt the file
        {
            string s = inFile.Substring(inFile.LastIndexOf(".") + 1, inFile.Length - 1 - inFile.LastIndexOf("."));
            byte[] sInBytes = Encoding.Default.GetBytes(s);
            byte[] encryptedSInBytes = new byte[3]; int i = 0; int j = 2;
            foreach (byte b in sInBytes)
            {
                encryptedSInBytes[i] = (byte) (b + 17 + j);
                i++; j--;
            }

            if (s.Equals("enc"))
            {
                MessageBox.Show("You can not encrypt more than once.");
                return;
            }

            int startFileName = inFile.LastIndexOf("\\") + 1; // good
            // Change the file's extension to ".enc"
            string outFile = encryptFolder +
                inFile.Substring(startFileName, inFile.LastIndexOf(".") - startFileName) + ".enc";

            if (File.Exists(outFile))
            {
                MessageBox.Show("This file is already encrypted!");
            }

            else
            {
                // Create instance of Rijndael for symmetric encryption of data
                RijndaelManaged rd = new RijndaelManaged();
                rd.Key = key;
                rd.Mode = CipherMode.CBC;
                rd.IV = IV;
                ICryptoTransform transform = rd.CreateEncryptor(key, IV);

                using (FileStream outFileStream = new FileStream(outFile, FileMode.Create))
                {
                    outFileStream.Write(encryptedSInBytes, 0, sInBytes.Length); // good

                    using (CryptoStream outStreamEncrypted = new CryptoStream(outFileStream, transform,
                        CryptoStreamMode.Write))
                    {
                        // Encrypt a chunk at a time
                        int count = 0;
                        int offset = 0;

                        // blockSizeBytes can be any arbitrary size
                        int blockSizeBytes = rd.BlockSize / 8;
                        byte[] data = new byte[blockSizeBytes];
                        int bytesRead = 0;

                        using (FileStream inFileStream = new FileStream(inFile, FileMode.Open))
                        {
                            do
                            {
                                count = inFileStream.Read(data, 0, blockSizeBytes);
                                offset += count;
                                outStreamEncrypted.Write(data, 0, count);
                                bytesRead += blockSizeBytes;
                            } while (count > 0);
                            inFileStream.Close();
                        }
                        outStreamEncrypted.FlushFinalBlock();
                        outStreamEncrypted.Close();
                    }
                    outFileStream.Close();
                }
                MessageBox.Show("File Encrypted!", "Information", MessageBoxButtons.OK);
            }
        }

        private void buttonDecrypt_Click(object sender, EventArgs e)
        {
            CreateFolders();
            if (key == null)
            {
                MessageBox.Show("Key not set.");
            }
            else
            {
                if (fileName.Equals("Select file"))
                {
                    MessageBox.Show("You have to select a file!");
                    richTextBoxFileSelected.BackColor = Color.Red;
                }
                else
                {
                    try
                    {
                        DecryptFile(fInfo.FullName);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("An error occurred!\nPlease select a correct file.");
                    }
                    
                }
            }
        }

        private void DecryptFile(string inFile) // function to decrypt the file
        {
            string s = inFile.Substring(inFile.LastIndexOf(".") + 1, inFile.Length - 1 - inFile.LastIndexOf("."));
            byte[] fileExtension = new byte[3];
            byte[] decryptedFileExtension = new byte[3];
            string fileExtensionString = "";

            if (!(s.Equals("enc")))
            {
                MessageBox.Show("You can not decrypt more than once.\nThis is a plain file.");
                return;
            }

            // Create instance of Rijndael
            RijndaelManaged rd = new RijndaelManaged();
            rd.Key = key;
            rd.IV = IV;
            rd.Mode = CipherMode.CBC;

            using (FileStream inFileStream = new FileStream(inFile, FileMode.Open))
            {
                ICryptoTransform transform = rd.CreateDecryptor(key, IV);

                inFileStream.Seek(0, SeekOrigin.Begin);
                inFileStream.Read(fileExtension, 0, 3);
                int i = 0; int j = 2;
                foreach (byte b in fileExtension)
                {
                    decryptedFileExtension[i] = (byte) (b - 17 - j);
                    i++; j--;
                }
                fileExtensionString = Encoding.Default.GetString(decryptedFileExtension, 0, fileExtension.Length);

                int startFileName = inFile.LastIndexOf("\\") + 1; // good
                // Change the file extension to its original extension
                string outFile = decryptFolder +
                    inFile.Substring(startFileName, inFile.LastIndexOf(".") - startFileName) + "." + fileExtensionString;

                if (File.Exists(outFile))
                {
                    MessageBox.Show("This file is already decrypted!");
                    return;
                }

                using (FileStream outFileStream = new FileStream(outFile, FileMode.Create))
                {
                    int count = 0;
                    int offset = 0;

                    int blockSizeBytes = rd.BlockSize / 8;
                    byte[] data = new byte[blockSizeBytes];

                    inFileStream.Seek(3, SeekOrigin.Begin);

                    using (CryptoStream outStreamDecrypted = new CryptoStream(outFileStream,
                        transform, CryptoStreamMode.Write))
                    {
                        do
                        {
                            count = inFileStream.Read(data, 0, blockSizeBytes);
                            offset += count;
                            outStreamDecrypted.Write(data, 0, count);

                        } while (count > 0);

                        outStreamDecrypted.FlushFinalBlock();
                        outStreamDecrypted.Close();
                    }
                    outFileStream.Close();
                }
                inFileStream.Close();
            }
            MessageBox.Show("File Decrypted!", "Information", MessageBoxButtons.OK);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAbout formAbout = new FormAbout();
            formAbout.Show();
        }

        private void howToUseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormHowToUse formHowToUse = new FormHowToUse();
            formHowToUse.Show();
        }

        private bool selectFile(byte b1, byte b2)
        {
            int retCode;
            string st;
            byte[] str1 = { b1, b2 };

            st = b1.ToString("X2");
            st += b2.ToString("X2");

            retCode = wpx.CLX_Select7816(readerHandle, 2, str1); // select LINEAR - 3040
            if (retCode == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
