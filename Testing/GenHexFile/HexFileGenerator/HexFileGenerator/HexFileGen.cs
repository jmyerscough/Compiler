using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using Compiler;

namespace HexFileGenerator
{
    class HexFileGen
    {

        static void Main(string[] args)
        {
            try
            {

                CPicCodeGenerator Code = new CPicCodeGenerator("Jason.HEX");
                CP16F84ByteOperations Instructions = new CP16F84ByteOperations();
                CP16F84ControlOperations Control = new CP16F84ControlOperations();
                Code.AddInstruction( Control.MovLW(5)     );
               // Code.AddInstruction( Instructions.MovWF(0x0C) );
               // Code.AddInstruction( Control.MovLW(5)     );
               // Code.AddInstruction( Instructions.SubWF(TRegisters.WORKING_REGISTER, 0x0C) );
               // Code.AddInstruction( Instructions.MovWF(0x0D));

                Code.GenerateHexFile();
            }
            catch (Exception)
            {
            }
        }
    }
}
