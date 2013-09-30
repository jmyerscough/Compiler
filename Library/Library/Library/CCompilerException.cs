using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    /// <summary>
    /// 
    /// </summary>
    public class CCompilerException : Exception 
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Error"></param>
        /// <param name="LineNumber"></param>
        public CCompilerException(String Error, Int32 LineNumber, TErrorCodes ErrCode)
            : base(Error)
        {
            m_LineNumber = LineNumber;
            m_ErrorCode = ErrCode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Error"></param>
        public CCompilerException(String Error, TErrorCodes ErrCode)
            : base(Error)
        {
            m_ErrorCode = ErrCode;
        }

        /// <summary>
        /// 
        /// </summary>
        public Int32 LineNumber
        {
            get
            {
                return m_LineNumber;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public TErrorCodes ErrorCode
        {
            get
            {
                return m_ErrorCode;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private Int32 m_LineNumber = 0;

        private TErrorCodes m_ErrorCode;
    }
}
