
using PM.config;
using PM.utils;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PM
{
    public partial class AddForm : Form
    {
        private IniUtils iniUtils = new IniUtils();
        public MainForm mainForm { get; set; }

        public AddForm()
        {
            InitializeComponent();
        }

        private void AddForm_Cancel_Button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddForm_Save_Button_Click(object sender, EventArgs e)
        {
            String title = AddForm_Title_TextBox.Text;
            String jar = AddForm_Jar_TextBox.Text;
            String port = AddForm_Port_TextBox.Text;
            String heartBeat = AddForm_HeartBeat_TextBox.Text;
            Boolean checkFlag = true;
            StringBuilder checkMsg = new StringBuilder();
            if ("".Equals(title))
            {
                checkFlag = false;
                checkMsg.Append("名称未填写\r\n");
            }
            if ("".Equals(jar))
            {
                checkFlag = false;
                checkMsg.Append("jar包路径未选择\r\n");
            }
            if ("".Equals(port))
            {
                checkFlag = false;
                checkMsg.Append("端口未配置\r\n");
            }  else {
                if (!Regex.IsMatch(port, @"^[+-]?\d*$"))
                {
                    // 端口必须要是数字
                    checkFlag = false;
                    checkMsg.Append("端口不合法\r\n");
                }
            }
            if ("".Equals(heartBeat ))
            {
                checkFlag = false;
                checkMsg.Append("心跳监控地址未配置\r\n");
            }
            
            if (!checkFlag)
            {
                MessageBox.Show(checkMsg.ToString(), "错误");
            }
            else
            {
                /** 数据正常，生成新的ini数据，执行StartForm添加按钮和新增rdp文件操作 */
                /* 生成新INI ****************************/
                String newSection = Guid.NewGuid().ToString();
                // 生成title
                iniUtils.IniWriteValue(Config.ProjectsIniPath, newSection, "title", title);
                iniUtils.IniWriteValue(Config.ProjectsIniPath, newSection, "jar", jar);
                iniUtils.IniWriteValue(Config.ProjectsIniPath, newSection, "port", port);
                iniUtils.IniWriteValue(Config.ProjectsIniPath, newSection, "heartBeat", heartBeat);
                // 生成taskkill.bat
                ProjectUtils.createStopBat(title, port);
                /* StartForm中添加新服务按钮 *************/
                mainForm.addButton(newSection, title, jar, port, heartBeat);
                /* 添加新服务按钮完成****** *************/
                // 关闭新增窗口
                AddForm_Cancel_Button_Click(sender, e);
            }
        }

        private void AddForm_Load(object sender, EventArgs e)
        {

        }

        private void Jar_Dialog_Button_Click(object sender, EventArgs e)
        {
            if(Jar_OpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                AddForm_Jar_TextBox.Text = Jar_OpenFileDialog.FileName;
            }
        }
    }
}
