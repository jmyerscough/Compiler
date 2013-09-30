using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    /// <summary>
    /// Builds the machine code values for the P16F84 Byte operations.
    /// 
    /// Author: Jason Myerscough
    /// EMail: JMyerscough1@uclan.ac.uk
    /// </summary>
    public class CP16F84ByteOperations : CByteOrientedOperations 
    {
        /// <summary>
        /// Add Working Register to File Register
        /// </summary>
        /// <param name="Destination">Set to 1 to store the results in the working register or 0 to
        /// store the result in the file register.</param>
        /// <param name="FlagAddress">Address of File register. The address has to be in the range of
        /// 0 ... 127 </param>
        /// <returns>Machine code value of instruction</returns>
        public override UInt16 AddWF(TRegisters Destination, Byte FlagAddress)
        {
            UInt16 IntelFormat = 0x00;

            if (IsAddressValid(FlagAddress) == true)
            {
                IntelFormat = (UInt16)Destination;
                IntelFormat <<= 7;
                IntelFormat |= FlagAddress;
                IntelFormat <<= 8;
                IntelFormat |= ADDWF >> 8;
            }
            return IntelFormat;
        }

        /// <summary>
        /// And the working register and file register.
        /// </summary>
        /// <param name="Destination">Set to 1 to store the results in the working register or 0 to
        /// store the result in the file register.</param>
        /// <param name="FlagAddress">Address of File register. The address has to be in the range of
        /// 0 ... 127 </param>
        /// <returns>Machine code value of instruction</returns>
        public override UInt16 AndWF(TRegisters Destination, Byte FlagAddress)
        {
            UInt16 IntelFormat = 0x00;

            if (IsAddressValid(FlagAddress) == true)
            {
                IntelFormat = (UInt16)Destination;
                IntelFormat <<= 7;
                IntelFormat |= FlagAddress;
                IntelFormat <<= 8;
                IntelFormat |= ANDWF >> 8;
            }
            return IntelFormat;
        }

        /// <summary>
        /// Clears the File register
        /// </summary>
        /// <param name="FlagAddress">Address of File register. The address has to be in the range of
        /// 0 ... 127 </param>
        /// <returns>Machine code value of instruction</returns>
        public override UInt16 ClrF(Byte FlagAddress)
        {
            UInt16 TempOpCode = 0x00;
            UInt16 IntelFormat = 0x00;

            if (IsAddressValid(FlagAddress) == true)
            {
                TempOpCode = CLRF;
                TempOpCode |= FlagAddress;

                IntelFormat |= (UInt16)(TempOpCode & 0xFF00);
                IntelFormat |= (UInt16)(TempOpCode & 0x00FF);
            }
            return IntelFormat;
        }

        /// <summary>
        /// Clear the working register
        /// </summary>
        /// <returns>Machine code value of instruction</returns>
        public override UInt16 ClrW()
        {
            return CLRW;
        }

        /// <summary>
        /// Complements the File register.
        /// </summary>
        /// <param name="Destination">Set to 1 to store the results in the working register or 0 to
        /// store the result in the file register.</param>
        /// <param name="FlagAddress">Address of File register. The address has to be in the range of
        /// 0 ... 127</param>
        /// <returns>Machine code value of instruction</returns>
        public override UInt16 ComF(TRegisters Destination, Byte FlagAddress)
        {
            UInt16 IntelFormat = 0x00;

            if (IsAddressValid(FlagAddress) == true)
            {
                IntelFormat = (UInt16)Destination;
                IntelFormat <<= 7;
                IntelFormat |= FlagAddress;
                IntelFormat <<= 8;
                IntelFormat |= COMF >> 8;
            }
            return IntelFormat;
        }

        /// <summary>
        /// Decrements the file register
        /// </summary>
        /// <param name="Destination">Set to 1 to store the results in the working register or 0 to
        /// store the result in the file register.</param>
        /// <param name="FlagAddress">Address of File register. The address has to be in the range of
        /// 0 ... 127</param>
        /// <returns>Machine code value of instruction</returns>
        public override UInt16 DecF(TRegisters Destination, Byte FlagAddress)
        {
            UInt16 IntelFormat = 0x00;

            if (IsAddressValid(FlagAddress) == true)
            {
                IntelFormat = (UInt16)Destination;
                IntelFormat <<= 7;
                IntelFormat |= FlagAddress;
                IntelFormat <<= 8;
                IntelFormat |= DECF >> 8;
            }
            return IntelFormat;
        }

        /// <summary>
        /// Decrements the file register if it is != 0
        /// </summary>
        /// <param name="Destination">Set to 1 to store the results in the working register or 0 to
        /// store the result in the file register.</param>
        /// <param name="FlagAddress">Address of File register. The address has to be in the range of
        /// 0 ... 127</param>
        /// <returns>Machine code value of instruction</returns>
        public override UInt16 DecFSZ(TRegisters Destination, Byte FlagAddress)
        {
            UInt16 IntelFormat = 0x00;

            if (IsAddressValid(FlagAddress) == true)
            {
                IntelFormat = (UInt16)Destination;
                IntelFormat <<= 7;
                IntelFormat |= FlagAddress;
                IntelFormat <<= 8;
                IntelFormat |= DECFSZ >> 8;
            }
            return IntelFormat;
        }

        /// <summary>
        /// Increment the File register
        /// </summary>
        /// <param name="Destination">Set to 1 to store the results in the working register or 0 to
        /// store the result in the file register.</param>
        /// <param name="FlagAddress">Address of File register. The address has to be in the range of
        /// 0 ... 127</param>
        /// <returns>Machine code value of instruction</returns>
        public override UInt16 IncF(TRegisters Destination, Byte FlagAddress)
        {
            UInt16 IntelFormat = 0x00;

            if (IsAddressValid(FlagAddress) == true)
            {
                IntelFormat = (UInt16)Destination;
                IntelFormat <<= 7;
                IntelFormat |= FlagAddress;
                IntelFormat <<= 8;
                IntelFormat |= INCF >> 8;
            }
            return IntelFormat;
        }

        /// <summary>
        /// Increment the file register if != 0
        /// </summary>
        /// <param name="Destination">Set to 1 to store the results in the working register or 0 to
        /// store the result in the file register.</param>
        /// <param name="FlagAddress">Address of File register. The address has to be in the range of
        /// 0 ... 127</param>
        /// <returns>Machine code value of instruction</returns>
        public override UInt16 IncFSZ(TRegisters Destination, Byte FlagAddress)
        {
            UInt16 IntelFormat = 0x00;

            if (IsAddressValid(FlagAddress) == true)
            {
                IntelFormat = (UInt16)Destination;
                IntelFormat <<= 7;
                IntelFormat |= FlagAddress;
                IntelFormat <<= 8;
                IntelFormat |= INCFSZ >> 8;
            }
            return IntelFormat;
        }

        /// <summary>
        /// Inclusive OR working register with the File register
        /// </summary>
        /// <param name="Destination">Set to 1 to store the results in the working register or 0 to
        /// store the result in the file register.</param>
        /// <param name="FlagAddress">Address of File register. The address has to be in the range of
        /// 0 ... 127</param>
        /// <returns>Machine code value of instruction</returns>
        public override UInt16 IorWF(TRegisters Destination, Byte FlagAddress)
        {
            UInt16 IntelFormat = 0x00;

            if (IsAddressValid(FlagAddress) == true)
            {
                IntelFormat = (UInt16)Destination;
                IntelFormat <<= 7;
                IntelFormat |= FlagAddress;
                IntelFormat <<= 8;
                IntelFormat |= IORWF >> 8;
            }
            return IntelFormat;
        }

        /// <summary>
        /// Move the file register
        /// </summary>
        /// <param name="Destination">Set to 1 to store the results in the working register or 0 to
        /// store the result in the file register.</param>
        /// <param name="FlagAddress">Address of File register. The address has to be in the range of
        /// 0 ... 127</param>
        /// <returns>Machine code value of instruction</returns>
        public override UInt16 MovF(TRegisters Destination, Byte FlagAddress)
        {
            UInt16 IntelFormat = 0x00;

            if (IsAddressValid(FlagAddress) == true)
            {
                IntelFormat = (UInt16)Destination;
                IntelFormat <<= 7;
                IntelFormat |= FlagAddress;
                IntelFormat <<= 8;
                IntelFormat |= MOVF >> 8;
            }
            return IntelFormat;
        }

        /// <summary>
        /// Move the working register to the file register
        /// </summary>
        /// <param name="FlagAddress">Address of File register. The address has to be in the range of
        /// 0 ... 127</param>
        /// <returns>Machine code value of instruction</returns>
        public override UInt16 MovWF(Byte FlagAddress)
        {
            UInt16 IntelFormat = 0x00;

            if (IsAddressValid(FlagAddress) == true)
            {
                IntelFormat = MOVWF;
                IntelFormat |= FlagAddress;

                IntelFormat <<= 8;
            }
            return IntelFormat;
        }

        /// <summary>
        /// No operation.
        /// </summary>
        /// <returns>Machine code value of instruction</returns>
        public override UInt16 Nop()
        {
            return 0x00;
        }

        /// <summary>
        /// Rotate the file register left through the carry flag.
        /// </summary>
        /// <param name="Destination">Set to 1 to store the results in the working register or 0 to
        /// store the result in the file register.</param>
        /// <param name="FlagAddress">Address of File register. The address has to be in the range of
        /// 0 ... 127</param>
        /// <returns>Machine code value of instruction</returns>
        public override UInt16 RlF(TRegisters Destination, Byte FlagAddress)
        {
            UInt16 IntelFormat = 0x00;

            if (IsAddressValid(FlagAddress) == true)
            {
                IntelFormat = (UInt16)Destination;
                IntelFormat <<= 7;
                IntelFormat |= FlagAddress;
                IntelFormat <<= 8;
                IntelFormat |= RLF >> 8;
            }
            return IntelFormat;
        }

        /// <summary>
        /// Rotate the file register right through the carry flag.
        /// </summary>
        /// <param name="Destination">Set to 1 to store the results in the working register or 0 to
        /// store the result in the file register.</param>
        /// <param name="FlagAddress">Address of File register. The address has to be in the range of
        /// 0 ... 127</param>
        /// <returns>Machine code value of instruction</returns>
        public override UInt16 RrF(TRegisters Destination, Byte FlagAddress)
        {
            UInt16 IntelFormat = 0x00;

            if (IsAddressValid(FlagAddress) == true)
            {
                IntelFormat = (UInt16)Destination;
                IntelFormat <<= 7;
                IntelFormat |= FlagAddress;
                IntelFormat <<= 8;
                IntelFormat |= RRF >> 8;
            }
            return IntelFormat;
        }

        /// <summary>
        /// Subtract the Working Register from File Register
        /// </summary>
        /// <param name="Destination">Set to 1 to store the results in the working register or 0 to
        /// store the result in the file register.</param>
        /// <param name="FlagAddress">Address of File register. The address has to be in the range of
        /// 0 ... 127</param>
        /// <returns>Machine code value of instruction</returns>
        public override UInt16 SubWF(TRegisters Destination, Byte FlagAddress)
        {
            UInt16 IntelFormat = 0x00;

            if (IsAddressValid(FlagAddress) == true)
            {
                IntelFormat = (UInt16)Destination;
                IntelFormat <<= 7;
                IntelFormat |= FlagAddress;
                IntelFormat <<= 8;
                IntelFormat |= SUBWF >> 8;
            }
            return IntelFormat;
        }

        /// <summary>
        /// Swap the nibbles in the File Register
        /// </summary>
        /// <param name="Destination">Set to 1 to store the results in the working register or 0 to
        /// store the result in the file register.</param>
        /// <param name="FlagAddress">Address of File register. The address has to be in the range of
        /// 0 ... 127</param>
        /// <returns>Machine code value of instruction</returns>
        public override UInt16 SwapWF(TRegisters Destination, Byte FlagAddress)
        {
            UInt16 IntelFormat = 0x00;

            if (IsAddressValid(FlagAddress) == true)
            {
                IntelFormat = (UInt16)Destination;
                IntelFormat <<= 7;
                IntelFormat |= FlagAddress;
                IntelFormat <<= 8;
                IntelFormat |= SWAPWF >> 8;
            }
            return IntelFormat;
        }

        /// <summary>
        /// XOR the working register with the file register.
        /// </summary>
        /// <param name="Destination">Set to 1 to store the results in the working register or 0 to
        /// store the result in the file register.</param>
        /// <param name="FlagAddress">Address of File register. The address has to be in the range of
        /// 0 ... 127</param>
        /// <returns>Machine code value of instruction</returns>
        public override UInt16 XorWF(TRegisters Destination, Byte FlagAddress)
        {
            UInt16 IntelFormat = 0x00;

            if (IsAddressValid(FlagAddress) == true)
            {
                IntelFormat = (UInt16)Destination;
                IntelFormat <<= 7;
                IntelFormat |= FlagAddress;
                IntelFormat <<= 8;
                IntelFormat |= XORWF >> 8;
            }
            return IntelFormat;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Address"></param>
        /// <returns>Machine code value of instruction</returns>
        private Boolean IsAddressValid(Byte Address)
        {
            if (Address >= 0 && Address <= ADDR_LIMIT)
                return true;
            return false;
        }

        /// <summary>
        /// The machine code values for the PIC16F84 Byte Operations.
        /// </summary>
        private const UInt16 ADDWF	= 0x0700;	/* 00	0111	dfff	ffff */
        private const UInt16 ANDWF	= 0x0500;	/* 00	0101	dfff	ffff */
        private const UInt16 CLRF	= 0x0180;	/* 00	0001	1fff	ffff */
        private const UInt16 CLRW	= 0x0103;	/* 00	0001	0000	0011 */
        private const UInt16 COMF	= 0x0900;	/* 00	1001	dfff	ffff */
        private const UInt16 DECF	= 0x0300;   /* 00	0011	dfff	ffff */
        private const UInt16 DECFSZ	= 0x0B00;	/* 00	1011	dfff	ffff */
        private const UInt16 INCF	= 0x0A00;	/* 00	1010	dfff	ffff */
        private const UInt16 INCFSZ	= 0x0F00;	/* 00	1111	dfff	ffff */
        private const UInt16 IORWF	= 0x0400;	/* 00	0100	dfff	ffff */
        private const UInt16 MOVF	= 0x0800;	/* 00	1000	dfff	ffff */
        private const UInt16 MOVWF	= 0x0080;	/* 00	0000	1fff	ffff */
        private const UInt16 NOP	= 0x0000;	/* 00	0000	0000	0000 */
        private const UInt16 RLF	= 0x0D00;	/* 00	1101	dfff	ffff */
        private const UInt16 RRF	= 0x0C00;	/* 00	1100	dfff	ffff */
        private const UInt16 SUBWF	= 0x0200;	/* 00	0010	dfff	ffff */
        private const UInt16 SWAPWF	= 0x0E00;	/* 00	1110	dfff	ffff */
        private const UInt16 XORWF	= 0x0600;	/* 00	0110	dfff	ffff */

        /// <summary>
        /// The maximum address size for the file register. 
        /// </summary>
        private const UInt16 ADDR_LIMIT = 127;
    }
}
