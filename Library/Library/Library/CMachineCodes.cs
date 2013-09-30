using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    class CMachineCodes
    {
        public CMachineCodes(PIC_PROCESSOR ProcessorType)
        {
            // Create instances of the code generation classes depending on the 
            // processor type.
            switch (ProcessorType)
            {
                case PIC_PROCESSOR.PIC16F84:
                    BitOperations       = new CP16F84BitOperations();
                    ByteOperations      = new CP16F84ByteOperations();
                    ControlOperations   = new CP16F84ControlOperations();
                    break;
                default:
                    BitOperations       = null;
                    ByteOperations      = null;
                    ControlOperations   = null;
                    break;
            }
        }

        public CBitOperations BitOperations;
        public CByteOrientedOperations ByteOperations;
        public CControlOperations ControlOperations;
    }
}
