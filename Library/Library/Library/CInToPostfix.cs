using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Compiler
{
    public class CInToPostfix
    {
        public CInToPostfix()
        {
            // Add the operators to The Operator Precedence Table.
            // Each operator has a integer value assigned to it
            // The higher the precedence of the operator, the higher
            // the integer value.
            m_OperatorPrecedenceTable = new Dictionary<String, Int32>();

            m_OperatorPrecedenceTable.Add("=", 1);

            // Bit & Boolean Operations
            m_OperatorPrecedenceTable.Add("&", 2);
            m_OperatorPrecedenceTable.Add("^", 2);
            m_OperatorPrecedenceTable.Add("|", 2);
            m_OperatorPrecedenceTable.Add("&&", 2);
            m_OperatorPrecedenceTable.Add("||", 2);      

            // Equality Operator
            m_OperatorPrecedenceTable.Add("==", 3);


            // Relational operators.
            m_OperatorPrecedenceTable.Add("<", 4);
            m_OperatorPrecedenceTable.Add(">", 4);
            m_OperatorPrecedenceTable.Add("<=", 4);
            m_OperatorPrecedenceTable.Add(">=", 4);

            // Shift operators.
            m_OperatorPrecedenceTable.Add("<<", 5);
            m_OperatorPrecedenceTable.Add(">>", 5);
            
            //Arithmetic
            m_OperatorPrecedenceTable.Add("+", 6);
            m_OperatorPrecedenceTable.Add("-", 6);
            
            m_OperatorPrecedenceTable.Add("*", 7);
            m_OperatorPrecedenceTable.Add("/", 7);
        }

        /// <summary>
        /// Infix to Postfix Algorithm
        /// 
        /// 1. Push ( on to the stack.
        /// 2. Append ) to InfixStr
        /// 
        /// 3. While the stack is not empty
        ///   3.1 if current char is Digit Copy to output string.
        ///   3.2 if current char is ( push on to the stack
        ///   3.3 if current char is operator then
        ///      3.3.1 if there is any operators on top of the stack
        ///            pop them while their precedence is >= current char
        ///            and place them in output string.
        ///      3.3.2 Push the current char onto the stack
        ///   3.4 if current char is ( pop all operators from the stack until
        ///       the top element is ) and place the operatrs in the output string.
        ///   3.5 Discard )
        /// </summary>
        /// <param name="InfixStr"></param>
        /// <returns></returns>
        public String ConvertToPostfix(StringBuilder InfixStr)
        {
            // Stores the Postfix expression.
            StringBuilder PostfixExpression = new StringBuilder();
            
            // Builds up number bigger than 9.
            StringBuilder IntegerBuffer = new StringBuilder();
            
            Int32 InfixStrIndex = 0;

            m_ExpressionStack = new Stack();
            m_ExpressionStack.Push('(');

            InfixStr.Append(")"); 


            while (m_ExpressionStack.Count > 0)
            {
                Char T = InfixStr[InfixStrIndex];
                // if Current Char is a Digit place copy it to PostfixExpression.
                if (Char.IsDigit(InfixStr[InfixStrIndex]) == true)
                {
                    IntegerBuffer.Append(InfixStr[InfixStrIndex]);

                    while (Char.IsDigit(InfixStr[InfixStrIndex+1]) == true)
                    {
                        IntegerBuffer.Append(InfixStr[++InfixStrIndex]);
                    }

                    PostfixExpression.Append(IntegerBuffer.ToString());
                    PostfixExpression.Append(" ");
                    IntegerBuffer.Remove(0, IntegerBuffer.Length);
                }
                else if (InfixStr[InfixStrIndex] == '(')
                    m_ExpressionStack.Push(InfixStr[InfixStrIndex]);
                else if (IsOperator(InfixStr[InfixStrIndex].ToString()) == true)
                {
                    while ( (IsOperator(m_ExpressionStack.Peek().ToString()) == true) &&
                        (Precendence(m_ExpressionStack.Peek().ToString(), InfixStr[InfixStrIndex].ToString()) == true))
                            PostfixExpression.Append(m_ExpressionStack.Pop());

                        m_ExpressionStack.Push(InfixStr[InfixStrIndex]);
                }
                else if (InfixStr[InfixStrIndex] == ')')
                {
                   if(IsOperator(m_ExpressionStack.Peek().ToString()) == true)
                   {
                       while(m_ExpressionStack.Peek().ToString() != "(")
                           PostfixExpression.Append(m_ExpressionStack.Pop().ToString());

                        // pop )
                        m_ExpressionStack.Pop();
                   }
                }
                ++InfixStrIndex;
            }


            return PostfixExpression.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Op"></param>
        /// <returns></returns>
        private Boolean IsOperator(String Op)
        {
            return m_OperatorPrecedenceTable.ContainsKey(Op);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Op1"></param>
        /// <param name="Op2"></param>
        /// <returns></returns>
        private Boolean Precendence(String Op1, String Op2)
        {
            return m_OperatorPrecedenceTable[Op1] >= m_OperatorPrecedenceTable[Op2];
        }

        /// <summary>
        /// 
        /// </summary>
        private Stack m_ExpressionStack = null;

        /// <summary>
        /// 
        /// </summary>
        private Dictionary<String, Int32> m_OperatorPrecedenceTable = null;
    }
}
