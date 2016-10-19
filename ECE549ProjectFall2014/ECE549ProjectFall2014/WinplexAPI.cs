using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;


namespace ECE549ProjectFall2014
{
    sealed class wpx
    {
        // ********************************************************************
        // **                                                                **
        // **   WinPlex API Version 2.0 VB Defs                              **
        // **                                                                **
        // ********************************************************************

        //  Reader Types
        // --------------------------------------------------------------------
        public const int CLX_RTypeAxiohm = 1;
        public const int CLX_RTypeST100 = 2;
        public const int CLX_RTypeST110 = 3;
        public const int CLX_RTypeKDM9902 = 4;
        public const int CLX_RTypeKDM9680 = 5;
        public const int CLX_RTypeTowitoko = 6;
        public const int CLX_RTypeSCM = 7;
        public const int CLX_RTypeInnovonics_SC = 8;
        public const int CLX_RTypeInnovonics_MAG = 9;
        public const int CLX_RTypePCSC = 10;
        public const int CLX_RTypePreciseBiometrics = 11;
        public const int CLX_RTypeIDTech = 12;
        public const int CLX_RTypeIDTech2 = 13;
        public const int CLX_RTypeOMNIKey = 14;
        public const int CLX_RTypePrecise = 15;
        public const int CLX_RTypeMagtek = 16;

        //  Card Types
        // --------------------------------------------------------------------
        public const int CLX_CType_CPU = 0;
        public const int CLX_CType_B1 = 1;
        public const int CLX_CType_B2 = 2;
        public const int CLX_CType_B6 = 3;
        public const int CLX_CType_A1 = 4;
        public const int CLX_CType_K1 = 5;
        public const int CLX_CType_A2 = 6;
        public const int CLX_CType_E1 = 7;
        // public const int CLX_CType_D1 As Long = 8  //  Obsolete
        public const int CLX_CType_B5 = 9;
        public const int CLX_CType_A6 = 10;
        public const int CLX_CType_D2 = 11;
        public const int CLX_CType_E2 = 12;
        public const int CLX_CType_A7 = 13;
        public const int CLX_CType_B3 = 14;
        public const int CLX_CType_B4 = 15;
        public const int CLX_CType_E3 = 16;
        public const int CLX_CType_A8 = 17;
        public const int CLX_CType_J7 = 18;
        public const int CLX_CType_A9 = 19;
        public const int CLX_CType_A3 = 20;
        public const int CLX_CType_E4 = 21;
        public const int CLX_CType_E6 = 22;
        public const int CLX_CType_A5 = 23;
        public const int CLX_CType_J6 = 24;
        public const int CLX_CType_K2 = 25;
        public const int CLX_CType_A4 = 26;
        public const int CLX_CType_MD1 = 27;
        public const int CLX_CType_K4 = 28;
        public const int CLX_CType_D5 = 29;


        [DllImport("WinPlex.dll", EntryPoint = "CLX_APIVersion")]
        public static extern int CLX_APIVersion(byte[] vdata);

        [DllImport("WinPlex.dll", EntryPoint = "CLX_OpenReader")]
        public static extern int CLX_OpenReader(uint portNum, uint readerType, uint baud);

        [DllImport("WinPlex.dll", EntryPoint = "CLX_OpenUSBreader")]
        public static extern int CLX_OpenUSBreader(uint vport, uint readerType, uint cardMode);

        [DllImport("WinPlex.dll", EntryPoint = "CLX_CloseReader")]
        public static extern int CLX_CloseReader(int rhandle);

        [DllImport("WinPlex.dll", EntryPoint = "CLX_ResetReader")]
        public static extern int CLX_ResetReader(int rhandle);

        // DLLEXPORT CLX_GetReaderName(long ReaderIndex, UCHAR *ReaderName, long NameSize)
        [DllImport("WinPlex.dll", EntryPoint = "CLX_GetReaderName")]
        public static extern int CLX_GetReaderName(int rhandle, int ReaderIndex, byte[] ReaderName, ref long NameSize);

        [DllImport("WinPlex.dll", EntryPoint = "CLX_SetCardType")]
        public static extern int CLX_SetCardType(int rhandle, uint ctype);

        [DllImport("WinPlex.dll", EntryPoint = "CLX_CardInserted")]
        public static extern int CLX_CardInserted(int rhandle);

        [DllImport("WinPlex.dll", EntryPoint = "CLX_CardOn")]
        public static extern int CLX_CardOn(int rhandle, byte[] vdata, ref uint len);

