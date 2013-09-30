using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Compiler
{
    public class CExpressionEvaluator
    {
        public CExpressionEvaluator(String InfixExpression)
        {
            m_PostfixEvalStack = new Stack();
            m_InfixToPostfix = new CInToPostfix();

            EvaluateExpression(m_InfixToPostfix.ConvertToPostfix(new StringBuilder(InfixExpression)));
        }

        public void EvaluateExpression(String PostfixExpression)
        {
            int StrIndex = 0;

            // make sure the stack is empty before starting to evaluation
            // the expression.
            m_PostfixEvalStack.Clear(); 

#if DEBUG
            Console.WriteLine("\tClrWF");
#endif

            while (StrIndex < PostfixExpression.Length)
            {
                Char T = PostfixExpression[StrIndex];

                if (Char.IsWhiteSpace(PostfixExpression[StrIndex]) == false)
                {
                    if (Char.IsDigit(PostfixExpression[StrIndex]) == true)
                        m_PostfixEvalStack.Push(PostfixExpression[StrIndex]);
                    else
                    // start generating machine code.
                    {
                        switch (PostfixExpression[StrIndex])
                        {
                            case '+':
                                if (m_PostfixEvalStack.Peek().ToString() != " ")
                                    Console.WriteLine("\tAddlw " + m_PostfixEvalStack.Pop().ToString());
                                else
                                    m_PostfixEvalStack.Pop();

                                if (m_PostfixEvalStack.Peek().ToString() != " ")
                                    Console.WriteLine("\tAddlw " + m_PostfixEvalStack.Pop().ToString());
                                else
                                    m_PostfixEvalStack.Pop();

                                m_PostfixEvalStack.Push(" ");
                                break;
                            case '-':
                                if (m_PostfixEvalStack.Peek().ToString() != " ")
                                    Console.WriteLine("\tSublw " + m_PostfixEvalStack.Pop().ToString());
                                else
                                    m_PostfixEvalStack.Pop();

                                if (m_PostfixEvalStack.Peek().ToString() != " ")
                                    Console.WriteLine("\tSublw " + m_PostfixEvalStack.Pop().ToString());
                                else
                                    m_PostfixEvalStack.Pop();

                                m_PostfixEvalStack.Push(" ");
                                break;
                            case '*':
                                if (m_PostfixEvalStack.Peek().ToString() != " ")
                                    Console.WriteLine("\tMullw " + m_PostfixEvalStack.Pop().ToString());
                                else
                                    m_PostfixEvalStack.Pop();

                                if (m_PostfixEvalStack.Peek().ToString() != " ")
                                    Console.WriteLine("\tMullw " + m_PostfixEvalStack.Pop().ToString());
                                else
                                    m_PostfixEvalStack.Pop();

                                m_PostfixEvalStack.Push(" ");
                                break;
                            case '/':
                                if (m_PostfixEvalStack.Peek().ToString() != " ")
                                    Console.WriteLine("\tDivlw " + m_PostfixEvalStack.Pop().ToString());
                                else
                                    m_PostfixEvalStack.Pop();

                                if (m_PostfixEvalStack.Peek().ToString() != " ")
                                    Console.WriteLine("\tDivlw " + m_PostfixEvalStack.Pop().ToString());
                                else
                                    m_PostfixEvalStack.Pop();
                                m_PostfixEvalStack.Push(" ");
                                break;
                        }
                    }

                }                

                ++StrIndex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private Stack m_PostfixEvalStack = null;

        /// <summary>
        /// 
        /// </summary>
        private CInToPostfix m_InfixToPostfix = null;
    }
}
