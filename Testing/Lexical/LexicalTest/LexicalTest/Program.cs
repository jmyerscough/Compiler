using System;
using System.Collections.Generic;
using System.Text;
using Compiler;

namespace LexicalTest
{
    class Program
    {
        static void Main(string[] args)
        {
             CCompiler Compiler = new CCompiler("Example.jasc", "keywords.xml", "SpecialRegisters.xml", PIC_PROCESSOR.PIC16F84);

             try
             {
                 if (Compiler.Build() == true)
                 {
                     Console.WriteLine("0 Error(s)");
                     Compiler.OuputGeneratedFiles(true);
                 }
             }
             catch (CCompilerException Error)
             {
                 Console.WriteLine(Error.Message + " on line " + Error.LineNumber);
             }
        }
    }
}
