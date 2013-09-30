using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    /// <summary>
    /// The base class for the Variable and Function entries in the
    /// symbol table.
    /// </summary>
    abstract class CIdentiferBase
    {
        /// <summary>
        /// sets and gets m_IdentifierName
        /// </summary>
        public String IdentifierName
        {
            get
            {
                return m_IdentifierName;
            }

            set
            {
                m_IdentifierName = value;
            }
        }

        /// <summary>
        /// gets and sets m_IdentifierType.
        /// </summary>
        public TIdentiferType IdentifierType
        {
            get
            {
                return m_IdentifierType;
            }

            set
            {
                m_IdentifierType = value;
            }
        }

        /// <summary>
        /// Gets and Sets m_DataType.
        /// </summary>
        public TDataType DataType
        {
            get
            {
                return m_IdentifierDataType;
            }

            set
            {
                m_IdentifierDataType = value;
            }
        }

        /// <summary>
        /// Gets and Sets the m_ScopeLevel.
        /// </summary>
        public Int32 ScopeLevel
        {
            get
            {
                return m_ScopeLevel;
            }

            set
            {
                m_ScopeLevel = value;
            }
        }

        /// <summary>
        /// Gets and Sets the variables memory location.
        /// </summary>
        public Int16 MemoryAddress
        {
            get
            {
                return m_MemoryLocation;
            }

            set
            {
                m_MemoryLocation = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>The identifiers unique Id.</returns>
        public String GetIdentifierId()
        {
            return m_IdentifierKey;
        }

        /// <summary>
        /// A unique Id value is needed by CSymbolTable to be used as the 
        /// Key value.
        /// </summary>
        /// <param name="IdName">The identifiers name.</param>
        /// <param name="ScopeLevel">The identifiers scope.</param>
        public void GenerateIdentifierId(String IdName, Int32 ScopeLevel)
        {
            m_IdentifierKey = IdName + Convert.ToString(ScopeLevel); 
        }

        /// <summary>
        /// Holds the Identifiers name.
        /// </summary>
        private String m_IdentifierName = "";

        /// <summary>
        /// Specifies whether the identifier is a variable or
        /// a function.
        /// </summary>
        private TIdentiferType m_IdentifierType;

        /// <summary>
        /// The data type of the identifier. This is also used to 
        /// indicate a functions return type.
        /// </summary>
        private TDataType m_IdentifierDataType;

        /// <summary>
        /// Indicates the scope of the identifier. This is always Global
        /// for functions. 
        /// </summary>
        private Int32 m_ScopeLevel = 0;

        /// <summary>
        /// A unique value made up of the Identifier name and 
        /// m_ScopeLevel value.
        /// </summary>
        private String m_IdentifierKey = "";

        /// <summary>
        /// The Identifiers memory location on the PIC
        /// </summary>
        private Int16 m_MemoryLocation = 0;
    }
}
