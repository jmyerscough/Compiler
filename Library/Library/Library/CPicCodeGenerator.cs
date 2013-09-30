using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;

namespace Compiler
{
    /// <summary>
    /// 
    /// </summary>
    public class CPicCodeGenerator
    {
        public CPicCodeGenerator(String Filename)
        {
            ArrayList Init = new ArrayList();

            m_Hexfile = new CHexFile(Filename);
            m_CurrentMemoryLocation = m_RecordMemoryMarker;

            Init.Add(0x0000);

            m_MachineRecord = new CMachineCodeRecord(m_RecordMemoryMarker, 00, Init);
            m_Hexfile.InsertHexRecord(m_MachineRecord);
            m_RecordMemoryMarker += (Int16)(1 * 2);
        
            m_CurrentMemoryLocation++;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Instruction"></param>
        public void AddInstruction(Object Instruction)
        {
            if (m_Instructions == null)
                m_Instructions = new ArrayList();

            if ((m_Instructions.Count * 2) == 0x10)
            {
                m_MachineRecord = new CMachineCodeRecord(m_RecordMemoryMarker, 00, m_Instructions);
                m_Hexfile.InsertHexRecord(m_MachineRecord);
                m_RecordMemoryMarker += (Int16)(m_Instructions.Count * 2);
                m_Instructions.Clear();
            }

            m_CurrentMemoryLocation++;
            m_Instructions.Add(Instruction);
        }

        /// <summary>
        /// 
        /// </summary>
        public void GenerateHexFile()
        {
            // Write the rest of the array to file.
            if (m_Instructions.Count > 0)
            {
                m_MachineRecord = new CMachineCodeRecord(m_RecordMemoryMarker, 00, m_Instructions);
                m_Hexfile.InsertHexRecord(m_MachineRecord);
            }
            m_Hexfile.GenerateHexFile();
        }

        public int CurrentOpCodeIndex
        {
            get
            {
                return m_Instructions.Count;
            }
        }

        public int CurrentHexRecordIndex
        {
            get
            {
                return m_Hexfile.CurrentRecordIndex;
            }
        }

        public Int16 CurrentCodeMemoryLocation
        {
            get
            {
                return m_CurrentMemoryLocation;
            }            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Filename"></param>
        public void GenerateXmlFile(String Filename)
        {
            m_Hexfile.CreateXmlFile(Filename);
        }

        /// <summary>
        /// Modifies the machine code stored at location RecordIndex, OpCodeIndex.
        /// </summary>
        /// <param name="RecordIndex"></param>
        /// <param name="OpCodeIndex"></param>
        /// <param name="NewOpCode"></param>
        public void ModifyOpCode(int RecordIndex, int OpCodeIndex, UInt16 NewOpCode)
        {
            CMachineCodeRecord Record = null;

            if (RecordIndex == m_Hexfile.CurrentRecordIndex-1)
                m_Instructions[OpCodeIndex] = NewOpCode;
            else
            {
                Record = (CMachineCodeRecord)m_Hexfile.HexRecords[RecordIndex+1];
                Record.MachineCode[OpCodeIndex] = NewOpCode;
                Record.GenerateCheckSum(Record.MachineCode);
            }
        }

        /// <summary>
        /// This is the starting address for a record
        /// </summary>
        private Int16 m_RecordMemoryMarker = 0x00;

        /// <summary>
        /// 
        /// </summary>
        private CHexFile m_Hexfile = null;

        /// <summary>
        /// 
        /// </summary>
        private CMachineCodeRecord m_MachineRecord = null;

        /// <summary>
        /// 
        /// </summary>
        private ArrayList m_Instructions = null;

        private Int16 m_CurrentMemoryLocation = 0;
    }
}