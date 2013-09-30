using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    /// <summary>
    /// 
    /// </summary>
    public class CP16F84ControlOperations : CControlOperations 
    {
        /// <summary>
        /// Literal: 0..255
        /// </summary>
        /// <param name="Literal"></param>
        /// <returns></returns>
        public override UInt16 AddLW(Byte Literal)
        {
            UInt16 IntelFormat = 0x00;

            /* the opcode needs to be in intel format. ie, bytes
               back to front.
               So the literal needs to be in the MSB and the OPCode in the
               LSB. */

            IntelFormat = Literal;
            IntelFormat <<= 8;
            IntelFormat |= (ADDLW >> 8);

            return IntelFormat;
        }

        /// <summary>
        /// Literal: 0..255
        /// </summary>
        /// <param name="Literal"></param>
        /// <returns></returns>
        public override UInt16 AndLW(Byte Literal)
        {
            UInt16 IntelFormat = 0x00;

            /* the opcode needs to be in intel format. ie, bytes
               back to front.
               So the literal needs to be in the MSB and the OPCode in the
               LSB. */

            IntelFormat = Literal;
            IntelFormat <<= 8;
            IntelFormat |= (ANDLW >> 8);

            return IntelFormat;
        }

        /// <summary>
        /// Address: 0..2047
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        public override UInt16 Call(Int16 Address)
        {
            UInt16 IntelFormat = 0x00;

            if (IsAddressValid(Address) == true)
            {
                IntelFormat = (UInt16)Address;
                IntelFormat <<= 8;
                IntelFormat |= (CALL >> 8);
            }
            return IntelFormat;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override UInt16 ClrWDT()
        {
            UInt16 IntelFormat = 0x00;

            IntelFormat = CLRWDT & 0x00FF;
            IntelFormat <<= 8;
            IntelFormat |= CLRWDT & 0xFF00;

            return IntelFormat;
        }

        /// <summary>
        /// Address: 0..2047
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        public override UInt16 Goto(Int16 Address)
        {
            UInt16 IntelFormat = 0x00;

            if (IsAddressValid(Address) == true)
            {
                IntelFormat = (UInt16)Address;
                IntelFormat <<= 8;
                IntelFormat |= (GOTO >> 8);
            }
            return IntelFormat;
        }

        /// <summary>
        /// Literal: 0..255
        /// </summary>
        /// <param name="Literal"></param>
        /// <returns></returns>
        public override UInt16 IorLW(Byte Literal)
        {
            UInt16 IntelFormat = 0x00;

            IntelFormat = Literal;
            IntelFormat <<= 8;
            IntelFormat |= (IORLW >> 8);

            return IntelFormat;
        }

        /// <summary>
        /// Literal: 0..255
        /// </summary>
        /// <param name="Literal"></param>
        /// <returns></returns>
        public override UInt16 MovLW(Byte Literal)
        {
            UInt16 IntelFormat = 0x00;

            IntelFormat = Literal;
            IntelFormat <<= 8;
            IntelFormat |= (MOVLW >> 8);

            return IntelFormat;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override UInt16 RetFIE()
        {
            UInt16 IntelFormat = 0x00;

            IntelFormat = RETFIE & 0x00FF;
            IntelFormat <<= 8;
            IntelFormat |= RETFIE & 0xFF00;

            return IntelFormat;
        }

        /// <summary>
        /// Literal: 0..255
        /// </summary>
        /// <param name="Literal"></param>
        /// <returns></returns>
        public override UInt16 RetLW(Byte Literal)
        {
            UInt16 IntelFormat = 0x00;

            IntelFormat = Literal;
            IntelFormat <<= 8;
            IntelFormat |= (RETLW >> 8);

            return IntelFormat;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override UInt16 Return()
        {
            UInt16 IntelFormat = 0x00;

            IntelFormat = RETURN & 0x00FF;
            IntelFormat <<= 8;
            IntelFormat |= RETURN & 0xFF00;

            return IntelFormat;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override UInt16 Sleep()
        {
            UInt16 IntelFormat = 0x00;

            IntelFormat = SLEEP & 0x00FF;
            IntelFormat <<= 8;
            IntelFormat |= SLEEP & 0xFF00;

            return IntelFormat;
        }

        /// <summary>
        /// Literal: 0..255
        /// </summary>
        /// <param name="Literal"></param>
        /// <returns></returns>
        public override UInt16 SubLW(Byte Literal)
        {
            UInt16 IntelFormat = 0x00;

            IntelFormat = Literal;
            IntelFormat <<= 8;
            IntelFormat |= (SUBLW >> 8);

            return IntelFormat;
        }

        /// <summary>
        /// Literal: 0..255
        /// </summary>
        /// <param name="Literal"></param>
        /// <returns></returns>
        public override UInt16 XorLW(Byte Literal)
        {
            UInt16 IntelFormat = 0x00;

            IntelFormat = Literal;
            IntelFormat <<= 8;
            IntelFormat |= (XORLW >> 8);

            return IntelFormat;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        private Boolean IsAddressValid(Int16 Address)
        {
	        if(Address >= 0 && Address <= MAX_ADDR)
		        return true;
	        return false;
        }

        /// <summary>
        /// The machine code values for the PIC16F84 Byte Operations.
        /// </summary>
        private const UInt16 ADDLW	=	0x3E00;	/* 11	111x	kkkk	kkkk	*/
        private const UInt16 ANDLW	=	0x3900;	/* 11	1001	kkkk	kkkk	*/
        private const UInt16 CALL	=	0x2000;	/* 10	0kkk	kkkk	kkkk	*/
        private const UInt16 CLRWDT	=	0x0064;	/* 00	0000	0110	0100	*/
        private const UInt16 GOTO	=	0x2800;	/* 10	1kkk	kkkk	kkkk	*/
        private const UInt16 IORLW	=	0x3800;	/* 11	1000	kkkk	kkkk	*/
        private const UInt16 MOVLW	=	0x3000;	/* 11	00xx	kkkk	kkkk	*/
        private const UInt16 RETFIE	=	0x0009;	/* 00	0000	0000	0001	*/
        private const UInt16 RETLW	=	0x3400;	/* 11	01xx	kkkk	kkkk	*/
        private const UInt16 RETURN	=	0x0008;	/* 00	0000	0000	1000	*/
        private const UInt16 SLEEP	=	0x0063;	/* 00	0000	0110	0011	*/
        private const UInt16 SUBLW  =	0x3C00;	/* 11	110x	kkkk	kkkk	*/
        private const UInt16 XORLW  =   0x3A00;	/* 11	1010	kkkk	kkkk	*/

        /// <summary>
        /// The Upper bound for the file register address.
        /// </summary>
        private const UInt16 MAX_ADDR =	0x07FF;	 
    }
}
