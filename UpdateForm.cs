using PM;
using PM.config;
using PM.utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PM
{
    public partial class UpdateForm : Form
    {
        IniUtils iniUtils = new IniUtils();
        public MainForm mainForm { get; set; }
        public String section { get; set; }

        public UpdateForm()
        {
            InitializeComponent();
        }

        private void UpdateForm_Load(object sender, EventArgs e)
        {
            ProjectSections.ProjectSection monitorSection = ProjectSections.getProjectBySection(section);
            String title = monitorSection.title;
            String jar = monitorSection.jar;
            String port = monitorSection.port;
            String heartBeat = monitorSection.heartBeat;
            UpdateForm_Title_TextBox.Text = title;
            UpdateForm_Jar_TextBox.Text = jar;
            UpdateForm_Port_TextBox.Text = port;
            UpdateForm_HeartBeat_TextBox.Text = heartBeat;
        }

        private void UpdateForm_Cancel_Button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UpdateForm_Save_Button_Click(object sender, EventArgs e)
        {
            String title = UpdateForm_Title_TextBox.Text;
            String jar = UpdateForm_Jar_TextBox.Text;
            String port = UpdateForm_Port_TextBox.Text;
            String heartBeat = UpdateForm_HeartBeat_TextBox.Text;
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
            }
            else
            {
                if (!Regex.IsMatch(port, @"^[+-]?\d*$"))
                {
                    // 端口必须要是数字
                    checkFlag = false;
                    checkMsg.Append("端口不合法\r\n");
                }
            }
            if ("".Equals(heartBeat))
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
                /** 数据正常，修改ini数据，执行StartForm添加按钮和新增rdp文件操作 */
                // 生成title
                iniUtils.IniWriteValue(Config.ProjectsIniPath, section, "title", title);
                iniUtils.IniWriteValue(Config.ProjectsIniPath, section, "jar", jar);
                iniUtils.IniWriteValue(Config.ProjectsIniPath, section, "port", port);
                iniUtils.IniWriteValue(Config.ProjectsIniPath, section, "heartBeat", heartBeat);
                /* 生成新INI结束 ************************/
                // sections缓存数据新增
                ProjectSections.ProjectSection monitorSection = ProjectSections.getProjectBySection(section);
                monitorSection.title = title;
                monitorSection.jar = jar;
                monitorSection.port = port;
                monitorSection.heartBeat = heartBeat;
                ProjectSections.updateProjectSection(section, monitorSection);
                /* StartForm中更新服务按钮 *************/
                mainForm.updateButton(section);
                /* 更新服务按钮完成****** *************/
                // 关闭修改窗口
                UpdateForm_Cancel_Button_Click(sender, e);
            }
        }
    }
}
