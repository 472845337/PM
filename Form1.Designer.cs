
namespace PM
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.System_GroupBox = new System.Windows.Forms.GroupBox();
            this.SystemConfig_Save_Button = new System.Windows.Forms.Button();
            this.JDPPath_Dialog_Button = new System.Windows.Forms.Button();
            this.JDKPath_TextBox = new System.Windows.Forms.TextBox();
            this.JDKPath_Label = new System.Windows.Forms.Label();
            this.Profile_ComboBox = new System.Windows.Forms.ComboBox();
            this.Profile_label = new System.Windows.Forms.Label();
            this.Project_GroupBox = new System.Windows.Forms.GroupBox();
            this.Projects_Panel = new System.Windows.Forms.FlowLayoutPanel();
            this.ProjectAdd_Button = new System.Windows.Forms.Button();
            this.JDKPath_FolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.Author_Label = new System.Windows.Forms.Label();
            this.System_GroupBox.SuspendLayout();
            this.Project_GroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // System_GroupBox
            // 
            this.System_GroupBox.Controls.Add(this.SystemConfig_Save_Button);
            this.System_GroupBox.Controls.Add(this.JDPPath_Dialog_Button);
            this.System_GroupBox.Controls.Add(this.JDKPath_TextBox);
            this.System_GroupBox.Controls.Add(this.JDKPath_Label);
            this.System_GroupBox.Controls.Add(this.Profile_ComboBox);
            this.System_GroupBox.Controls.Add(this.Profile_label);
            this.System_GroupBox.Location = new System.Drawing.Point(13, 13);
            this.System_GroupBox.Name = "System_GroupBox";
            this.System_GroupBox.Size = new System.Drawing.Size(630, 87);
            this.System_GroupBox.TabIndex = 0;
            this.System_GroupBox.TabStop = false;
            this.System_GroupBox.Text = "系统配置";
            // 
            // SystemConfig_Save_Button
            // 
            this.SystemConfig_Save_Button.Location = new System.Drawing.Point(570, 52);
            this.SystemConfig_Save_Button.Name = "SystemConfig_Save_Button";
            this.SystemConfig_Save_Button.Size = new System.Drawing.Size(54, 23);
            this.SystemConfig_Save_Button.TabIndex = 5;
            this.SystemConfig_Save_Button.Text = "保存";
            this.SystemConfig_Save_Button.UseVisualStyleBackColor = true;
            this.SystemConfig_Save_Button.Click += new System.EventHandler(this.SystemConfig_Save_Button_Click);
            // 
            // JDPPath_Dialog_Button
            // 
            this.JDPPath_Dialog_Button.Location = new System.Drawing.Point(598, 23);
            this.JDPPath_Dialog_Button.Name = "JDPPath_Dialog_Button";
            this.JDPPath_Dialog_Button.Size = new System.Drawing.Size(26, 23);
            this.JDPPath_Dialog_Button.TabIndex = 4;
            this.JDPPath_Dialog_Button.Text = "...";
            this.JDPPath_Dialog_Button.UseVisualStyleBackColor = true;
            this.JDPPath_Dialog_Button.Click += new System.EventHandler(this.JDKPath_Dialog_Button_Click);
            // 
            // JDKPath_TextBox
            // 
            this.JDKPath_TextBox.Location = new System.Drawing.Point(265, 23);
            this.JDKPath_TextBox.Name = "JDKPath_TextBox";
            this.JDKPath_TextBox.Size = new System.Drawing.Size(327, 23);
            this.JDKPath_TextBox.TabIndex = 3;
            // 
            // JDKPath_Label
            // 
            this.JDKPath_Label.AutoSize = true;
            this.JDKPath_Label.Location = new System.Drawing.Point(201, 23);
            this.JDKPath_Label.Name = "JDKPath_Label";
            this.JDKPath_Label.Size = new System.Drawing.Size(57, 17);
            this.JDKPath_Label.TabIndex = 2;
            this.JDKPath_Label.Text = "JDK路径:";
            // 
            // Profile_ComboBox
            // 
            this.Profile_ComboBox.FormattingEnabled = true;
            this.Profile_ComboBox.Items.AddRange(new object[] {
            "dev",
            "test",
            "prod"});
            this.Profile_ComboBox.Location = new System.Drawing.Point(73, 23);
            this.Profile_ComboBox.Name = "Profile_ComboBox";
            this.Profile_ComboBox.Size = new System.Drawing.Size(121, 25);
            this.Profile_ComboBox.TabIndex = 1;
            // 
            // Profile_label
            // 
            this.Profile_label.AutoSize = true;
            this.Profile_label.Location = new System.Drawing.Point(7, 23);
            this.Profile_label.Name = "Profile_label";
            this.Profile_label.Size = new System.Drawing.Size(59, 17);
            this.Profile_label.TabIndex = 0;
            this.Profile_label.Text = "运行环境:";
            // 
            // Project_GroupBox
            // 
            this.Project_GroupBox.Controls.Add(this.Projects_Panel);
            this.Project_GroupBox.Controls.Add(this.ProjectAdd_Button);
            this.Project_GroupBox.Location = new System.Drawing.Point(13, 107);
            this.Project_GroupBox.Name = "Project_GroupBox";
            this.Project_GroupBox.Size = new System.Drawing.Size(630, 330);
            this.Project_GroupBox.TabIndex = 1;
            this.Project_GroupBox.TabStop = false;
            this.Project_GroupBox.Text = "项目";
            // 
            // Projects_Panel
            // 
            this.Projects_Panel.Location = new System.Drawing.Point(7, 23);
            this.Projects_Panel.Name = "Projects_Panel";
            this.Projects_Panel.Size = new System.Drawing.Size(617, 248);
            this.Projects_Panel.TabIndex = 1;
            // 
            // ProjectAdd_Button
            // 
            this.ProjectAdd_Button.Font = new System.Drawing.Font("Microsoft YaHei UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ProjectAdd_Button.Location = new System.Drawing.Point(6, 277);
            this.ProjectAdd_Button.Name = "ProjectAdd_Button";
            this.ProjectAdd_Button.Size = new System.Drawing.Size(618, 46);
            this.ProjectAdd_Button.TabIndex = 0;
            this.ProjectAdd_Button.Text = "+";
            this.ProjectAdd_Button.UseVisualStyleBackColor = true;
            this.ProjectAdd_Button.Click += new System.EventHandler(this.ProjectAdd_Button_Click);
            // 
            // Author_Label
            // 
            this.Author_Label.AutoSize = true;
            this.Author_Label.Location = new System.Drawing.Point(482, 453);
            this.Author_Label.Name = "Author_Label";
            this.Author_Label.Size = new System.Drawing.Size(161, 17);
            this.Author_Label.TabIndex = 2;
            this.Author_Label.Text = "作者:1320311706@qq.com";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(655, 485);
            this.Controls.Add(this.Author_Label);
            this.Controls.Add(this.Project_GroupBox);
            this.Controls.Add(this.System_GroupBox);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Java项目管理";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.System_GroupBox.ResumeLayout(false);
            this.System_GroupBox.PerformLayout();
            this.Project_GroupBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox System_GroupBox;
        private System.Windows.Forms.ComboBox Profile_ComboBox;
        private System.Windows.Forms.Label Profile_label;
        private System.Windows.Forms.Button SystemConfig_Save_Button;
        private System.Windows.Forms.Button JDPPath_Dialog_Button;
        private System.Windows.Forms.TextBox JDKPath_TextBox;
        private System.Windows.Forms.Label JDKPath_Label;
        private System.Windows.Forms.GroupBox Project_GroupBox;
        private System.Windows.Forms.Button ProjectAdd_Button;
        private System.Windows.Forms.FolderBrowserDialog JDKPath_FolderBrowserDialog;
        private System.Windows.Forms.FlowLayoutPanel Projects_Panel;
        private System.Windows.Forms.Label Author_Label;
    }
}

