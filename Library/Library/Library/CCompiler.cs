using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    /// <summary>
    /// Contains all the components of the compiler and links 
    /// each component together.
    /// </summary>
    public class CCompiler
    {
        public CCompiler(String SourceFile, String Keywords, String SpecialRegisters, PIC_PROCESSOR microp)
        {
            try
            {
                String HexFile = SourceFile.Substring(0, SourceFile.Length - 5);
                HexFile += ".hex";
                m_LexScanner = new CLexScanner(SourceFile, Keywords);
                m_SymbolTbl = new CSymbolTable(SpecialRegisters);
                m_CodeGenerator = new CPicCodeGenerator(HexFile);
                m_MachineCodeOps = new CMachineCodes(microp);
                m_Parser = new CParser(m_LexScanner, m_SymbolTbl, m_MachineCodeOps, m_CodeGenerator);
            }
            catch (Exception)
            {
                throw new CCompilerException("Error initialising the compiler", TErrorCodes.COMPILER_ERROR);
            }
            finally
            {
               
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Boolean Build()
        {
            try
            {
                m_Parser.Start();
            }
            catch (CCompilerException)
            {
                // Let the user of the Compiler library
                // handle the expections thrown by the
                // library.
                throw;
            }
            finally
            {
                m_LexScanner.CloseSourceFile();
                
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Debug"></param>
        public void OuputGeneratedFiles(Boolean Debug)
        {
            m_CodeGenerator.GenerateHexFile();
            if(Debug == true)
                m_CodeGenerator.GenerateXmlFile("DEBUG_HEX.xml");
        }

        /// <summary>
        /// The Compilers Lexical Scanner
        /// </summary>
        private CLexScanner m_LexScanner = null;

        /// <summary>
        /// The compilers parser.
        /// </summary>
        private CParser m_Parser = null;

        /// <summary>
        /// Used to store variable and function information.
        /// </summary>
        private CSymbolTable m_SymbolTbl = null;

        /// <summary>
        /// This class generates the .HEX and .XML files.
        /// </summary>
        private CPicCodeGenerator m_CodeGenerator = null;

        private CMachineCodes m_MachineCodeOps = null;
    }
}