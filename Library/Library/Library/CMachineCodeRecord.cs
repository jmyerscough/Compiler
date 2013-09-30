using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace Compiler
{
    /// <summary>
    /// This class is used to store each machine
    /// code record. These objects will be stored in 
    /// an array and will be outputted to file.
    /// </summary>
    public class CMachineCodeRecord
    {
        public CMachineCodeRecord(Int16 LoadAddress, Byte RecordType, ArrayList MachineCode)
        {
            // I have to multiple by 2 because the instructions are stored in
            // Words. (Int16)
            this.BytesInRecord = (Byte)(MachineCode.Count * 2);
            this.LoadAddress = LoadAddress;
            this.RecordType = RecordType;
            GenerateCheckSum(MachineCode);
            m_MachineCode = new ArrayList(MachineCode);
        }

        /// <summary>
        /// 
        /// </summary>
        public Int16 LoadAddress
        {
            get
            {
                return m_LoadAddress;
            }

            set
            {
                m_LoadAddress = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Byte BytesInRecord
        {
            get
            {
                return m_BytesInRecord;
            }

            set
            {
                m_BytesInRecord = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Byte RecordType
        {
            get
            {
                return m_RecordType;
            }

            set
            {
                m_RecordType = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Byte CheckSum
        {
            get
            {
                return m_CheckSum;
            }

            set
            {
                m_CheckSum = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ArrayList MachineCode
        {
            get
            {
                return m_MachineCode;
            }
        }

        /// <summary>
        /// Calculates a checksum. The checksum is calculated 
        /// using the following algorithm
        /// 
        /// (0x01 + !(Bytes in the Record)) % 0x100
        /// 
        /// </summary>
        /// <param name="MachineCode">The  byte of code used to  generate the checksum.</param>
        /// <returns>Checksum for the machine code record.</returns>
        public Byte GenerateCheckSum(ArrayList MachineCode)
        {
            Int32 CheckSum = BytesInRecord;

            CheckSum += (Byte)((LoadAddress >> 8) & 0xFF);
            CheckSum += (Byte)(LoadAddress & 0xFF);
            CheckSum += (Byte)RecordType;

            for(int i = 0; i < MachineCode.Count; i++) 
            {
                CheckSum += (Byte)(( Convert.ToInt32(MachineCode[i]) >> 8) & 0xFF);
                CheckSum += (Byte)(Convert.ToInt32(MachineCode[i]) & 0xFF);
            }

            this.CheckSum = (Byte)((0x01 + ~CheckSum) % 0x100);
            return this.CheckSum;
        }

        /// <summary>
        /// 
        /// </summary>
        private Byte m_BytesInRecord = 0x00;

        /// <summary>
        /// 
        /// </summary>
        private Int16 m_LoadAddress = 0x0000;

        /// <summary>
        /// 
        /// </summary>
        private Byte m_RecordType = 0x00;


        /// <summary>
        /// 
        /// </summary>
        private ArrayList m_MachineCode = null;

        /// <summary>
        /// 
        /// </summary>
        private Byte m_CheckSum = 0x00;
    }
}
