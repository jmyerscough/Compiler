using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Compiler;

namespace prototypeOne
{
    public partial class CompilerFrm : Form
    {
        public CompilerFrm()
        {
            InitializeComponent();

        }

        private void OpenBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (JasCOpenDlg.ShowDialog() == DialogResult.OK)
                {
                    Filename = JasCOpenDlg.FileName;
                    SourceEdit.LoadFile(JasCOpenDlg.FileName,
                                    RichTextBoxStreamType.PlainText);
                }
            }
            catch (IOException)
            {
            }
        }

        private String Filename = "";

        private CCompiler PicCompiler = null;

        private void BuildBtn_Click(object sender, EventArgs e)
        {
            if (Filename == "")
            {
                MessageBox.Show("You must save your source file before you can compile your program", "JasC Compiler Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
            else
            {
                SourceEdit.SaveFile(Filename, RichTextBoxStreamType.PlainText);
                
                PicCompiler = new CCompiler(Filename, "keywords.xml",
                                            "SpecialRegisters.xml",
                                                PIC_PROCESSOR.PIC16F84);

                try
                {
                    StatusEdit.AppendText("Building Application ...\n");
                    if (PicCompiler.Build() == true)
                        StatusEdit.AppendText("Build complete 0 Error(s)\n");

                    StatusEdit.AppendText("\n\n");
                    PicCompiler.OuputGeneratedFiles(true);
                }
                catch (CCompilerException Ex)
                {
                    StatusEdit.AppendText("Build Failed!!\n");
                    StatusEdit.AppendText(Ex.Message + " on line " + Ex.LineNumber);
                    StatusEdit.AppendText("\n========================================\n\n");
                }
            }
        }

        private void CompilerFrm_Load(object sender, EventArgs e)
        {
            SourceEdit.Focus();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (JasCSaveDlg.ShowDialog() == DialogResult.OK)
            {
                Filename = JasCSaveDlg.FileName;
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenBtn_Click(sender, e);
        }

        private void NewBtn_Click(object sender, EventArgs e)
        {
           SourceEdit.Clear();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveBtn_Click(sender, e);
        }

        private void compileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BuildBtn_Click(sender, e);
        }
    }
}