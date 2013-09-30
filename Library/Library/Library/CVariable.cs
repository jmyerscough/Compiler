using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    class CVariable : CIdentiferBase
    {
        public CVariable()
        {

        }

        /// <summary>
        /// Initialises a new CVariable object.
        /// </summary>
        /// <param name="VariableName">The name of the variable.</param>
        /// <param name="VariableDataType">The data type of the variable.</param>
        /// <param name="VariableBlockScopeLevel">The variables scope level.</param>
        public CVariable(String VariableName, TDataType VariableDataType, Int32 VariableBlockScopeLevel, Int16 SPMemLocation)
        {
            IdentifierName = VariableName;
            IdentifierType = TIdentiferType.VARIABLE;
            DataType = VariableDataType;
            ScopeLevel = VariableBlockScopeLevel;
            GenerateIdentifierId(IdentifierName, ScopeLevel);

            if (SPMemLocation > 0)
                MemoryAddress = SPMemLocation;
            else
                MemoryAddress = m_VariableAddressSpace++;
        }

        /// <summary>
        /// This variable marks the memory location currently
        /// available for variables. When a variable object is
        /// created. The current value of m_VariableAddressSpace
        /// is assigned to MemoryLocation and is then incremented by 1
        /// which will be the next available memory location.
        /// </summary>
        private static Int16 m_VariableAddressSpace = 0x10;
    }
}