        [DllImport("WinPlex.dll", EntryPoint = "CLX_CardOff")]
        public static extern int CLX_CardOff(int rhandle);

        [DllImport("WinPlex.dll", EntryPoint = "CLX_Select7816")]
        public static extern int CLX_Select7816(int rhandle, uint p3, byte[] DataOut);

        [DllImport("WinPlex.dll", EntryPoint = "CLX_UpdateBinary7816")]
        public static extern int CLX_UpdateBinary7816(int rhandle, uint p3, uint num, byte[] dataOut);

        [DllImport("WinPlex.dll", EntryPoint = "CLX_ReadBinary7816")]
        public static extern int CLX_ReadBinary7816(int rhandle, uint p3, uint num, byte[] dataIn);

        [DllImport("WinPlex.dll", EntryPoint = "CLX_WriteBinary7816")]
        public static extern int CLX_WriteBinary7816(int rhandle, uint p3, uint num, byte[] dataOut);

        [DllImport("WinPlex.dll", EntryPoint = "CLX_GetChallenge7816")]
        public static extern int CLX_GetChallenge7816(int rhandle, uint num, byte[] dataIn);

        [DllImport("WinPlex.dll", EntryPoint = "CLX_Sha1")]
        public static extern int CLX_Sha1(byte[] dataOut, byte[] pwd, byte[] sha);

        [DllImport("WinPlex.dll", EntryPoint = "CLX_ExternalAuth7816Ex")]
        public static extern int CLX_ExternalAuth7816Ex(int rhandle, uint tokenID, byte[] dataOut);

        [DllImport("WinPlex.dll", EntryPoint = "CLX_InternalAuth7816")]
        public static extern int CLX_InternalAuth7816(int rhandle, byte[] dataOut);

        [DllImport("WinPlex.dll", EntryPoint = "CLX_Invalidate7816")]
        public static extern int CLX_Invalidate7816(int rhandle);

        [DllImport("WinPlex.dll", EntryPoint = "CLX_Rehab7816")]
        public static extern int CLX_Rehab7816(int rhandle);

        [DllImport("WinPlex.dll", EntryPoint = "CLX_GetResponse7816")]
        public static extern int CLX_GetResponse7816(int rhandle, byte[] dataIn);

        [DllImport("WinPlex.dll", EntryPoint = "CLX_GetError")]
        public static extern int CLX_GetError(int rhandle);

        [DllImport("WinPlex.dll", EntryPoint = "CLX_GenerateKey")]
        public static extern int CLX_GenerateKey(int rhandle, uint Secret, int keyreg, byte[] sk, byte[] challenge, byte[] session);

        [DllImport("WinPlex.dll", EntryPoint = "CLX_DesEncrypt")]
        public static extern int CLX_DesEncrypt(byte[] DataBlock, byte[] SessionKey, uint datalen);

        [DllImport("WinPlex.dll", EntryPoint = "CLX_3DesEncrypt")]
        public static extern int CLX_3DesEncrypt(byte[] DataBlock, byte[] SessionKey, uint datalen);

        [DllImport("WinPlex.dll", EntryPoint = "CLX_ReadSecure")]
        public static extern int CLX_ReadSecure(int rhandle, uint Offset, uint leng, byte[] dataIN);

        [DllImport("WinPlex.dll", EntryPoint = "CLX_SetEncryption")]
        public static extern int CLX_SetEncryption(int rhandle, uint Algorithm, uint keyreg);

        [DllImport("WinPlex.dll", EntryPoint = "CLX_DesDecrypt")]
        public static extern int CLX_DesDecrypt(byte[] DataBlock, byte[] SessionKey, uint datalen);

        [DllImport("WinPlex.dll", EntryPoint = "CLX_3DesDecrypt")]
        public static extern int CLX_3DesDecrypt(byte[] DataBlock, byte[] SessionKey, uint datalen);

        [DllImport("WinPlex.dll", EntryPoint = "CLX_UpdateSecure")]
        public static extern int CLX_UpdateSecure(int rhandle, uint Offset, uint leng, byte[] dataOut);

        [DllImport("WinPlex.dll", EntryPoint = "CLX_Verify7816Ex")]
        public static extern int CLX_Verify7816Ex(int rhandle, uint tokenID, uint leng, byte[] dataOut);

