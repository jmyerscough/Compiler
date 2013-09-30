using System;
using System.Collections.Generic;
using System.Text;

// The context free grammar I used and modified can be found at: 
// http://lists.canonical.org/pipermail/kragen-hacks/1999-October/000201.html
//
// My compiler implemented a sub set of the grammar found at the above URL.


namespace Compiler
{
    /// <summary>
    /// LL(1) Parser. Recursive Descent Calls the lexical Analyiser
    /// the symbol table and the semantice(code generation) methods.
    /// 
    /// First Version: 13/12/06 - 
    /// 
    /// Modified [10/01/07]
    /// The parser now processes functions and allows a source file to contain more than
    /// one function. The new functions added to the class are:
    /// 
    /// void FunctionDefinition()
    /// void ParameterList()
    /// void ParameterDeclaration()
    /// 
    /// Modified [11/01/07] 
    /// New member variable added to the class: m_CurrentFunction. This variable
    /// is used when generating a Hash key value for symbols added to the symbol
    /// table.
    /// 
    /// Modified [15/01/07] This version on the Parser is a Prototype, which
    /// will output PIC assembly code to the console window. The purpose is to
    /// Check Semantic routines will be called in the correct place. Please note,
    /// Once the output calls are in the correct place they will be replaced with 
    /// the Semantic routines.
    /// </summary>
    sealed class CParser
    {
        /// <summary>
        /// Modified: [10/01/07] The constructor now accepts the symbol table
        /// as a parameter. The Symbol table is initialised by the CCompiler 
        /// object and populated by the CParser obejct.
        /// </summary>
        /// <param name="LexicalScanner">The lexical scanner that scans the JasC source file.</param>
        /// <param name="SymTbl">Symbol table used to store variable and function information.</param>
        public CParser(CLexScanner LexicalScanner, CSymbolTable SymTbl, CMachineCodes MachineCodeOps, CPicCodeGenerator CGen)
        {
            m_LexScanner = LexicalScanner;
            m_SymbolTable = SymTbl;
            m_CodeGen = CGen;
            m_MachineCodeOps = MachineCodeOps;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Start()
        {
            CFunction MainF = null;
            
            // Process functions, there could be more than one.
            FunctionDefinition();
            m_CodeGen.AddInstruction(m_MachineCodeOps.ControlOperations.Return());

            // Parse all the functions.
            while (IsDataType(m_LexScanner.NextToken()))
            {
                FunctionDefinition();
                m_CodeGen.AddInstruction(m_MachineCodeOps.ControlOperations.Return());
            }

            MainF = (CFunction)m_SymbolTable["main"];

            m_CodeGen.ModifyOpCode(0, 0, m_MachineCodeOps.ControlOperations.Call((Byte)MainF.MemoryAddress)); 
        }

        /// <summary>
        /// FunctionDefinition = <TypeSpecifier> <Declarator> <BlockStatement>
        /// </summary>
        public void FunctionDefinition()
        {
            CFunction NewFunction = null;

            TDataType ReturnType = TypeSpecifier();

            // the next valid token should be the name of function.
            if (m_LexScanner.NextToken() == TTokens.IDENTIFIER)
                m_CurrentFunction = m_LexScanner.TokenValue;

            m_CodeGen.AddInstruction(m_MachineCodeOps.ByteOperations.Nop());
            NewFunction = new CFunction(m_CurrentFunction, (Byte)(m_CodeGen.CurrentCodeMemoryLocation));
            NewFunction.DataType = ReturnType;

            Declarator(NewFunction);

            m_SymbolTable.Add(m_CurrentFunction, NewFunction);
            BlockStatement();
        }

        /// <summary>
        /// 
        /// </summary>
        public String Declarator(CFunction FunctionDeclaration)
        {
            String Identifer = m_LexScanner.TokenValue;

            if (m_LexScanner.Match(TTokens.IDENTIFIER) == false)
                throw new CCompilerException("Identifier Expected.", m_LexScanner.LineNumber,
                                                                            TErrorCodes.PARSER_ERROR);
            if (m_LexScanner.NextToken() == TTokens.LPAREN)
            {
                m_LexScanner.Match(TTokens.LPAREN);
                
                // the parameter list maybe empty. If the parameter list is
                // empty then the next valid token should be )
                if(m_LexScanner.NextToken() != TTokens.RPAREN)
                    ParameterList(FunctionDeclaration);
                
                if (m_LexScanner.Match(TTokens.RPAREN) == false)
                    throw new CCompilerException(") Expected.", m_LexScanner.LineNumber, 
                                                                    TErrorCodes.PARSER_ERROR);
            }
            return Identifer;
        }

        /// <summary>
        /// Used for declaring variables.
        /// </summary>
        public void Declaration()
        {
            TypeSpecifier();
            InitDeclarations();

            if (m_LexScanner.Match(TTokens.SEMICOL) == false)
                throw new CCompilerException("Missing Semicolon.", m_LexScanner.LineNumber,
                                                                        TErrorCodes.PARSER_ERROR);
        }

        /// <summary>
        /// 
        /// </summary>
        public TDataType TypeSpecifier()
        {
            switch (m_LexScanner.NextToken())
            {
                case TTokens.INT:
                    m_LexScanner.Match(TTokens.INT);
                    return TDataType.INTEGER;
                case TTokens.SIGNED:
                    m_LexScanner.Match(TTokens.SIGNED);
                    return TDataType.INTEGER;
                case TTokens.UNSIGNED:
                    m_LexScanner.Match(TTokens.UNSIGNED);
                    return TDataType.INTEGER;
                case TTokens.VOID:
                    m_LexScanner.Match(TTokens.VOID);
                    return TDataType.VOID;
                default:
                    throw new CCompilerException("Data type expected.", m_LexScanner.LineNumber,
                                                                            TErrorCodes.PARSER_ERROR);
            }
                
        }

        /// <summary>
        /// 
        /// </summary>
        public void InitDeclarations()
        {
            CIdentiferBase NewIdentifier = null;
            
            // used for debugging
            String TokenVal = "";
            
            if (m_LexScanner.Match(TTokens.IDENTIFIER) == false)
                throw new CCompilerException("Identifier expected.", m_LexScanner.LineNumber,
                                                                            TErrorCodes.PARSER_ERROR);

            // add the variable to the symbol table.
            // The key value for the symbol entry is the identifier name and 
            // scope level value and the name of the current function.
            if (m_SymbolTable.ContainsKey(m_LexScanner.TokenValue) == true)
                throw new CCompilerException("Identifier already declared.", m_LexScanner.LineNumber,
                                                                                TErrorCodes.PARSER_ERROR);
            else
            {
                TokenVal = m_LexScanner.TokenValue;
                NewIdentifier = new CVariable(m_LexScanner.TokenValue, TDataType.INTEGER,
                                                                m_LexScanner.ScopeLevelCount, 0);
                m_SymbolTable.Add(m_LexScanner.TokenValue, NewIdentifier);
            }
            if (m_LexScanner.NextToken() == TTokens.ASSIGNMENT)
            {
                AssignmentExpression(ref m_CurrentOperator);
            }

            while (m_LexScanner.NextToken() == TTokens.COMMA)
            {
                m_LexScanner.Match(TTokens.COMMA);
                InitDeclarations();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void BlockStatement()
        {
            if (m_LexScanner.Match(TTokens.LCURLY) == false)
                throw new CCompilerException("{ expected.", m_LexScanner.LineNumber,
                                                                    TErrorCodes.PARSER_ERROR);

            while (IsDataType(m_LexScanner.NextToken()) == true)
                Declaration();

            while (m_LexScanner.NextToken() != TTokens.RCURLY)
                Statement();

            if (m_LexScanner.Match(TTokens.RCURLY) == false)
                throw new CCompilerException("} expected.", m_LexScanner.LineNumber,
                                                                    TErrorCodes.PARSER_ERROR);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Statement()
        {
            switch (m_LexScanner.NextToken())
            {
                case TTokens.IDENTIFIER:
                    // Check the identifier has been declared
                    CFunction IsMethod = null;

                    if (m_SymbolTable.ContainsKey(m_LexScanner.TokenValue) == false)
                        throw new CCompilerException("Undeclared identifier.", m_LexScanner.LineNumber,
                                                                                    TErrorCodes.PARSER_ERROR);

                    /** Check to see if the Identifier is a function **/
                    if (m_SymbolTable[m_LexScanner.TokenValue] is CFunction)
                    {
                        IsMethod = (CFunction)m_SymbolTable[m_LexScanner.TokenValue];
                        ProcessFunctionCall(IsMethod);
                    }
                    else
                        AssignmentExpression(ref m_CurrentOperator);
                    
                   if (m_LexScanner.Match(TTokens.SEMICOL) == false)
                        throw new CCompilerException("; expected.", m_LexScanner.LineNumber,
                            TErrorCodes.PARSER_ERROR);
                    break;
                case TTokens.IF:
                    IfStatement();
                    break;
                case TTokens.WHILE:
                    ProcessWhileLoop();
                    break;
                case TTokens.DOWHILE:
                    ProcessDoWhileLoop();
                    break;
                case TTokens.LCURLY:
                    BlockStatement();
                    break;
                case TTokens.RETURN:
                    m_LexScanner.Match(TTokens.RETURN);
                    Expression(ref m_CurrentOperator);
                    break;
                default:
                    Expression(ref m_CurrentOperator);
                    if (m_LexScanner.Match(TTokens.SEMICOL) == false)
                        throw new CCompilerException("; expected.", m_LexScanner.LineNumber,
                            TErrorCodes.PARSER_ERROR);
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String Expression(ref TTokens Operator)
        {
            return AssignmentExpression(ref Operator);
        }

        /// <summary>
        /// The result will be stored in the working register.
        /// This method moves the value stored in the working 
        /// register into the L-Value. (The identifier on the left
        /// side of the assignment operator.)
        /// </summary>
        public String AssignmentExpression(ref TTokens Operator)
        {
            // The file register, which will store the value
            // of the assignment operation.
            String FileRegister = m_LexScanner.TokenValue;
            Boolean BitLevel = false;
            
            UnaryExpression();
            BitLevel = m_BitLevelManipulation;

            do
            {
                if (m_LexScanner.NextToken() == TTokens.ASSIGNMENT)
                {
                    m_LexScanner.Match(TTokens.ASSIGNMENT);
                    ConditionalExpression(ref Operator);
                    // move the result of the expression into the FileRegister.

                    // check the variable is in the symbol table.
                    CVariable T = null;
                    if (m_SymbolTable.Contains(FileRegister) == true)
                        T = (CVariable)m_SymbolTable[FileRegister];

                    if (BitLevel == true)
                    {
                        // check the value stored in the working register.
                        m_CodeGen.AddInstruction(m_MachineCodeOps.BitOperations.Bcf((TBits)m_VariableIndex, (Byte)T.MemoryAddress));
                        m_CodeGen.AddInstruction(m_MachineCodeOps.ControlOperations.SubLW(0));
                        m_CodeGen.AddInstruction(m_MachineCodeOps.BitOperations.Btfss(TBits.BIT2, 3));
                        m_CodeGen.AddInstruction(m_MachineCodeOps.BitOperations.Bsf((TBits)m_VariableIndex, (Byte)T.MemoryAddress));
                    }
                    else
                    {
                        if (IsInSecondBank(FileRegister) == true)
                            m_CodeGen.AddInstruction(m_MachineCodeOps.BitOperations.Bsf(TBits.BIT5, 3));

                        m_CodeGen.AddInstruction(m_MachineCodeOps.ByteOperations.MovWF((Byte)T.MemoryAddress));
                        m_CodeGen.AddInstruction(m_MachineCodeOps.ByteOperations.Nop());

                        if (IsInSecondBank(FileRegister) == true)
                            m_CodeGen.AddInstruction(m_MachineCodeOps.BitOperations.Bcf(TBits.BIT5, 3));
                    }
              }
            } while (m_LexScanner.NextToken() == TTokens.ASSIGNMENT);

            if (m_LexScanner.NextToken() != TTokens.SEMICOL)
                ConditionalExpression(ref Operator);
            return FileRegister;
        }

        /// <summary>
        /// 
        /// </summary>
        public String ConditionalExpression(ref TTokens Operator)
        {
            return LogicalOrExpression(ref Operator);
        }

        /// <summary>
        /// 
        /// </summary>
        public String LogicalOrExpression(ref TTokens Operator)
        {
            String Identifier = LogicalAndExpression(ref Operator);

            do
            {
                if (m_LexScanner.NextToken() == TTokens.OROP)
                {
                    m_LexScanner.Match(TTokens.OROP);
                    Operator = TTokens.OROP;

                    m_CodeGen.AddInstruction(m_MachineCodeOps.ByteOperations.MovWF(
                                           (Byte)m_TempStoreMemoryLocation++));

                    LogicalAndExpression(ref Operator);

                    m_CodeGen.AddInstruction(m_MachineCodeOps.ByteOperations.AndWF(TRegisters.WORKING_REGISTER,
                            Convert.ToByte(--m_TempStoreMemoryLocation))); 
                }
            } while (m_LexScanner.NextToken() == TTokens.OROP);
            return Identifier;
        }

        /// <summary>
        /// 
        /// </summary>
        public String LogicalAndExpression(ref TTokens Operator)
        {
            String Identifier = InclusiveOrExpression(ref Operator);

            do
            {
                if (m_LexScanner.NextToken() == TTokens.ANDOP)
                {
                    m_LexScanner.Match(TTokens.ANDOP);
                    Operator = TTokens.ANDOP;

                    m_CodeGen.AddInstruction(m_MachineCodeOps.ByteOperations.MovWF(
                                           (Byte)m_TempStoreMemoryLocation++));

                    InclusiveOrExpression(ref Operator);

                    m_CodeGen.AddInstruction(m_MachineCodeOps.ByteOperations.AndWF(TRegisters.WORKING_REGISTER,
                            Convert.ToByte(--m_TempStoreMemoryLocation))); 
                }
            } while (m_LexScanner.NextToken() == TTokens.ANDOP);
            return Identifier;
        }

        /// <summary>
        /// 
        /// </summary>
        public String InclusiveOrExpression(ref TTokens Operator)
        {
            String Identifier = ExclusiveOrExpression(ref Operator);

            if (m_LexScanner.NextToken() == TTokens.ORBITOP)
            {
                m_LexScanner.Match(TTokens.ORBITOP);

                m_CodeGen.AddInstruction(m_MachineCodeOps.ByteOperations.MovWF(
                                                                (Byte)m_TempStoreMemoryLocation++));
                ExclusiveOrExpression(ref Operator);

                m_CodeGen.AddInstruction(m_MachineCodeOps.ByteOperations.IorWF(TRegisters.WORKING_REGISTER,
                        Convert.ToByte(--m_TempStoreMemoryLocation))); 
            }
            return Identifier;
        }

        /// <summary>
        /// 
        /// </summary>
        public String ExclusiveOrExpression(ref TTokens Operator)
        {
            String Identifier = AndExpression(ref Operator);

            do
            {
                if (m_LexScanner.NextToken() == TTokens.XOROP)
                {
                    m_LexScanner.Match(TTokens.XOROP);


                    m_CodeGen.AddInstruction(m_MachineCodeOps.ByteOperations.MovWF(
                                                                (Byte)m_TempStoreMemoryLocation++));
                    AndExpression(ref Operator);                    

                    m_CodeGen.AddInstruction(m_MachineCodeOps.ByteOperations.XorWF(TRegisters.WORKING_REGISTER,
                            Convert.ToByte(--m_TempStoreMemoryLocation))); 
                }
            } while (m_LexScanner.NextToken() == TTokens.XOROP);
            return Identifier;
        }

        /// <summary>
        /// 
        /// </summary>
        public String AndExpression(ref TTokens Operator)
        {
            String Identifier = EqualityExpression(ref Operator);

            do
            {
                if (m_LexScanner.NextToken() == TTokens.ANDBITOP)
                {
                    m_LexScanner.Match(TTokens.ANDBITOP);

                    m_CodeGen.AddInstruction(m_MachineCodeOps.ByteOperations.MovWF(
                                                                (Byte)m_TempStoreMemoryLocation++));
                    EqualityExpression(ref Operator);
                
                    m_CodeGen.AddInstruction(m_MachineCodeOps.ByteOperations.AndWF(TRegisters.WORKING_REGISTER,
                            Convert.ToByte(--m_TempStoreMemoryLocation))); 
                }
            } while (m_LexScanner.NextToken() == TTokens.ANDBITOP);
            return Identifier;
        }

        /// <summary>
        /// 
        /// </summary>
        public String EqualityExpression(ref TTokens Operator)
        {
            String Identifier = RelationalExpression(ref Operator);

            do
            {
                if (m_LexScanner.NextToken() == TTokens.EQUALOP)
                {
                    m_LexScanner.Match(TTokens.EQUALOP);

                    Operator = TTokens.EQUALOP;

                    m_CodeGen.AddInstruction(m_MachineCodeOps.ByteOperations.MovWF(
                                                                       (Byte)m_TempStoreMemoryLocation++));
                    RelationalExpression(ref Operator);
                    m_CodeGen.AddInstruction(m_MachineCodeOps.ByteOperations.SubWF(TRegisters.WORKING_REGISTER,
                            Convert.ToByte(--m_TempStoreMemoryLocation))); 
                }
                else if (m_LexScanner.NextToken() == TTokens.NOTEQLOP)
                {
                    m_LexScanner.Match(TTokens.NOTEQLOP);
                    m_CodeGen.AddInstruction(m_MachineCodeOps.ByteOperations.MovWF(
                                                                       (Byte)m_TempStoreMemoryLocation++));
                    RelationalExpression(ref Operator);
                    m_CodeGen.AddInstruction(m_MachineCodeOps.ByteOperations.SubWF(TRegisters.WORKING_REGISTER,
                            Convert.ToByte(--m_TempStoreMemoryLocation))); 
                    Operator = TTokens.NOTEQLOP;
                }
            } while (m_LexScanner.NextToken() == TTokens.EQUALOP ||
                                            m_LexScanner.NextToken() == TTokens.NOTEQLOP);
            return Identifier;
        }

        /// <summary>
        /// 
        /// </summary>
        public String RelationalExpression(ref TTokens Operator)
        {
            String Identifier = ShiftExpression();

            do
            {
                if (m_LexScanner.NextToken() == TTokens.LTOP)
                {
                    m_LexScanner.Match(TTokens.LTOP);
                    
                    m_CodeGen.AddInstruction(m_MachineCodeOps.ByteOperations.MovWF(
                                                   (Byte)m_TempStoreMemoryLocation++));
                    
                    ShiftExpression();
                    
                    m_CodeGen.AddInstruction(m_MachineCodeOps.ByteOperations.SubWF(TRegisters.WORKING_REGISTER,
                            Convert.ToByte(--m_TempStoreMemoryLocation))); 
                    
                    Operator = TTokens.LTOP;
                }
                else if (m_LexScanner.NextToken() == TTokens.GTOP)
                {
                    m_LexScanner.Match(TTokens.GTOP);
                    m_CodeGen.AddInstruction(m_MachineCodeOps.ByteOperations.MovWF(
                                                   (Byte)m_TempStoreMemoryLocation++));

                    ShiftExpression();

                    m_CodeGen.AddInstruction(m_MachineCodeOps.ByteOperations.SubWF(TRegisters.WORKING_REGISTER,
                            Convert.ToByte(--m_TempStoreMemoryLocation))); 
                    
                    Operator = TTokens.GTOP;
                }
                else if (m_LexScanner.NextToken() == TTokens.LTEOP)
                {
                    m_LexScanner.Match(TTokens.LTEOP);
                    m_CodeGen.AddInstruction(m_MachineCodeOps.ByteOperations.MovWF(
                                                   (Byte)m_TempStoreMemoryLocation++));

                    ShiftExpression();

                    m_CodeGen.AddInstruction(m_MachineCodeOps.ByteOperations.SubWF(TRegisters.WORKING_REGISTER,
                            Convert.ToByte(--m_TempStoreMemoryLocation))); 
                    
                    Operator = TTokens.LTEOP;
                }
                else if (m_LexScanner.NextToken() == TTokens.GTEOP)
                {
                    m_LexScanner.Match(TTokens.GTEOP);
                    m_CodeGen.AddInstruction(m_MachineCodeOps.ByteOperations.MovWF(
                                                   (Byte)m_TempStoreMemoryLocation++));

                    ShiftExpression();

                    m_CodeGen.AddInstruction(m_MachineCodeOps.ByteOperations.SubWF(TRegisters.WORKING_REGISTER,
                            Convert.ToByte(--m_TempStoreMemoryLocation)));
                    
                    Operator = TTokens.GTEOP;
                }
            } while (m_LexScanner.NextToken() == TTokens.LTOP || m_LexScanner.NextToken() == TTokens.GTOP
                                || m_LexScanner.NextToken() == TTokens.LTEOP ||
                                            m_LexScanner.NextToken() == TTokens.GTEOP);
            return Identifier;
        }

        /// <summary>
        /// 
        /// </summary>
        public String ShiftExpression()
        {
            // The value from this method is stored in the working 
            // register.
            String Identifier = AdditionExpression();
            CVariable IdentiferInfo = null;
            UInt16 StartLoopAddress = 0;

            do
            {
                if (m_LexScanner.NextToken() == TTokens.LSHIFTOP)
                {
                    m_LexScanner.Match(TTokens.LSHIFTOP);

                    m_CodeGen.AddInstruction(m_MachineCodeOps.BitOperations.Bcf(TBits.BIT0, 3));

                    // store the working registers value.
                    m_CodeGen.AddInstruction(m_MachineCodeOps.ByteOperations.MovWF(
                                                                (Byte)m_TempStoreMemoryLocation++));

                    AdditionExpression();

                    IdentiferInfo = (CVariable)m_SymbolTable[Identifier];
                    if (IsInSecondBank(Identifier) == true)
                        m_CodeGen.AddInstruction(m_MachineCodeOps.BitOperations.Bsf(TBits.BIT5, 3));

                    m_CodeGen.AddInstruction(m_MachineCodeOps.ByteOperations.MovWF((Byte)m_TempStoreBuffer));

                    m_CodeGen.AddInstruction(m_MachineCodeOps.ByteOperations.IncF(TRegisters.FILE_REGISTER,
                                                                        (Byte)m_TempStoreBuffer));

                    StartLoopAddress = (Byte)m_CodeGen.CurrentCodeMemoryLocation;

                    m_CodeGen.AddInstruction(m_MachineCodeOps.ByteOperations.DecF(TRegisters.FILE_REGISTER,
                                                                    (Byte)m_TempStoreBuffer));

                    m_CodeGen.AddInstruction(m_MachineCodeOps.BitOperations.Btfsc(TBits.BIT2, 3));
                    m_CodeGen.AddInstruction(m_MachineCodeOps.ControlOperations.Goto((Byte)(StartLoopAddress + 5)));

                    

                    m_CodeGen.AddInstruction(m_MachineCodeOps.ByteOperations.RlF(TRegisters.FILE_REGISTER,
                                                                      (Byte)IdentiferInfo.MemoryAddress));

                    m_CodeGen.AddInstruction(m_MachineCodeOps.ControlOperations.Goto((Byte)StartLoopAddress));
                    if (IsInSecondBank(Identifier) == true)
                        m_CodeGen.AddInstruction(m_MachineCodeOps.BitOperations.Bcf(TBits.BIT5, 3));

                    m_CodeGen.AddInstruction(m_MachineCodeOps.ByteOperations.MovF(TRegisters.WORKING_REGISTER,
                                                                            (Byte)IdentiferInfo.MemoryAddress));
                }
                else if (m_LexScanner.NextToken() == TTokens.RSHIFTOP)
                {
                    m_LexScanner.Match(TTokens.RSHIFTOP);

                    m_CodeGen.AddInstruction(m_MachineCodeOps.ByteOperations.MovWF(
                                                                (Byte)m_TempStoreMemoryLocation++));

                    AdditionExpression();

                    IdentiferInfo = (CVariable)m_SymbolTable[Identifier];
                    if (IsInSecondBank(Identifier) == true)
                        m_CodeGen.AddInstruction(m_MachineCodeOps.BitOperations.Bsf(TBits.BIT5, 3));

                    m_CodeGen.AddInstruction(m_MachineCodeOps.ByteOperations.MovWF((Byte)m_TempStoreBuffer));

                    m_CodeGen.AddInstruction(m_MachineCodeOps.ByteOperations.IncF(TRegisters.FILE_REGISTER,
                                                                        (Byte)m_TempStoreBuffer));

                    StartLoopAddress = (Byte)m_CodeGen.CurrentCodeMemoryLocation;

                    m_CodeGen.AddInstruction(m_MachineCodeOps.ByteOperations.DecF(TRegisters.FILE_REGISTER,
                                                                    (Byte)m_TempStoreBuffer));

                    m_CodeGen.AddInstruction(m_MachineCodeOps.BitOperations.Btfsc(TBits.BIT2, 3));
                    m_CodeGen.AddInstruction(m_MachineCodeOps.ControlOperations.Goto((Byte)(StartLoopAddress + 5)));



                    m_CodeGen.AddInstruction(m_MachineCodeOps.ByteOperations.RrF(TRegisters.FILE_REGISTER,
                                                                      (Byte)IdentiferInfo.MemoryAddress));

                    m_CodeGen.AddInstruction(m_MachineCodeOps.ControlOperations.Goto((Byte)StartLoopAddress));
                    if (IsInSecondBank(Identifier) == true)
                        m_CodeGen.AddInstruction(m_MachineCodeOps.BitOperations.Bcf(TBits.BIT5, 3));

                    m_CodeGen.AddInstruction(m_MachineCodeOps.ByteOperations.MovF(TRegisters.WORKING_REGISTER,
                                                                            (Byte)IdentiferInfo.MemoryAddress));
                }
            } while (m_LexScanner.NextToken() == TTokens.LSHIFTOP ||
                                            m_LexScanner.NextToken() == TTokens.RSHIFTOP);
            return Identifier;
        }

        /// <summary>
        /// 
        /// </summary>
        public String AdditionExpression()
        {

            String LOperand = m_LexScanner.TokenValue;
            String ROperand = "";

            MultiplicationExpression();

            do
            {
                if (m_LexScanner.NextToken() == TTokens.ADDOP)
                {
                    m_LexScanner.Match(TTokens.ADDOP);

                    // I know MultiplicationExpression() will store
                    // its result in the Working Register. So it needs
                    // to be saved so the working register can be used
                    // again.
                       // use the temp variable to store the value stored in W register because there is no
                    // stock to store variables.
                    m_CodeGen.AddInstruction(m_MachineCodeOps.ByteOperations.MovWF(
                                                                (Byte)m_TempStoreMemoryLocation++));

                    // Get the right operand.
                    ROperand = MultiplicationExpression();
                    // This value is stored in the working register.
    
                    m_CodeGen.AddInstruction(m_MachineCodeOps.ByteOperations.AddWF(TRegisters.WORKING_REGISTER,
                            Convert.ToByte(--m_TempStoreMemoryLocation))); 
                }
                else if (m_LexScanner.NextToken() == TTokens.SUBOP)
                {
                    m_LexScanner.Match(TTokens.SUBOP);
                    // I know MultiplicationExpression() will store
                    // its result in the Working Register. So it needs
                    // to be saved so the working register can be used
                    // again.
                    // use the temp variable to store the value stored in W register because there is no
                    // stock to store variables.
                    m_CodeGen.AddInstruction(m_MachineCodeOps.ByteOperations.MovWF(
                                                                        (Byte)m_TempStoreMemoryLocation++));
                    ROperand = MultiplicationExpression();
                    m_CodeGen.AddInstruction(m_MachineCodeOps.ByteOperations.SubWF(TRegisters.WORKING_REGISTER,
                            Convert.ToByte(--m_TempStoreMemoryLocation))); 
                }
            } while (m_LexScanner.NextToken() == TTokens.ADDOP || m_LexScanner.NextToken() == TTokens.SUBOP);

            return LOperand;
        }

        /// <summary>
        /// Multiples the operands and stores the result in the 
        /// working register.
        /// </summary>
        public String MultiplicationExpression()
        {
            String TokenType = UnaryExpression();

            Int16 StartLoopAddress = 0;
        
            do
            {
                if (m_LexScanner.NextToken() == TTokens.MULOP)
                {
                    m_LexScanner.Match(TTokens.MULOP);

                    // Push the left operand onto the stack.
                    m_CodeGen.AddInstruction(m_MachineCodeOps.ByteOperations.MovWF((Byte)m_TempStoreMemoryLocation));

                    TokenType = UnaryExpression();

                    // There is no multiplication operator for the PIC
                    // So I gotta do a series of adds.
                    // move the second operand to m_TempStoreBuffer
                    m_CodeGen.AddInstruction(m_MachineCodeOps.ByteOperations.MovWF((Byte)m_TempStoreBuffer));

                    // Clear the working register
                    m_CodeGen.AddInstruction(m_MachineCodeOps.ControlOperations.MovLW(0));

                    m_CodeGen.AddInstruction(m_MachineCodeOps.ByteOperations.IncF(TRegisters.FILE_REGISTER,
                                                                        (Byte)m_TempStoreBuffer));

                    StartLoopAddress = m_CodeGen.CurrentCodeMemoryLocation;
                    m_CodeGen.AddInstruction(m_MachineCodeOps.ByteOperations.DecF(TRegisters.FILE_REGISTER,
                                                                    (Byte)m_TempStoreBuffer));
                    m_CodeGen.AddInstruction(m_MachineCodeOps.BitOperations.Btfsc(TBits.BIT2, 3));
                    m_CodeGen.AddInstruction(m_MachineCodeOps.ControlOperations.Goto((Byte)(StartLoopAddress+5)));

                    // move the first operand to the working register.
                    m_CodeGen.AddInstruction(m_MachineCodeOps.ByteOperations.AddWF(TRegisters.WORKING_REGISTER,
                             Convert.ToByte(m_TempStoreMemoryLocation)));

                    m_CodeGen.AddInstruction(m_MachineCodeOps.ControlOperations.Goto(StartLoopAddress));
                }
                else if (m_LexScanner.NextToken() == TTokens.DIVOP)
                {
                    m_LexScanner.Match(TTokens.DIVOP);

                    m_CodeGen.AddInstruction(m_MachineCodeOps.ByteOperations.MovWF((Byte)m_TempStoreMemoryLocation));
                    
                    // clear the temp buffer.
                    m_CodeGen.AddInstruction(m_MachineCodeOps.ControlOperations.MovLW(0));
                    m_CodeGen.AddInstruction(m_MachineCodeOps.ByteOperations.MovWF((Byte)m_TempStoreBuffer)); 

                    UnaryExpression();
                    
                    StartLoopAddress = m_CodeGen.CurrentCodeMemoryLocation;

                    m_CodeGen.AddInstruction(m_MachineCodeOps.ByteOperations.SubWF(TRegisters.FILE_REGISTER,
                                                                            (Byte)m_TempStoreMemoryLocation));
                    m_CodeGen.AddInstruction(m_MachineCodeOps.BitOperations.Btfsc(TBits.BIT2, 3));
                    m_CodeGen.AddInstruction(m_MachineCodeOps.ControlOperations.Goto((Byte)(StartLoopAddress + 6)));

                    // check the carry flag.
                    m_CodeGen.AddInstruction(m_MachineCodeOps.BitOperations.Btfss(TBits.BIT0, 3));
                    m_CodeGen.AddInstruction(m_MachineCodeOps.ControlOperations.Goto((Byte)(StartLoopAddress + 7)));

                    m_CodeGen.AddInstruction(m_MachineCodeOps.ByteOperations.IncF(TRegisters.FILE_REGISTER,
                                                                            (Byte)m_TempStoreBuffer));
                    m_CodeGen.AddInstruction(m_MachineCodeOps.ControlOperations.Goto(StartLoopAddress));

                    m_CodeGen.AddInstruction(m_MachineCodeOps.ByteOperations.MovF(TRegisters.WORKING_REGISTER,
                                                                            (Byte)m_TempStoreBuffer));
                }
            } while (m_LexScanner.NextToken() == TTokens.MULOP || m_LexScanner.NextToken() == TTokens.DIVOP);

            // add while loop to create complex expression.
            return TokenType;
        }

        /// <summary>
        /// Generate code for pre increment/decrement operations.
        /// </summary>
        public String UnaryExpression()
        {
            if (m_LexScanner.NextToken() == TTokens.INCOP)
            {
                m_LexScanner.Match(TTokens.INCOP);
                return PostfixExpression();

                // Generate some code here..
            }
            else if (m_LexScanner.NextToken() == TTokens.DECOP)
            {
                m_LexScanner.Match(TTokens.DECOP);
                return PostfixExpression();

                // generate some code here
            }
            else
                return PostfixExpression();
        }

        /// <summary>
        /// Move literals to the working register and
        /// moves the values stored in variables to the 
        /// working register.
        /// </summary>
        public String PostfixExpression()
        {
            CVariable T = null;
            m_BitLevelManipulation = false;

            if (m_LexScanner.NextToken() == TTokens.IDENTIFIER)
            {
                m_LexScanner.Match(TTokens.IDENTIFIER);
                T = (CVariable)m_SymbolTable[m_LexScanner.TokenValue];

                // is the user trying some bit level operations??
                if (m_LexScanner.NextToken() == TTokens.LBRACKET)
                {
                    String Literal = "";
                    Int16 Res = 0;
                    
                    // the user is indexing a variable.

                    m_LexScanner.Match(TTokens.LBRACKET);

                    Literal = Expression(ref m_CurrentOperator);

                    if (Int16.TryParse(Literal, out Res) == false)
                        throw new CCompilerException("Only literals can be used in this situation.", 
                                                    m_LexScanner.LineNumber, TErrorCodes.COMPILER_ERROR);

                    if (Res < 0 || Res > 7)
                        throw new CCompilerException("The variable index must be a literal value between 0 and 7",
                            m_LexScanner.LineNumber, TErrorCodes.COMPILER_ERROR);


                    if (m_LexScanner.Match(TTokens.RBRACKET) == false)
                        throw new CCompilerException("] expected.", m_LexScanner.LineNumber, 
                                                                        TErrorCodes.PARSER_ERROR);

                    m_VariableIndex = (Byte)Res;
                    m_BitLevelManipulation = true;
                    IsBitSet((Byte)T.MemoryAddress, Res);
                }
                else
                {
                    // Move the value stored in the variable to the Working register.
                    if (IsInSecondBank(m_LexScanner.TokenValue) == true)
                        m_CodeGen.AddInstruction(m_MachineCodeOps.BitOperations.Bsf(TBits.BIT5, 3));

                    m_CodeGen.AddInstruction(m_MachineCodeOps.ByteOperations.MovF(TRegisters.WORKING_REGISTER,
                                                                                        (Byte)T.MemoryAddress));

                    if (IsInSecondBank(m_LexScanner.TokenValue) == true)
                        m_CodeGen.AddInstruction(m_MachineCodeOps.BitOperations.Bcf(TBits.BIT5, 3));

                    if (m_LexScanner.NextToken() == TTokens.INCOP)
                    {
                        m_LexScanner.Match(TTokens.INCOP);

                        // Increment the file register.
                        m_CodeGen.AddInstruction(m_MachineCodeOps.ByteOperations.IncF(TRegisters.FILE_REGISTER,
                                                                            (Byte)T.MemoryAddress));
                    }
                    else if (m_LexScanner.NextToken() == TTokens.DECOP)
                    {
                        m_LexScanner.Match(TTokens.DECOP);
                        // generate some code here.
                        m_CodeGen.AddInstruction(m_MachineCodeOps.ByteOperations.DecF(TRegisters.FILE_REGISTER,
                                                                            (Byte)T.MemoryAddress));
                    }
                }
                // return a Variable name.
                return m_LexScanner.TokenValue;
            }
            else if (m_LexScanner.NextToken() == TTokens.LITERAL)
            {
                m_LexScanner.Match(TTokens.LITERAL);

                m_CodeGen.AddInstruction(m_MachineCodeOps.ControlOperations.MovLW(Convert.ToByte(m_LexScanner.TokenValue)));
                return m_LexScanner.TokenValue;
            }
            else if (m_LexScanner.NextToken() == TTokens.LPAREN)
            {
                m_LexScanner.Match(TTokens.LPAREN);
                Expression(ref m_CurrentOperator);
                if (m_LexScanner.Match(TTokens.RPAREN) == false)
                    throw new CCompilerException(") expected.", m_LexScanner.LineNumber,
                        TErrorCodes.PARSER_ERROR);
                m_BitLevelManipulation = true;

                // return the result of the expression.
                return "";
            }
            return "";
        }

        /// <summary>
        /// 
        /// </summary>
        public void ParameterList(CFunction FunctionDefinition)
        {
            ParameterDeclaration(FunctionDefinition);

            while (m_LexScanner.NextToken() == TTokens.COMMA)
            {
                m_LexScanner.Match(TTokens.COMMA);
                ParameterDeclaration(FunctionDefinition);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void ParameterDeclaration(CFunction FunctionDefinition)
        {
            String Argument = "";
            CVariable NewIdentifier = null;
            
            TypeSpecifier();
            
            Argument = Declarator(FunctionDefinition);

            NewIdentifier = new CVariable(Argument, TDataType.INTEGER,
                                                                m_LexScanner.ScopeLevelCount, 0);
            FunctionDefinition.AddParameter(Argument);
            m_SymbolTable.Add(Argument, NewIdentifier);
        }

        /// <summary>
        /// Created 22/02/07
        /// </summary>
        public void IfStatement()
        {
            UInt16 JumpOp = 0;
            int CurrentRecordIndex = 0;
            int CurrentOpIndex = 0;

            m_LexScanner.Match(TTokens.IF);
            if (m_LexScanner.Match(TTokens.LPAREN) == false)
                throw new CCompilerException("( Expected.", m_LexScanner.LineNumber,
                    TErrorCodes.PARSER_ERROR);

            Expression(ref m_CurrentOperator);

            /* Generate the jump statement */
            BuildBitTestInstruction(m_CurrentOperator);
            JumpOp = m_MachineCodeOps.ControlOperations.Goto(0);
            m_CodeGen.AddInstruction(JumpOp);

            // get the index for the current hex record and op code
            CurrentRecordIndex = m_CodeGen.CurrentHexRecordIndex - 1;
            CurrentOpIndex = m_CodeGen.CurrentOpCodeIndex - 1;

            if (m_LexScanner.Match(TTokens.RPAREN) == false)
                throw new CCompilerException(") Expected.", m_LexScanner.LineNumber,
                    TErrorCodes.PARSER_ERROR);

            Statement();
            JumpOp = m_MachineCodeOps.ControlOperations.Goto(m_CodeGen.CurrentCodeMemoryLocation);

            /** Modify the Goto statement **/
            m_CodeGen.ModifyOpCode(CurrentRecordIndex, CurrentOpIndex, JumpOp);
            m_CodeGen.AddInstruction(m_MachineCodeOps.ByteOperations.Nop());
        }

        /// <summary>
        /// 
        /// </summary>
        public void ProcessWhileLoop()
        {
            UInt16 JumpOp = 0;
            int CurrentRecordIndex = 0;
            int CurrentOpIndex = 0;
            UInt16 StartOfLoopAddress = 0;

            m_LexScanner.Match(TTokens.WHILE);
            if (m_LexScanner.Match(TTokens.LPAREN) == false)
                throw new CCompilerException("( Expected.", m_LexScanner.LineNumber,
                    TErrorCodes.PARSER_ERROR);


            // before the loop starts clear the Zero flag.

            StartOfLoopAddress = (Byte)m_CodeGen.CurrentCodeMemoryLocation;

            Expression(ref m_CurrentOperator);

            /* Generate the jump statement */
            BuildBitTestInstruction(m_CurrentOperator);
            JumpOp = m_MachineCodeOps.ControlOperations.Goto(0);
            m_CodeGen.AddInstruction(JumpOp);

            // get the index for the current hex record and op code
            CurrentRecordIndex = m_CodeGen.CurrentHexRecordIndex - 1;
            CurrentOpIndex = m_CodeGen.CurrentOpCodeIndex - 1;

            
            if (m_LexScanner.Match(TTokens.RPAREN) == false)
                throw new CCompilerException(") Expected.", m_LexScanner.LineNumber,
                    TErrorCodes.PARSER_ERROR);
            Statement();
            m_CodeGen.AddInstruction(m_MachineCodeOps.ControlOperations.Goto((Byte)StartOfLoopAddress));

            JumpOp = m_MachineCodeOps.ControlOperations.Goto(m_CodeGen.CurrentCodeMemoryLocation);

            /** Modify the Goto statement **/
            m_CodeGen.ModifyOpCode(CurrentRecordIndex, CurrentOpIndex, JumpOp);
            m_CodeGen.AddInstruction(m_MachineCodeOps.ByteOperations.Nop());
        }


        /// <summary>
        /// Checks to see if CurrentToken is a data type.
        /// </summary>
        /// <param name="CurrentToken">The token to be compared with the datatype tokens.</param>
        /// <returns>True if CurrentToken is a datatype. Otherwise returns false.</returns>
        private Boolean IsDataType(TTokens CurrentToken)
        {
            switch (CurrentToken)
            {
                case TTokens.INT:
                case TTokens.SIGNED:
                case TTokens.UNSIGNED:
                case TTokens.VOID:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="FunctionData"></param>
        public void ProcessFunctionCall(CFunction FunctionData)
        {
            int ParamCounter = 0;
            String CurrentParameter = "";
            TTokens DummyData = TTokens.NONE;
            CVariable Parameter = null;

            m_LexScanner.Match(TTokens.IDENTIFIER);

            if (m_LexScanner.Match(TTokens.LPAREN) == false)
                throw new CCompilerException("( Expected for function calls.", m_LexScanner.LineNumber,
                    TErrorCodes.PARSER_ERROR);

            /** process parameters **/
            while (ParamCounter < FunctionData.ParameterCount)
            {
                // generate code similar to an assigment because
                // that is what is happening effectivly;
                AssignmentExpression(ref m_CurrentOperator);

                // assignment code.
                CurrentParameter = FunctionData.GetParameter(ParamCounter);
                Parameter = (CVariable) m_SymbolTable[CurrentParameter];

                m_CodeGen.AddInstruction(m_MachineCodeOps.ByteOperations.MovWF((Byte)Parameter.MemoryAddress));

                ++ParamCounter;

                if(ParamCounter < FunctionData.ParameterCount)
                    if (m_LexScanner.Match(TTokens.COMMA) == false)
                        throw new CCompilerException("Hold on a second, I am expecting a ,", m_LexScanner.LineNumber,
                                                    TErrorCodes.PARSER_ERROR);
            }

            if (m_LexScanner.NextToken() == TTokens.RPAREN)
            {
                m_LexScanner.Match(TTokens.RPAREN);
                m_CodeGen.AddInstruction(m_MachineCodeOps.ControlOperations.Call((Byte)FunctionData.MemoryAddress));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void ProcessDoWhileLoop()
        {
            UInt16 StartOfLoopAddress = 0;

            m_LexScanner.Match(TTokens.DOWHILE);
            
            StartOfLoopAddress = (Byte)m_CodeGen.CurrentCodeMemoryLocation;
            Statement();

            if (m_LexScanner.Match(TTokens.WHILE) == false)
                throw new CCompilerException("Keyword while expected.", m_LexScanner.LineNumber, 
                                                                            TErrorCodes.PARSER_ERROR);
            if (m_LexScanner.Match(TTokens.LPAREN) == false)
                throw new CCompilerException("( Expected.", m_LexScanner.LineNumber,
                    TErrorCodes.PARSER_ERROR);
            Expression(ref m_CurrentOperator);
            if (m_LexScanner.Match(TTokens.RPAREN) == false)
                throw new CCompilerException(") Expected.", m_LexScanner.LineNumber,
                    TErrorCodes.PARSER_ERROR);

            /* Generate the jump statement */
            BuildBitTestInstruction(m_CurrentOperator);
            m_CodeGen.AddInstruction(m_MachineCodeOps.ControlOperations.Goto((Byte)StartOfLoopAddress));
        }

        /// <summary>
        /// Checks to see if the variable name is in the second memory bank.
        /// Returns true if the variable is. If the function returns true
        /// a call to bsf should be called to access it.
        /// </summary>
        /// <param name="VariableName"></param>
        /// <returns>true if the variable belongs in the second bank.</returns>
        public Boolean IsInSecondBank(String VariableName)
        {
            switch (VariableName)
            {
                case "TRISA":
                case "TRISB":
                case "OPTION":
                case "EECON1":
                case "EECON2":
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// If the bit in address is set then the working register
        /// is set to 1 otherwise it is set to 0.
        /// </summary>
        /// <param name="Address">The variable to check</param>
        public void IsBitSet(Int16 Address, Int16 Bit)
        {
            //
            // clear the working register.
            m_CodeGen.AddInstruction(m_MachineCodeOps.ControlOperations.MovLW(0));

            m_CodeGen.AddInstruction(m_MachineCodeOps.BitOperations.Btfsc((TBits)Bit, (Byte)Address));
            m_CodeGen.AddInstruction(m_MachineCodeOps.ControlOperations.MovLW(1));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Operator"></param>
        public void BuildBitTestInstruction(TTokens Operator)
        {
            switch (Operator)
            {
                case TTokens.EQUALOP:
                    m_CodeGen.AddInstruction(m_MachineCodeOps.BitOperations.Btfss(TBits.BIT2, 0x03));
                    break;
                case TTokens.NOTEQLOP:
                    m_CodeGen.AddInstruction(m_MachineCodeOps.BitOperations.Btfsc(TBits.BIT2, 0x03));
                    break;
                case TTokens.GTOP:
                    // check carry is 0 and zero is 0
                    m_CodeGen.AddInstruction(m_MachineCodeOps.BitOperations.Btfss(TBits.BIT0, 0x03));
                    m_CodeGen.AddInstruction(m_MachineCodeOps.ControlOperations.Goto(
                                                        (Byte)(m_CodeGen.CurrentCodeMemoryLocation + 2)));
                    m_CodeGen.AddInstruction(m_MachineCodeOps.BitOperations.Btfsc(TBits.BIT2, 0x03));
                    break;
                case TTokens.GTEOP:
                    // check carry is 0 OR zero is 1
                    m_CodeGen.AddInstruction(m_MachineCodeOps.BitOperations.Btfsc(TBits.BIT0, 0x03));
                    m_CodeGen.AddInstruction(m_MachineCodeOps.ControlOperations.Goto(
                                                        (Byte)(m_CodeGen.CurrentCodeMemoryLocation + 3)));
                    m_CodeGen.AddInstruction(m_MachineCodeOps.BitOperations.Btfss(TBits.BIT2, 0x03));
                    break;
                case TTokens.LTOP:
                    // check carry is 1 and zero 0
                    m_CodeGen.AddInstruction(m_MachineCodeOps.BitOperations.Btfsc(TBits.BIT0, 0x03));
                    m_CodeGen.AddInstruction(m_MachineCodeOps.ControlOperations.Goto(
                                                        (Byte)(m_CodeGen.CurrentCodeMemoryLocation + 2)));
                    m_CodeGen.AddInstruction(m_MachineCodeOps.BitOperations.Btfsc(TBits.BIT2, 0x03));
                    break;
                case TTokens.LTEOP:
                    // check carry is 1 or zero 1
                    m_CodeGen.AddInstruction(m_MachineCodeOps.BitOperations.Btfss(TBits.BIT0, 0x03));
                    m_CodeGen.AddInstruction(m_MachineCodeOps.ControlOperations.Goto(
                                                        (Byte)(m_CodeGen.CurrentCodeMemoryLocation + 3)));
                    m_CodeGen.AddInstruction(m_MachineCodeOps.BitOperations.Btfss(TBits.BIT2, 0x03));
                    break;
            }
        }

        /// <summary>
        /// A reference to the lexical scanner passed to the constructor.
        /// </summary>
        private CLexScanner m_LexScanner = null;

        /// <summary>
        /// A reference to the symbol table passed to the constructor.
        /// The symbol table is used to store information about variables 
        /// and functions.
        /// </summary>
        private CSymbolTable m_SymbolTable = null;

        /// <summary>
        /// Stores the name of the current function being parsed.
        /// This value is used when a Key value is created for the symbol
        /// table.
        /// </summary>
        private String m_CurrentFunction = "";

        /// <summary>
        /// 
        /// </summary>
        private CMachineCodes m_MachineCodeOps = null;

        /// <summary>
        /// 
        /// </summary>
        private CPicCodeGenerator m_CodeGen = null;

        /// <summary>
        /// This is used to hold the values when I am swapping the valued
        /// stored in the working register with one of the values on the stack.
        /// 
        /// The problem is, when an expression such as 5 + 4 is encountered the 
        /// following happens.
        /// 
        /// 1. 5 is moved to the working register
        /// 2. 5 is moved to the TempStoreMemoryLocation
        /// 3. 4 is moved to the working register.
        /// 4. Then 5 is added to 4 when it should be 4 is added to 5.
        /// 
        /// </summary>
        private Int16 m_TempStoreBuffer = 0x0C;

        /// <summary>
        /// This is the start address of my very simiple Stack.
        /// </summary>
        private Int16 m_TempStoreMemoryLocation = 0x0D;

        /// <summary>
        /// This variable stores the index used to index the scalar variables.
        /// </summary>
        private Byte m_VariableIndex = 0;

        /// <summary>
        /// Used to indicate whether variable indexing is being used.
        /// </summary>
        private Boolean m_BitLevelManipulation = false;

        /// <summary>
        /// This variable is used to find out which operator was 
        /// used in a expression when comparing values. This value
        /// is need so the compiler can check which flag to check for
        /// changes.
        /// </summary>
        private TTokens m_CurrentOperator;
    }
}