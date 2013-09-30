using System;

namespace Compiler
{
    /// <summary>
    /// REGISTERS is used to specify either the Working register or the file register.
    /// </summary>
    public enum TRegisters {WORKING_REGISTER, FILE_REGISTER };
    
    /// <summary>
    /// 
    /// </summary>
    public enum TBits { BIT0, BIT1, BIT2, BIT3, BIT4, BIT5, BIT6, BIT7 };

    /// <summary>
    /// The error codes returned in the CLexicalException Class.
    /// </summary>
    public enum TErrorCodes { UNAUTHORISED_ACCESS, SECURITY_ERROR, FILE_ACCESS_ERR,
                              LEXICAL_ERROR, PARSER_ERROR, COMPILER_ERROR
                            };
    
    /// <summary>
    /// Keyword: identifies keywords.
    /// IDENTIFIER: idenitfies variables and function names.
    /// LITERAL: identifies const literal values.
    /// LPAREN: left parenthesis - (
    /// RPAREN: right parentheis - )
    /// LCURLY: left curly brace - {
    /// RCURLY: right curly brace - }
    /// LBRCAKET: left square bracket - [
    /// RBRACKET: right square bracket - ]
    /// LSHIFTOP: bitwise left shift operator - &lt;&lt;
    /// RSHIFTOP: bitwise right shift operatpr - &gt;&gt;
    /// ANDOP: bitwise and operator - &amp;&amp;
    /// OROP: bitwise or operator - |
    /// XOROP: bitwise exclusive or operator - ^
    /// NOTOP: relational operator - !
    /// NOTEQLOP: relational operator - !=
    /// EQUALOP: relational operator - ==
    /// LTOP: &lt;
    /// RTOP &gt;
    /// GTEOP
    /// INCOP
    /// DECOP
    /// ADDOP
    /// SUBOP
    /// DIVOP
    /// MULOP 
    /// COMMA 
    /// SEMICOL 
    /// ASSIGNMENT
    /// 
    /// Modified [13/12/06] KEYWORD has been removed and constants
    /// for each of the keywords have been added to make parsing
    /// easier.
    /// </summary>
    public enum TTokens {NONE, IDENTIFIER, LITERAL, LPAREN, RPAREN, 
                        LCURLY, RCURLY, LBRACKET, RBRACKET, LSHIFTOP,
                        RSHIFTOP, ANDOP, OROP, XOROP, NOTOP, NOTEQLOP,
                        EQUALOP, LTOP, RTOP, LTEOP, GTOP, GTEOP, INCOP, DECOP,
                        ADDOP, SUBOP, DIVOP, MULOP, ANDBITOP, ORBITOP,
                        COMMA, SEMICOL, ASSIGNMENT, RETURN, 
                        VOID, INT, IF, WHILE, SIGNED, UNSIGNED, EOF, DOWHILE
                       };

    /// <summary>
    /// 
    /// </summary>
    public enum TIdentiferType { VARIABLE, FUNCTION };

    /// <summary>
    /// 
    /// </summary>
    public enum TDataType { INTEGER, VOID };

    public enum PIC_PROCESSOR { PIC16F84 };
}