using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class CControlOperations
    {
        /// <summary>
        /// Literal: 0..255
        /// </summary>
        /// <param name="Literal"></param>
        /// <returns></returns>
        public abstract UInt16 AddLW(Byte Literal);
	
        /// <summary>
        /// Literal: 0..255
        /// </summary>
        /// <param name="Literal"></param>
        /// <returns></returns>
	    public abstract UInt16 AndLW(Byte Literal);
	
        /// <summary>
        /// Address: 0..2047
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
	    public abstract UInt16 Call(Int16 Address);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
	    public abstract UInt16 ClrWDT();
    
        /// <summary>
        /// Address: 0..2047
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
	    public abstract UInt16 Goto(Int16 Address);
	
        /// <summary>
        /// Literal: 0..255
        /// </summary>
        /// <param name="Literal"></param>
        /// <returns></returns>
	    public abstract UInt16 IorLW(Byte Literal);
	
        /// <summary>
        /// Literal: 0..255
        /// </summary>
        /// <param name="Literal"></param>
        /// <returns></returns>
	    public abstract UInt16 MovLW(Byte Literal);
    	
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
	    public abstract UInt16 RetFIE();
    	
        /// <summary>
        /// Literal: 0..255
        /// </summary>
        /// <param name="Literal"></param>
        /// <returns></returns>
	    public abstract UInt16 RetLW(Byte Literal);
    	
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
	    public abstract UInt16 Return();
    	
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
	    public abstract UInt16 Sleep();
    	
        /// <summary>
        /// Literal: 0..255
        /// </summary>
        /// <param name="Literal"></param>
        /// <returns></returns>
	    public abstract UInt16 SubLW(Byte Literal);

        /// <summary>
        /// Literal: 0..255
        /// </summary>
        /// <param name="Literal"></param>
        /// <returns></returns>
        public abstract UInt16 XorLW(Byte Literal);
    }
}
