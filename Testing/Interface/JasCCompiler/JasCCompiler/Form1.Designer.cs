namespace JasCCompiler
{
    partial class Form1
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
            this.MenuPanel = new System.Windows.Forms.Panel();
            this.EditAndTree = new System.Windows.Forms.SplitContainer();
            this.EditAndTree.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuPanel
            // 
            this.MenuPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.MenuPanel.Location = new System.Drawing.Point(0, 0);
            this.MenuPanel.Name = "MenuPanel";
            this.MenuPanel.Size = new System.Drawing.Size(920, 54);
            this.MenuPanel.TabIndex = 0;
            // 
            // EditAndTree
            // 
            this.EditAndTree.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EditAndTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EditAndTree.Location = new System.Drawing.Point(0, 54);
            this.EditAndTree.Name = "EditAndTree";
            // 
            // EditAndTree.Panel1
            // 
            this.EditAndTree.Panel1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.EditAndTree.Size = new System.Drawing.Size(920, 621);
            this.EditAndTree.SplitterDistance = 682;
            this.EditAndTree.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(920, 675);
            this.Controls.Add(this.EditAndTree);
            this.Controls.Add(this.MenuPanel);
            this.Name = "Form1";
            this.Text = "JasC Compiler";
            this.EditAndTree.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel MenuPanel;
        private System.Windows.Forms.SplitContainer EditAndTree;
    }
}

