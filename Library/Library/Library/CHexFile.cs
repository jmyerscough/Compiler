using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;
using System.Xml;

namespace Compiler
{
    /// <summary>
    /// This is pretty much an container class that writes the
    /// collection to file.
    /// </summary>
    public class CHexFile
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Filename"></param>
        public CHexFile(String Filename)
        {
            try
            {
                m_HexFile = new FileStream(Filename, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                m_HexFileStream = new StreamWriter(m_HexFile);
            }
            catch (Exception)
            {
                throw new CCompilerException("Unable to create .hex file.", TErrorCodes.FILE_ACCESS_ERR);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="HexRecord"></param>
        public void InsertHexRecord(CMachineCodeRecord HexRecord)
        {   
            if (m_HexRecords == null)
                m_HexRecords = new ArrayList();

            m_HexRecords.Add(HexRecord);
        }

        /// <summary>
        /// Writes all the hex records to the file.
        /// </summary>
        public void GenerateHexFile()
        {
            try
            {
                m_HexFileStream.WriteLine(HexFileHeader);

                //now write each of the hex records to the file.
                foreach (CMachineCodeRecord MachineRecord in m_HexRecords)
                {
                    m_HexFileStream.Write(":{0:X2}{1:X4}{2:X2}", MachineRecord.BytesInRecord, 
                                                                 MachineRecord.LoadAddress, 
                                                                 MachineRecord.RecordType);

                    for (int i = 0; i < MachineRecord.MachineCode.Count; i++ )
                    {
                        m_HexFileStream.Write("{0:X4}", MachineRecord.MachineCode[i]);
                    }

                    m_HexFileStream.WriteLine("{0:X2}", MachineRecord.CheckSum);
                }

                m_HexFileStream.WriteLine(EndOfFile);
            }
            catch(Exception)
            {
                    throw new CCompilerException("Unable to create .HEX file.", TErrorCodes.FILE_ACCESS_ERR);
            }
            finally
            {
                // close the file and stream.
                m_HexFileStream.Close();
                m_HexFile.Close(); 
            }
        }

        /// <summary>
        /// Creates an Xml file containing the machine code.
        /// This makes it eaiser to read when debugging application.
        /// And a transformation can be applied to the XML to format the
        /// document.
        /// </summary>
        /// <param name="Filename"></param>
        public void CreateXmlFile(String Filename)
        {
            XmlWriterSettings Settings = new XmlWriterSettings();
            XmlWriter XmlFile = XmlWriter.Create(Filename, Settings);

            try
            {
                XmlFile.WriteStartDocument();
                XmlFile.WriteStartElement("PicMachineCode");

                foreach (CMachineCodeRecord Record in m_HexRecords)
                {
                    XmlFile.WriteStartElement("HexRecord");

                    XmlFile.WriteStartElement("BytesInRecord");
                    XmlFile.WriteString(String.Format("{0:X2}", Record.BytesInRecord));
                    XmlFile.WriteEndElement();

                    XmlFile.WriteStartElement("LoadAddress");
                    XmlFile.WriteString(String.Format("{0:X4}", Record.LoadAddress));
                    XmlFile.WriteEndElement();

                    XmlFile.WriteStartElement("RecordType");
                    XmlFile.WriteString(String.Format("{0:X2}", Record.RecordType));
                    XmlFile.WriteEndElement();

                    XmlFile.WriteStartElement("MachineCodeWords");
                    for (int i = 0; i < Record.MachineCode.Count; i++)
                    {
                        XmlFile.WriteString(String.Format("{0:X4}\t", Record.MachineCode[i]));
                    }
                    XmlFile.WriteEndElement();

                    XmlFile.WriteStartElement("CheckSum");
                    XmlFile.WriteString(String.Format("{0:X2}", Record.CheckSum));
                    XmlFile.WriteEndElement();

                    // close HexRecord
                    XmlFile.WriteEndElement();
                }
            }
            finally
            {
                // close PicMachineCode
                XmlFile.WriteEndElement();
                XmlFile.Close();
            }
        }

        public int CurrentRecordIndex
        {
            get
            {
                return m_HexRecords.Count;
            }
        }

        public ArrayList HexRecords
        {
            get
            {
                return m_HexRecords;
            }
        }

        /// <summary>
        /// The new .hex file generated by the compiler.
        /// </summary>
        private FileStream m_HexFile = null;

        /// <summary>
        /// the hex files input stream.
        /// </summary>
        private StreamWriter m_HexFileStream = null;

        /// <summary>
        /// Stores all of the Hex, which
        /// will be written to the .hex file.
        /// </summary>
        private ArrayList m_HexRecords = null;

        /// <summary>
        /// All .hex files begin with the header
        /// </summary>
        private const string HexFileHeader = ":020000040000FA";

        /// <summary>
        /// All .hex files end with this signture.
        /// </summary>
        private const string EndOfFile = ":00000001FF";
     }
}