        // Memory Card Calls
        [DllImport("WinPlex.dll", EntryPoint = "CLX_ReadBinary7816")]
        public static extern int CLX_ReadCard(int rhandle, byte[] dataIn, uint DataAddr, uint datalen);

        [DllImport("WinPlex.dll", EntryPoint = "CLX_ReadBinary7816")]
        public static extern int CLX_WriteCard(int rhandle, byte[] dataOut, uint DataAddr, uint datalen);

        // Purse functions
        [DllImport("WinPlex.dll", EntryPoint = "CLX_PurseInit")]
        public static extern int CLX_PurseInit(int rhandle, byte[] chal);

        [DllImport("WinPlex.dll", EntryPoint = "CLX_PurseVal_Secure")]
        public static extern int CLX_PurseVal_Secure(int rhandle, byte[] chal, byte[] session, ref uint pval);

        [DllImport("WinPlex.dll", EntryPoint = "CLX_PurseDep_Secure")]
        public static extern int CLX_PurseDep_Secure(int rhandle, byte[] chal, byte[] session, uint deposit);

        [DllImport("WinPlex.dll", EntryPoint = "CLX_PurseWdl_Secure")]
        public static extern int CLX_PurseWdl_Secure(int rhandle, byte[] chal, byte[] session, uint withdrawal);

        [DllImport("WinPlex.dll", EntryPoint = "CLX_PurseVal")]
        public static extern int CLX_PurseVal(int rhandle, ref uint pval);

        [DllImport("WinPlex.dll", EntryPoint = "CLX_PurseDep")]
        public static extern int CLX_PurseDep(int rhandle, uint deposit);

        [DllImport("WinPlex.dll", EntryPoint = "CLX_PurseWdl")]
        public static extern int CLX_PurseWdl(int rhandle, uint wdl);

        //////////////////////////// Linear / Cyclic Record Functions

        [DllImport("WinPlex.dll", EntryPoint = "CLX_CyclicRecordReadAbsolute")]
        public static extern int CLX_CyclicRecordReadAbsolute(int portHandle, byte recordNumber, byte recordSize, byte[] returnBuffer);
        [DllImport("WinPlex.dll", EntryPoint = "CLX_CyclicRecordReadCurrent")]
        public static extern int CLX_CyclicRecordReadCurrent(int portHandle, byte recordSize, byte[] returnBuffer);
        [DllImport("WinPlex.dll", EntryPoint = "CLX_CyclicRecordReadPrevious")]
        public static extern int CLX_CyclicRecordReadPrevious(int portHandle, byte recordSize, byte[] returnBuffer);
        [DllImport("WinPlex.dll", EntryPoint = "CLX_CyclicRecordReadNext")]
        public static extern int CLX_CyclicRecordReadNext(int portHandle, byte recordSize, byte[] returnBuffer);
        [DllImport("WinPlex.dll", EntryPoint = "CLX_CyclicRecordUpdateCurrent")]
        public static extern int CLX_CyclicRecordUpdateCurrent(int portHandle, byte recordSize, byte[] recordBuffer);
        [DllImport("WinPlex.dll", EntryPoint = "CLX_CyclicRecordUpdateNext")]
        public static extern int CLX_CyclicRecordUpdateNext(int portHandle, byte recordSize, byte[] recordBuffer);
        [DllImport("WinPlex.dll", EntryPoint = "CLX_CyclicRecordUpdatePrevious")]
        public static extern int CLX_CyclicRecordUpdatePrevious(int portHandle, byte recordSize, byte[] recordBuffer);
        [DllImport("WinPlex.dll", EntryPoint = "CLX_CyclicRecordUpdateAbsolute")]
        public static extern int CLX_CyclicRecordUpdateAbsolute(int portHandle, byte recordNumber, byte recordSize, byte[] recordBuffer);
        [DllImport("WinPlex.dll", EntryPoint = "CLX_CyclicRecordWriteCurrent")]
        public static extern int CLX_CyclicRecordWriteCurrent(int portHandle, byte recordSize, byte[] recordBuffer);
        [DllImport("WinPlex.dll", EntryPoint = "CLX_CyclicRecordWriteNext")]
        public static extern int CLX_CyclicRecordWriteNext(int portHandle, byte recordSize, byte[] recordBuffer);
        [DllImport("WinPlex.dll", EntryPoint = "CLX_CyclicRecordWritePrevious")]
        public static extern int CLX_CyclicRecordWritePrevious(int portHandle, byte recordSize, byte[] recordBuffer);
        [DllImport("WinPlex.dll", EntryPoint = "CLX_CyclicRecordWriteAbsolute")]
        public static extern int CLX_CyclicRecordWriteAbsolute(int portHandle, byte recordNumber, byte recordSize, byte[] recordBuffer);

