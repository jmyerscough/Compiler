using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Compiler
{
    class CFunction : CIdentiferBase
    {
        public CFunction(String FunctionName, UInt16 MemoryAddress)
        {
            IdentifierName = FunctionName;
            this.MemoryAddress = (Byte)MemoryAddress;
            DataType = TDataType.VOID;
            IdentifierType = TIdentiferType.FUNCTION;
        }

        public int AddParameter(String ParamName)
        {
            if (m_Parameters == null)
                m_Parameters = new ArrayList();

            m_Parameters.Add(ParamName);
            return m_Parameters.Count;
        }

        public String GetParameter(int Index)
        {
            if (m_Parameters == null)
                return null;

            if (Index < 0 || Index >= m_Parameters.Count)
                return "";
            else
                return (String)m_Parameters[Index];
        }

        public int ParameterCount
        {
            get
            {
                return (m_Parameters == null) ? 0 : m_Parameters.Count;
            }
        }

        private ArrayList m_Parameters = null;
    }
}
