using System;
using System.Collections.Generic;
using System.Text;
using Compiler;

namespace ExpressionTest
{
    class Program
    {
        static void Main(string[] args)
        {
            CExpressionEvaluator Expr = new CExpressionEvaluator("(6 + 2) * 5 - 8 / 4");
            CInToPostfix Expression = new CInToPostfix();
            Console.WriteLine(Expression.ConvertToPostfix(new StringBuilder("(65 + 232) * 543 - 8 / 4223"))); 

        }
    }
}
