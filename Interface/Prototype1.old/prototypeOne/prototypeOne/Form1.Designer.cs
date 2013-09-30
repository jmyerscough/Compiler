namespace prototypeOne
{
    partial class CompilerFrm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CompilerFrm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.NewBtn = new System.Windows.Forms.ToolStripButton();
            this.OpenBtn = new System.Windows.Forms.ToolStripButton();
            this.SaveBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.BuildBtn = new System.Windows.Forms.ToolStripButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buildToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jasCIDEHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jasCLanguageHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.JasCOpenDlg = new System.Windows.Forms.OpenFileDialog();
            this.JasCSaveDlg = new System.Windows.Forms.SaveFileDialog();
            this.StatusEdit = new System.Windows.Forms.RichTextBox();
            this.SourceEdit = new System.Windows.Forms.RichTextBox();
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewBtn,
            this.OpenBtn,
            this.SaveBtn,
            this.toolStripSeparator1,
            this.BuildBtn});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1107, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // NewBtn
            // 
            this.NewBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.NewBtn.Image = ((System.Drawing.Image)(resources.GetObject("NewBtn.Image")));
            this.NewBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NewBtn.Name = "NewBtn";
            this.NewBtn.Size = new System.Drawing.Size(23, 22);
            this.NewBtn.Text = "New JasC File";
            this.NewBtn.Click += new System.EventHandler(this.NewBtn_Click);
            // 
            // OpenBtn
            // 
            this.OpenBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.OpenBtn.Image = ((System.Drawing.Image)(resources.GetObject("OpenBtn.Image")));
            this.OpenBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.OpenBtn.Name = "OpenBtn";
            this.OpenBtn.Size = new System.Drawing.Size(23, 22);
            this.OpenBtn.Text = "Open JasC File";
            this.OpenBtn.Click += new System.EventHandler(this.OpenBtn_Click);
            // 
            // SaveBtn
            // 
            this.SaveBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SaveBtn.Image = ((System.Drawing.Image)(resources.GetObject("SaveBtn.Image")));
            this.SaveBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(23, 22);
            this.SaveBtn.Text = "Save JasC File";
            this.SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // BuildBtn
            // 
            this.BuildBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BuildBtn.Image = ((System.Drawing.Image)(resources.GetObject("BuildBtn.Image")));
            this.BuildBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BuildBtn.Name = "BuildBtn";
            this.BuildBtn.Size = new System.Drawing.Size(23, 22);
            this.BuildBtn.Text = "Build Project";
            this.BuildBtn.Click += new System.EventHandler(this.BuildBtn_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.buildToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1107, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripMenuItem.Image")));
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newToolStripMenuItem.Text = "&New";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openToolStripMenuItem.Text = "O&pen";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem.Text = "S&ave";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(149, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            // 
            // buildToolStripMenuItem
            // 
            this.buildToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.compileToolStripMenuItem});
            this.buildToolStripMenuItem.Name = "buildToolStripMenuItem";
            this.buildToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.buildToolStripMenuItem.Text = "Build";
            // 
            // compileToolStripMenuItem
            // 
            this.compileToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("compileToolStripMenuItem.Image")));
            this.compileToolStripMenuItem.Name = "compileToolStripMenuItem";
            this.compileToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.compileToolStripMenuItem.Text = "&Compile";
            this.compileToolStripMenuItem.Click += new System.EventHandler(this.compileToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.jasCIDEHelpToolStripMenuItem,
            this.jasCLanguageHelpToolStripMenuItem,
            this.toolStripMenuItem2,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // jasCIDEHelpToolStripMenuItem
            // 
            this.jasCIDEHelpToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("jasCIDEHelpToolStripMenuItem.Image")));
            this.jasCIDEHelpToolStripMenuItem.Name = "jasCIDEHelpToolStripMenuItem";
            this.jasCIDEHelpToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.jasCIDEHelpToolStripMenuItem.Text = "JasC IDE Help";
            // 
            // jasCLanguageHelpToolStripMenuItem
            // 
            this.jasCLanguageHelpToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("jasCLanguageHelpToolStripMenuItem.Image")));
            this.jasCLanguageHelpToolStripMenuItem.Name = "jasCLanguageHelpToolStripMenuItem";
            this.jasCLanguageHelpToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.jasCLanguageHelpToolStripMenuItem.Text = "JasC Language Help";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(179, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // JasCOpenDlg
            // 
            this.JasCOpenDlg.AddExtension = false;
            this.JasCOpenDlg.Filter = "jasc files|*.jasc";
            this.JasCOpenDlg.Title = "JasC PIC Compiler";
            // 
            // JasCSaveDlg
            // 
            this.JasCSaveDlg.Filter = "jasc files|*.jasc";
            // 
            // StatusEdit
            // 
            this.StatusEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.StatusEdit.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.StatusEdit.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusEdit.Location = new System.Drawing.Point(0, 493);
            this.StatusEdit.Name = "StatusEdit";
            this.StatusEdit.ReadOnly = true;
            this.StatusEdit.Size = new System.Drawing.Size(1107, 152);
            this.StatusEdit.TabIndex = 7;
            this.StatusEdit.Text = "";
            // 
            // SourceEdit
            // 
            this.SourceEdit.Dock = System.Windows.Forms.DockStyle.Top;
            this.SourceEdit.Location = new System.Drawing.Point(0, 49);
            this.SourceEdit.Name = "SourceEdit";
            this.SourceEdit.Size = new System.Drawing.Size(1107, 438);
            this.SourceEdit.TabIndex = 8;
            this.SourceEdit.Text = "";
            // 
            // CompilerFrm
            // 
            this.ClientSize = new System.Drawing.Size(1107, 657);
            this.Controls.Add(this.SourceEdit);
            this.Controls.Add(this.StatusEdit);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "CompilerFrm";
            this.Text = "JasC Compiler IDE";
            this.Load += new System.EventHandler(this.CompilerFrm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton NewBtn;
        private System.Windows.Forms.ToolStripButton OpenBtn;
        private System.Windows.Forms.ToolStripButton SaveBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton BuildBtn;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buildToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem compileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jasCIDEHelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jasCLanguageHelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog JasCOpenDlg;
        private System.Windows.Forms.SaveFileDialog JasCSaveDlg;
        private System.Windows.Forms.RichTextBox StatusEdit;
        private System.Windows.Forms.RichTextBox SourceEdit;
    }
}

