using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    /// <summary>
    /// 
    /// </summary>
   public class CP16F84BitOperations : CBitOperations 
    {
        /// <summary>
        /// SelectedBit: 0..7	
        /// FileAddress: 0..127
        /// </summary>
        /// <param name="SelectedBit"></param>
        /// <param name="FileAddress"></param>
        /// <returns></returns>
        public override UInt16 Bcf(TBits SelectedBit, Byte FileAddress)
        {
            UInt16 IntelFormat = 0x00;
            UInt16 OpCode = 0x00;

            if (IsFileAddressValid(FileAddress) == true)
            {
                OpCode = (UInt16)SelectedBit;
                OpCode <<= SELECTED_BIT_POSITION;
                OpCode |= FileAddress;
                OpCode |= BCF;

                IntelFormat |= (UInt16)(OpCode & 0xFF);
                IntelFormat <<= 8;
                IntelFormat |= (UInt16)(OpCode >> 8 & 0xFF);
            }
            return IntelFormat;
        }

        /// <summary>
        /// SelectedBit: 0..7 FileAddress: 0..127
        /// </summary>
        /// <param name="SelectedBit"></param>
        /// <param name="FileAddress"></param>
        /// <returns></returns>
        public override UInt16 Bsf(TBits SelectedBit, Byte FileAddress)
        {
            UInt16 IntelFormat = 0x00;
            UInt16 OpCode = 0x00;

            if (IsFileAddressValid(FileAddress) == true)
            {
                OpCode = (UInt16)SelectedBit;
                OpCode <<= SELECTED_BIT_POSITION;
                OpCode |= BSF;
                OpCode |= FileAddress;
                
                IntelFormat |= (UInt16)(OpCode & 0xFF);
                IntelFormat <<= 8;
                IntelFormat |= (UInt16)(OpCode >> 8 & 0xFF);
                            
            }
            return IntelFormat;
        }

        /// <summary>
        ///  SelectedBit: 0..7  FileAddress: 0..127
        /// </summary>
        /// <param name="SelectedBit"></param>
        /// <param name="FileAddress"></param>
        /// <returns></returns>
        public override UInt16 Btfsc(TBits SelectedBit, Byte FileAddress)
        {
            UInt16 IntelFormat = 0x00;
            UInt16 OpCode = 0x00;

            if (IsFileAddressValid(FileAddress) == true)
            {
                OpCode = (UInt16)SelectedBit;
                OpCode <<= SELECTED_BIT_POSITION;
                OpCode |= BTFSC;
                OpCode |= FileAddress;

                IntelFormat |= (UInt16)(OpCode & 0xFF);
                IntelFormat <<= 8;
                IntelFormat |= (UInt16)(OpCode >> 8 & 0xFF);
            }
            return IntelFormat;
        }

        /// <summary>
        /// SelectedBit: 0..7 FileAddress: 0..127
        /// </summary>
        /// <param name="SelectedBit"></param>
        /// <param name="FileAddress"></param>
        /// <returns></returns>
        public override UInt16 Btfss(TBits SelectedBit, Byte FileAddress)
        {
            UInt16 IntelFormat = 0x00;
            UInt16 OpCode = 0x00;

            if (IsFileAddressValid(FileAddress) == true)
            {
                OpCode = (UInt16)SelectedBit;
                OpCode <<= SELECTED_BIT_POSITION;
                OpCode |= BTFSS;
                OpCode |= FileAddress;

                IntelFormat |= (UInt16)(OpCode & 0xFF);
                IntelFormat <<= 8;
                IntelFormat |= (UInt16)(OpCode >> 8 & 0xFF);
            }
            return IntelFormat;
        }

        private Boolean IsFileAddressValid(Byte FileAddress)
        {
	        if(FileAddress >= 0 && FileAddress <= FILE_ADDR_LIMIT)
		        return true;
	        return false;
        }

        /// <summary>
        /// The machine code values for the PIC16F84 Byte Operations.
        /// </summary>
        private const UInt16 BCF   = 0x1000;	/* 01	00bb	bfff	ffff */
        private const UInt16 BSF   = 0x1400;	/* 01	01bb	bfff	ffff */
        private const UInt16 BTFSC = 0x1800;	/* 01	10bb	bfff	ffff */
        private const UInt16 BTFSS = 0x1C00;	/* 01	11bb	bfff	ffff */

        /// <summary>
        /// the number of shifts required to shift a value to bbb of the opcode. 
        /// </summary>
        private const UInt16 SELECTED_BIT_POSITION	= 0x0007;	
        
        /// <summary>
        /// The Upper bound for the file register address.
        /// </summary>
        private const UInt16 FILE_ADDR_LIMIT = 0x007F;
    }
}
