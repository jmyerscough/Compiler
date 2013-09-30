using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class CByteOrientedOperations 
    {
        /// <summary>
        /// Add Working Register to File Register
        /// </summary>
        /// <param name="Destination">Set to 1 to store the results in the working register or 0 to
        /// store the result in the file register.</param>
        /// <param name="FlagAddress">Address of File register. The address has to be in the range of
        /// 0 ... 127 </param>
        /// <returns>Machine code value of instruction</returns>
        public abstract UInt16 AddWF(TRegisters Destination, Byte FlagAddress);

        /// <summary>
        /// And the working register and file register.
        /// </summary>
        /// <param name="Destination">Set to 1 to store the results in the working register or 0 to
        /// store the result in the file register.</param>
        /// <param name="FlagAddress">Address of File register. The address has to be in the range of
        /// 0 ... 127 </param>
        /// <returns>Machine code value of instruction</returns>
        public abstract UInt16 AndWF(TRegisters Destination, Byte FlagAddress);
	
        /// <summary>
        /// Clears the File register
        /// </summary>
        /// <param name="FlagAddress">Address of File register. The address has to be in the range of
        /// 0 ... 127 </param>
        /// <returns>Machine code value of instruction</returns>
    	public abstract UInt16 ClrF(Byte FlagAddress);
	
        /// <summary>
        /// Clear the working register
        /// </summary>
        /// <returns>Machine code value of instruction</returns>
    	public abstract UInt16 ClrW();
	
        /// <summary>
        /// Complements the File register.
        /// </summary>
        /// <param name="Destination">Set to 1 to store the results in the working register or 0 to
        /// store the result in the file register.</param>
        /// <param name="FlagAddress">Address of File register. The address has to be in the range of
        /// 0 ... 127</param>
        /// <returns>Machine code value of instruction</returns>
        public abstract UInt16 ComF(TRegisters Destination, Byte FlagAddress);
	
        /// <summary>
        /// Decrements the file register
        /// </summary>
        /// <param name="Destination">Set to 1 to store the results in the working register or 0 to
        /// store the result in the file register.</param>
        /// <param name="FlagAddress">Address of File register. The address has to be in the range of
        /// 0 ... 127</param>
        /// <returns>Machine code value of instruction</returns>
        public abstract UInt16 DecF(TRegisters Destination, Byte FlagAddress);
	
    	/// <summary>
    	/// Decrements the file register if it is != 0
    	/// </summary>
        /// <param name="Destination">Set to 1 to store the results in the working register or 0 to
        /// store the result in the file register.</param>
        /// <param name="FlagAddress">Address of File register. The address has to be in the range of
        /// 0 ... 127</param>
        /// <returns>Machine code value of instruction</returns>
        public abstract UInt16 DecFSZ(TRegisters Destination, Byte FlagAddress);
	
	    /// <summary>
	    /// Increment the File register
	    /// </summary>
        /// <param name="Destination">Set to 1 to store the results in the working register or 0 to
        /// store the result in the file register.</param>
        /// <param name="FlagAddress">Address of File register. The address has to be in the range of
        /// 0 ... 127</param>
        /// <returns>Machine code value of instruction</returns>
        public abstract UInt16 IncF(TRegisters Destination, Byte FlagAddress);
	
	    /// <summary>
	    /// Increment the file register if != 0
	    /// </summary>
        /// <param name="Destination">Set to 1 to store the results in the working register or 0 to
        /// store the result in the file register.</param>
        /// <param name="FlagAddress">Address of File register. The address has to be in the range of
        /// 0 ... 127</param>
        /// <returns>Machine code value of instruction</returns>
        public abstract UInt16 IncFSZ(TRegisters Destination, Byte FlagAddress);
	
	    /// <summary>
	    /// Inclusive OR working register with the File register
	    /// </summary>
        /// <param name="Destination">Set to 1 to store the results in the working register or 0 to
        /// store the result in the file register.</param>
        /// <param name="FlagAddress">Address of File register. The address has to be in the range of
        /// 0 ... 127</param>
        /// <returns>Machine code value of instruction</returns>
        public abstract UInt16 IorWF(TRegisters Destination, Byte FlagAddress);
	
	    /// <summary>
	    /// Move the file register
	    /// </summary>
        /// <param name="Destination">Set to 1 to store the results in the working register or 0 to
        /// store the result in the file register.</param>
        /// <param name="FlagAddress">Address of File register. The address has to be in the range of
        /// 0 ... 127</param>
        /// <returns>Machine code value of instruction</returns>
        public abstract UInt16 MovF(TRegisters Destination, Byte FlagAddress);

	    /// <summary>
	    /// Move the working register to the file register
	    /// </summary>
        /// <param name="FlagAddress">Address of File register. The address has to be in the range of
        /// 0 ... 127</param>
        /// <returns>Machine code value of instruction</returns>
        public abstract UInt16 MovWF(Byte FlagAddress);

	    /// <summary>
	    /// No operation.
	    /// </summary>
        /// <returns>Machine code value of instruction</returns>
        public abstract UInt16 Nop();
	
	    /// <summary>
	    /// Rotate the file register left through the carry flag.
	    /// </summary>
        /// <param name="Destination">Set to 1 to store the results in the working register or 0 to
        /// store the result in the file register.</param>
        /// <param name="FlagAddress">Address of File register. The address has to be in the range of
        /// 0 ... 127</param>
        /// <returns>Machine code value of instruction</returns>
        public abstract UInt16 RlF(TRegisters Destination, Byte FlagAddress);
	
	    /// <summary>
	    /// Rotate the file register right through the carry flag.
	    /// </summary>
        /// <param name="Destination">Set to 1 to store the results in the working register or 0 to
        /// store the result in the file register.</param>
        /// <param name="FlagAddress">Address of File register. The address has to be in the range of
        /// 0 ... 127</param>
        /// <returns>Machine code value of instruction</returns>
        public abstract UInt16 RrF(TRegisters Destination, Byte FlagAddress);
	
        /// <summary>
        /// Subtract the Working Register from File Register
        /// </summary>
        /// <param name="Destination">Set to 1 to store the results in the working register or 0 to
        /// store the result in the file register.</param>
        /// <param name="FlagAddress">Address of File register. The address has to be in the range of
        /// 0 ... 127</param>
        /// <returns>Machine code value of instruction</returns>
        public abstract UInt16 SubWF(TRegisters Destination, Byte FlagAddress);
	
	    /// <summary>
	    /// Swap the nibbles in the File Register
	    /// </summary>
        /// <param name="Destination">Set to 1 to store the results in the working register or 0 to
        /// store the result in the file register.</param>
        /// <param name="FlagAddress">Address of File register. The address has to be in the range of
        /// 0 ... 127</param>
        /// <returns>Machine code value of instruction</returns>
        public abstract UInt16 SwapWF(TRegisters Destination, Byte FlagAddress);
	
	    /// <summary>
	    /// XOR the working register with the file register.
	    /// </summary>
        /// <param name="Destination">Set to 1 to store the results in the working register or 0 to
        /// store the result in the file register.</param>
        /// <param name="FlagAddress">Address of File register. The address has to be in the range of
        /// 0 ... 127</param>
        /// <returns>Machine code value of instruction</returns>
        public abstract UInt16 XorWF(TRegisters  Destination, Byte FlagAddress);
    }
}
