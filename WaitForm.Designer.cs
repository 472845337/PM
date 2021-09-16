
namespace PM
{
    partial class WaitForm
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
            this.WaitForm_ProgressBar = new System.Windows.Forms.ProgressBar();
            this.WaitForm_Label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // WaitForm_ProgressBar
            // 
            this.WaitForm_ProgressBar.Location = new System.Drawing.Point(2, 45);
            this.WaitForm_ProgressBar.Name = "WaitForm_ProgressBar";
            this.WaitForm_ProgressBar.Size = new System.Drawing.Size(259, 23);
            this.WaitForm_ProgressBar.Step = 1;
            this.WaitForm_ProgressBar.TabIndex = 1;
            // 
            // WaitForm_Label
            // 
            this.WaitForm_Label.AutoSize = true;
            this.WaitForm_Label.Location = new System.Drawing.Point(113, 9);
            this.WaitForm_Label.Name = "WaitForm_Label";
            this.WaitForm_Label.Size = new System.Drawing.Size(53, 17);
            this.WaitForm_Label.TabIndex = 2;
            this.WaitForm_Label.Text = "加载中...";
            // 
            // WaitForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(264, 79);
            this.Controls.Add(this.WaitForm_Label);
            this.Controls.Add(this.WaitForm_ProgressBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WaitForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "加载数据";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.WaitForm_FormClosed);
            this.Load += new System.EventHandler(this.WaitForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ProgressBar WaitForm_ProgressBar;
        private System.Windows.Forms.Label WaitForm_Label;
    }
}