        // SECURE Cyclic Functions
        [DllImport("WinPlex.dll", EntryPoint = "CLX_CyclicRecordReadAbsoluteSecure")]
        public static extern int CLX_CyclicRecordReadAbsoluteSecure(int portHandle, byte recordNumber, byte recordSize, byte[] returnBuffer);
        [DllImport("WinPlex.dll", EntryPoint = "CLX_CyclicRecordReadCurrentSecure")]
        public static extern int CLX_CyclicRecordReadCurrentSecure(int portHandle, byte recordSize, byte[] returnBuffer);
        [DllImport("WinPlex.dll", EntryPoint = "CLX_CyclicRecordReadPreviousSecure")]
        public static extern int CLX_CyclicRecordReadPreviousSecure(int portHandle, byte recordSize, byte[] returnBuffer);
        [DllImport("WinPlex.dll", EntryPoint = "CLX_CyclicRecordReadNextSecure")]
        public static extern int CLX_CyclicRecordReadNextSecure(int portHandle, byte recordSize, byte[] returnBuffer);
        [DllImport("WinPlex.dll", EntryPoint = "CLX_CyclicRecordUpdateCurrentSecure")]
        public static extern int CLX_CyclicRecordUpdateCurrentSecure(int portHandle, byte recordSize, byte[] recordBuffer);
        [DllImport("WinPlex.dll", EntryPoint = "CLX_CyclicRecordUpdateNextSecure")]
        public static extern int CLX_CyclicRecordUpdateNextSecure(int portHandle, byte recordSize, byte[] recordBuffer);
        [DllImport("WinPlex.dll", EntryPoint = "CLX_CyclicRecordUpdatePreviousSecure")]
        public static extern int CLX_CyclicRecordUpdatePreviousSecure(int portHandle, byte recordSize, byte[] recordBuffer);
        [DllImport("WinPlex.dll", EntryPoint = "CLX_CyclicRecordUpdateAbsoluteSecure")]
        public static extern int CLX_CyclicRecordUpdateAbsoluteSecure(int portHandle, byte recordNumber, byte recordSize, byte[] recordBuffer);
        [DllImport("WinPlex.dll", EntryPoint = "CLX_CyclicRecordWriteCurrentSecure")]
        public static extern int CLX_CyclicRecordWriteCurrentSecure(int portHandle, byte recordSize, byte[] recordBuffer);
        [DllImport("WinPlex.dll", EntryPoint = "CLX_CyclicRecordWriteNextSecure")]
        public static extern int CLX_CyclicRecordWriteNextSecure(int portHandle, byte recordSize, byte[] recordBuffer);
        [DllImport("WinPlex.dll", EntryPoint = "CLX_CyclicRecordWritePreviousSecure")]
        public static extern int CLX_CyclicRecordWritePreviousSecure(int portHandle, byte recordSize, byte[] recordBuffer);
        [DllImport("WinPlex.dll", EntryPoint = "CLX_CyclicRecordWriteAbsoluteSecure")]
        public static extern int CLX_CyclicRecordWriteAbsoluteSecure(int portHandle, byte recordNumber, byte recordSize, byte[] recordBuffer);

        // LINEAR Functions

        [DllImport("WinPlex.dll", EntryPoint = "CLX_LinearRecordRead")]
        public static extern int CLX_LinearRecordRead(int portHandle, byte recordNumber, byte recordSize, byte[] returnBuffer);
        [DllImport("WinPlex.dll", EntryPoint = "CLX_LinearRecordUpdate")]
        public static extern int CLX_LinearRecordUpdate(int portHandle, byte recordNumber, byte recordSize, byte[] recordBuffer);
        [DllImport("WinPlex.dll", EntryPoint = "CLX_LinearRecordWrite")]
        public static extern int CLX_LinearRecordWrite(int portHandle, byte recordNumber, byte recordSize, byte[] recordBuffer);

        // Other Functions
        [DllImport("WinPlex.dll", EntryPoint = "CLX_GetPublicInfo")]
        public static extern int CLX_GetPublicInfo(int portHandle, byte recordNumber, byte[] returnBuffer, ref long returnLength);
    }
}
