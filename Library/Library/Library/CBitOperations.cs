using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class CBitOperations
    {
        /// <summary>
        /// SelectedBit: 0..7	
        /// FileAddress: 0..127
        /// </summary>
        /// <param name="SelectedBit"></param>
        /// <param name="FileAddress"></param>
        /// <returns></returns>
        public abstract UInt16 Bcf(TBits SelectedBit, Byte FileAddress);
	 
        /// <summary>
        /// SelectedBit: 0..7 FileAddress: 0..127
        /// </summary>
        /// <param name="SelectedBit"></param>
        /// <param name="FileAddress"></param>
        /// <returns></returns>
        public abstract UInt16 Bsf(TBits SelectedBit, Byte FileAddress);
	
	    /// <summary>
        ///  SelectedBit: 0..7  FileAddress: 0..127
        /// </summary>
        /// <param name="SelectedBit"></param>
        /// <param name="FileAddress"></param>
        /// <returns></returns>
        public abstract UInt16 Btfsc(TBits SelectedBit, Byte FileAddress);
	
        /// <summary>
        /// SelectedBit: 0..7 FileAddress: 0..127
        /// </summary>
        /// <param name="SelectedBit"></param>
        /// <param name="FileAddress"></param>
        /// <returns></returns>
        public abstract UInt16 Btfss(TBits SelectedBit, Byte FileAddress);
    }
}
