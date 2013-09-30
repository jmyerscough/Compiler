using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security; 
using System.Xml;
using System.Collections.Specialized;

namespace Compiler
{
    /// <summary>
    /// This class is used to scan the JasC source files and return the token
    /// types to the parser. When an identifier is found it is placed in the 
    /// symbol table. 
    /// The class also reports an Lexical errors found while scanning the input
    /// source.
    /// 
    /// Modified [13/12/06] New function added to the class.
    /// GetKeywordEnumType(String Token); The function assigns the correct enum const
    /// to m_CurrentToken. This helps the parser make predictions.
    /// 
    /// Modified [14/12/06] - New function added.
    /// </summary>
    sealed class CLexScanner
    {
        /// <summary>
        /// Opens the JasC source file and reads the first token from the file.
        /// </summary>
        /// <param name="Filename">The users JasC source filename.</param>
        /// <exception cref="System.FileNotFoundException">Thrown when the JasC source file
        /// is not found.</exception>
        /// <exception cref="Compiler.CLexicalException">Thrown when there is a problem reading from
        /// the JasC source file.</exception>
        public CLexScanner(String Filename, String KeywordFilename)
        {
            if (File.Exists(Filename) == false)
                throw new FileNotFoundException();
            else
            {
                try
                {
                    m_SourceFile = new FileStream(Filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    m_SourceFileStream = new StreamReader(m_SourceFile);
                   
                    BuildKeywordCollection(KeywordFilename);
                    ReadNextToken(); 
                }
                catch (UnauthorizedAccessException)
                {
                    throw new CCompilerException("Unauthorised File Access", TErrorCodes.UNAUTHORISED_ACCESS); 
                }
                catch (ArgumentException)
                {
                    // Throw a LexException.
                }
                catch (SecurityException)
                {
                    // Throw a LexException.
                }
                catch (IOException)
                {
                    // Throw a LexException.
                }
                catch (Exception)
                {
                    // Throw a LexException.
                }
            }
        }

        /// <summary>
        /// If the token passed to the function matches m_CurrentToken the function reads
        /// the next token from the source file and return true.
        /// </summary>
        /// <param name="Token">The token to me matched with m_CurrentToken</param>
        /// <returns>Returns true if <c>Token</c> equals <c>m_CurrentToken</c> else the false
        /// is returned.</returns>
        public Boolean Match(TTokens Token)
        {
            if (Token == m_CurrentToken)
            {
                ReadNextToken();
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Returns the value stored in <c>m_CurrentToken</c>
        /// </summary>
        /// <returns>Returns the current token stored in <c>m_CurrentToken</c></returns>
        public TTokens NextToken()
        {
            return m_CurrentToken; 
        }

        public Int32 LineNumber
        {
            get
            {
                return m_LineCount;
            }
        }

        /// <summary>
        /// Reads the next token from the input stream and stores the tokens
        /// type in m_CurrentToken.
        /// </summary>
        /// <exception cref="Compiler.CLexicalException">Thrown when lexical errors are detected.</exception>
        private void ReadNextToken()
        {
            StringBuilder TokenBuffer = new StringBuilder("");
            Char CurrentChar = ' ';

            if (m_SourceFileStream == null)
                return; // throw exception.

            while(m_SourceFileStream.EndOfStream == false)
            {
                CurrentChar = (Char)m_SourceFileStream.Peek();

                if (CurrentChar == '\n')
                    ++m_LineCount;

                if (Char.IsWhiteSpace(CurrentChar) != true)
                {
                    // first check for identifiers and keywords.
                    if (Char.IsLetter(CurrentChar) == true)
                    {
                        TokenBuffer.Append((Char)m_SourceFileStream.Read());

                        CurrentChar = (Char)m_SourceFileStream.Peek();

                        // Identifiers can be made up of letters, digits and the underscore.
                        while (Char.IsLetterOrDigit(CurrentChar) || CurrentChar == '_')
                        {
                            TokenBuffer.Append((Char)m_SourceFileStream.Read());
                            CurrentChar = (Char)m_SourceFileStream.Peek();
                        }

                        // check to see if the current token is a keyword or an identifer.
                        
                        if(m_Keywords.IndexOf(TokenBuffer.ToString()) == -1)
                        {
                            m_CurrentToken = TTokens.IDENTIFIER;
                        }
                        else
                        {
                            GetKeywordEnumType(TokenBuffer.ToString());
                        }
                        m_TokenValue = TokenBuffer.ToString();
                        TokenBuffer.Remove(0, TokenBuffer.Length);
                        return;
                    }
                    else if (Char.IsDigit(CurrentChar) == true)
                    {
                        TokenBuffer.Append((Char)m_SourceFileStream.Read());
                        CurrentChar = (Char)m_SourceFileStream.Peek();

                        while (Char.IsDigit(CurrentChar) == true)
                        {
                            TokenBuffer.Append((Char)m_SourceFileStream.Read());
                            CurrentChar = (Char)m_SourceFileStream.Peek();
                        }
                        m_CurrentToken = TTokens.LITERAL;

                        m_TokenValue = TokenBuffer.ToString();
                        TokenBuffer.Remove(0, TokenBuffer.Length);

                        return;
                    }
                    else if (CurrentChar == '(')
                    {
                        m_CurrentToken = TTokens.LPAREN;
                        m_SourceFileStream.Read();

                        return;
                    }
                    else if (CurrentChar == ')')
                    {
                        m_CurrentToken = TTokens.RPAREN;
                        m_SourceFileStream.Read();

                        return;
                    }
                    else if (CurrentChar == '{')
                    {
                        m_CurrentToken = TTokens.LCURLY;
                        m_SourceFileStream.Read();

                        ++m_ScopeLevelCount;

                        return;
                    }
                    else if (CurrentChar == '}')
                    {
                        m_CurrentToken = TTokens.RCURLY;
                        m_SourceFileStream.Read();

                        --m_ScopeLevelCount;

                        return;
                    }
                    else if (CurrentChar == '[')
                    {
                        m_CurrentToken = TTokens.LBRACKET;
                        m_SourceFileStream.Read();

                        return;
                    }
                    else if (CurrentChar == ']')
                    {
                        m_CurrentToken = TTokens.RBRACKET;
                        m_SourceFileStream.Read();

                        return;
                    }
                    else if (CurrentChar == ',')
                    {
                        m_CurrentToken = TTokens.COMMA;
                        m_SourceFileStream.Read();
                        return;
                    }
                    else if (CurrentChar == ';')
                    {
                        m_CurrentToken = TTokens.SEMICOL;
                        m_SourceFileStream.Read();

                        return;
                    }
                    else if (CurrentChar == '*')
                    {
                        m_CurrentToken = TTokens.MULOP;
                        m_SourceFileStream.Read();

                        return;
                    }
                    else if (CurrentChar == '/')
                    {
                        m_SourceFileStream.Read();
                        if ((Char)m_SourceFileStream.Peek() == '/')
                        {
                            // strip out the comments.
                            do
                            {
                                m_SourceFileStream.Read();
                            } while ((Char)m_SourceFileStream.Peek() != '\n');
                            ++m_LineCount;
                        }
                        else
                        {
                            m_CurrentToken = TTokens.DIVOP;
                            m_SourceFileStream.Read();

                            return;
                        }
                    }
                    else if (CurrentChar == '^')
                    {
                        m_CurrentToken = TTokens.XOROP;
                        m_SourceFileStream.Read();

                        return;
                    }
                    else if (CurrentChar == '<')
                    {
                        m_SourceFileStream.Read();
                        if ((Char)m_SourceFileStream.Peek() == '<')
                        {
                            m_SourceFileStream.Read();
                            m_CurrentToken = TTokens.LSHIFTOP;

                            return;
                        }
                        else if ((Char)m_SourceFileStream.Peek() == '=')
                        {
                            m_CurrentToken = TTokens.LTEOP;
                            m_SourceFileStream.Read();

                            return;
                        }
                        else
                        {
                            m_CurrentToken = TTokens.LTOP;

                            return;
                        }
                    }
                    else if (CurrentChar == '>')
                    {
                        m_SourceFileStream.Read();
                        if ((Char)m_SourceFileStream.Peek() == '>')
                        {
                            m_CurrentToken = TTokens.RSHIFTOP;
                            m_SourceFileStream.Read();

                            return;
                        }
                        else if ((Char)m_SourceFileStream.Peek() == '=')
                        {
                            m_CurrentToken = TTokens.GTEOP;
                            m_SourceFileStream.Read();
                            return;
                        }
                        else
                        {
                            m_CurrentToken = TTokens.GTOP;
                            return;
                        }
                    }
                    else if (CurrentChar == '+')
                    {
                        m_SourceFileStream.Read();
                        if ((Char)m_SourceFileStream.Peek() == '+')
                        {
                            m_CurrentToken = TTokens.INCOP;
                            m_SourceFileStream.Read();

                            return;
                        }
                        else
                        {
                            m_CurrentToken = TTokens.ADDOP;

                            return;
                        }
                    }
                    else if (CurrentChar == '-')
                    {
                        m_SourceFileStream.Read();
                        if ((Char)m_SourceFileStream.Peek() == '-')
                        {
                            m_CurrentToken = TTokens.DECOP;
                            m_SourceFileStream.Read();

                            return;
                        }
                        else
                        {
                            m_CurrentToken = TTokens.SUBOP;

                            return;
                        }
                    }
                    else if (CurrentChar == '=')
                    {
                        m_SourceFileStream.Read();
                        if ((Char)m_SourceFileStream.Peek() == '=')
                        {
                            m_CurrentToken = TTokens.EQUALOP;
                            m_SourceFileStream.Read();

                            return;
                        }
                        else
                        {
                            m_CurrentToken = TTokens.ASSIGNMENT;

                            return;
                        }
                    }
                    else if (CurrentChar == '!')
                    {
                        m_SourceFileStream.Read();
                        if ((Char)m_SourceFileStream.Peek() == '=')
                        {
                            m_CurrentToken = TTokens.NOTEQLOP;
                            m_SourceFileStream.Read();

                            return;
                        }
                        else
                        {
                            m_CurrentToken = TTokens.NOTOP;

                            return;
                        }
                    }
                    else if (CurrentChar == '&')
                    {
                        m_SourceFileStream.Read();
                        if ((Char)m_SourceFileStream.Peek() == '&')
                        {
                            m_CurrentToken = TTokens.ANDOP;
                            m_SourceFileStream.Read();

                            return;
                        }
                        else
                        {
                            m_CurrentToken = TTokens.ANDBITOP;

                            return;
                        }
                    }
                    else if (CurrentChar == '|')
                    {
                        m_SourceFileStream.Read();
                        if ((Char)m_SourceFileStream.Peek() == '|')
                        {
                            m_CurrentToken = TTokens.OROP;
                            m_SourceFileStream.Read();

                            return;
                        }
                        else
                        {
                            m_CurrentToken = TTokens.ORBITOP;

                            return;
                        }
                    }
                    else
                        throw new CCompilerException("Lexical Error", m_LineCount, TErrorCodes.LEXICAL_ERROR);

                }
                else
                    m_SourceFileStream.Read(); 
            }
            m_CurrentToken = TTokens.EOF;

        }
                
        /// <summary>
        /// returns the string value of a token. Such as variable
        /// names and constant values.
        /// </summary>
        public String TokenValue
        {
            get
            {
                return m_TokenValue;
            }
        }

        /// <summary>
        /// Holds the current scope level value.
        /// </summary>
        public Int32 ScopeLevelCount
        {
            get
            {
                return m_ScopeLevelCount;
            }
        }
        
        /// <summary>
        /// Reads the JasC reserved keywords and stores them in a StringCollection.
        /// </summary>
        /// <param name="Filename">The path to the xml file containing the keywords.</param>
        private void BuildKeywordCollection(String Filename)
        {
            XmlReaderSettings Settings = null;
            XmlReader Keywords = null;
            
            try
            {
                Settings = new XmlReaderSettings();
                Keywords = XmlReader.Create(Filename, Settings);

                m_Keywords = new StringCollection();

                while (Keywords.Read() == true)
                {
                    if (Keywords.NodeType == XmlNodeType.Text)
                        m_Keywords.Add(Keywords.Value);
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                Keywords.Close();
            }
        }

        /// <summary>
        /// Assigns the correct enumeration constant to m_CurrentToken.
        /// </summary>
        /// <param name="Token">The current token read from the inputstream.</param>
        private void GetKeywordEnumType(String Token)
        {
            switch (Token)
            {
                case "if":
                    m_CurrentToken = TTokens.IF;
                    break;
                case "while":
                    m_CurrentToken = TTokens.WHILE;
                    break;
                case "int":
                    m_CurrentToken = TTokens.INT;
                    break;
                case "void":
                    m_CurrentToken = TTokens.VOID;
                    break;
                case "signed":
                    m_CurrentToken = TTokens.SIGNED;
                    break;
                case "unsigned":
                    m_CurrentToken = TTokens.UNSIGNED;
                    break;
                case "do":
                    m_CurrentToken = TTokens.DOWHILE;
                    break;
                case "return":
                    m_CurrentToken = TTokens.RETURN;
                    break;
            }
        }

        public void CloseSourceFile()
        {
            m_SourceFile.Close();
            m_SourceFileStream.Close();
        }

        /// <summary>
        /// Used to open the JasC source file.
        /// </summary>
        private FileStream m_SourceFile = null;

        /// <summary>
        /// Used to read the JasC source file as text.
        /// </summary>
        private StreamReader m_SourceFileStream = null;

        /// <summary>
        /// Stores the current token read from the file.
        /// </summary>
        private TTokens m_CurrentToken = TTokens.NONE;

        /// <summary>
        /// Keeps the line count. This value is used to send error messages to the 
        /// user.
        /// </summary>
        private Int32 m_LineCount = 1;

        /// <summary>
        /// A collection of all the reserved keywords in the JasC language.
        /// </summary>
        private StringCollection m_Keywords = null;

        /// <summary>
        /// Holds the string value of a token. Such as variable
        /// names and constant values.
        /// </summary>
        private String m_TokenValue = null;

        /// <summary>
        /// Holds the current scope level value.
        /// </summary>
        private Int32 m_ScopeLevelCount = 0;
    }
}