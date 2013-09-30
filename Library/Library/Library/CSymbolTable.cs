using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Xml;

namespace Compiler
{
    class CSymbolTable : Hashtable 
    {
        /// <summary>
        /// The contrucutor loads the SpecialRegister file and stores
        /// register and memory address in the symbol table.
        /// </summary>
        public CSymbolTable(String Filename)
            : base()
        {
            XmlReader SpecialRegFile = null;
            try
            {
                SpecialRegFile = XmlReader.Create(Filename, new XmlReaderSettings());

                while (SpecialRegFile.Read() == true)
                {
                    CVariable SpecialRegisters = null;
                    String VariableName = "";
                    Int16 MemoryLocation = 0;

                    if (SpecialRegFile.NodeType == XmlNodeType.Element)
                    {
                        if (Int16.TryParse(SpecialRegFile.GetAttribute("address"), out (short)MemoryLocation) == true)
                        {
                            SpecialRegFile.Read();
                            VariableName = SpecialRegFile.Value;
                            SpecialRegisters = new CVariable(VariableName, TDataType.INTEGER, 0, MemoryLocation);
                            Add(VariableName, SpecialRegisters);
                        }
                    }
                }
            }
            catch (InvalidOperationException)
            {
                throw new CCompilerException("Cannot Open " + Filename, TErrorCodes.FILE_ACCESS_ERR);
            }
            finally
            {
                SpecialRegFile.Close();
            }
        }
    }
}
