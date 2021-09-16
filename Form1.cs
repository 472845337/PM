using PM.config;
using PM.utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace PM
{
    public partial class MainForm : Form
    {
        /** 强制GC API函数**/
        [System.Runtime.InteropServices.DllImport("kernel32")]
        public static extern Int32 SetProcessWorkingSetSize(IntPtr process, Int32 minSize, Int32 maxSize);
        IniUtils iniUtils = new IniUtils();
        // 不能使用窗体中的timer控件，要使用线程timer
        System.Timers.Timer GcTimer = new System.Timers.Timer();
        System.Timers.Timer MonitorTimer = new System.Timers.Timer();
        public MainForm()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            initData();
        }

        int progress = 0;
        private void initData()
        {
            WaitForm waitForm = new WaitForm();
            waitForm.mainForm = this;
            waitForm.Show();
            waitForm.Update();
            /// GC回收定时任务初始化
            GcTimer.Interval = 20000;
            GcTimer.Enabled = true;
            // 给时间控件绑定事件
            GcTimer.Elapsed += new System.Timers.ElapsedEventHandler(GCTimer_Tick);
            GcTimer.AutoReset = true;
            /// 参数加载

            String logFileName = iniUtils.IniReadValue(Config.SystemIniPath, "log", "filename");
            Config.logFileName = logFileName;
            waitForm.freshProgress(progress+=5);
            Thread.Sleep(200);
            String profile = iniUtils.IniReadValue(Config.SystemIniPath, "system", "profile");
            waitForm.freshProgress(progress += 5);
            Thread.Sleep(200);
            String JDKPath = iniUtils.IniReadValue(Config.SystemIniPath, "system", "JDKPath");
            waitForm.freshProgress(progress += 5);
            Thread.Sleep(200);
            // 计时器频率
            String intervalStr = iniUtils.IniReadValue(Config.SystemIniPath, "system", "interval");
            // 请求超时时间
            String timeoutStr = iniUtils.IniReadValue(Config.SystemIniPath, "system", "timeout");
            if(null == intervalStr || "".Equals(intervalStr))
            {
                intervalStr = "5000";
                iniUtils.IniWriteValue(Config.SystemIniPath, "system", "interval", "5000");
            }
            if(null == timeoutStr || "".Equals(timeoutStr))
            {
                timeoutStr = "20";
                iniUtils.IniWriteValue(Config.SystemIniPath, "system", "timeout", "20");
            }

            // 监控器开
            Config.interval = Convert.ToInt32(intervalStr);
            // http调用时的超时时间（单位秒，httputils已乘1000）
            Config.timeout = Convert.ToInt32(timeoutStr);

            MonitorTimer.Interval = Config.interval;
            MonitorTimer.Enabled = true;
            MonitorTimer.Elapsed += new System.Timers.ElapsedEventHandler(MonitorTimer_Tick);
            MonitorTimer.AutoReset = true;
            
            waitForm.freshProgress(progress += 5);
            Thread.Sleep(200);

            Profile_ComboBox.Text = profile;
            JDKPath_TextBox.Text = JDKPath;

            // 主窗体赋值，以便其它地方调用
            Config.mainForm = this;
            // 动态创建按钮控件
            List<String> sectionList = iniUtils.ReadSections(Config.ProjectsIniPath);
            waitForm.freshProgress(progress += 5);
            Thread.Sleep(200);
            int surplusProgress = 100 - progress;
            for (int i = 0; i < sectionList.Count; i++)
            {
                String section = sectionList[i];
                // 标题
                String buttonText = iniUtils.IniReadValue(Config.ProjectsIniPath, section, "title");
                // jar包路径
                String jar = iniUtils.IniReadValue(Config.ProjectsIniPath, section, "jar");
                // 端口
                String port = iniUtils.IniReadValue(Config.ProjectsIniPath, section, "port");
                // 监控地址
                String heartBeat = iniUtils.IniReadValue(Config.ProjectsIniPath, section, "heartBeat");
                // 创建按钮
                addButton(section, buttonText, jar, port, heartBeat);
                // 校验section
                checkSection(section, buttonText, port);
                waitForm.freshProgress(progress + (surplusProgress / sectionList.Count) * (i + 1));
                Thread.Sleep(200);
            }
            waitForm.freshProgress(100);
            Thread.Sleep(200);
            waitForm.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buttonName">按钮的代码名 用的section</param>
        /// <param name="buttonText">按钮文本</param>
        /// <param name="jar">jar包路径</param>
        /// <param name="port">启动端口</param>
        /// <param name="heartBeat">心跳检测地址</param>
        public void addButton(String buttonName, String buttonText, String jar, String port, String heartBeat)
        {
            Button button = new Button();
            button.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            button.Location = new System.Drawing.Point(3, 0);
            button.Name = buttonName;
            button.Size = new System.Drawing.Size(Convert.ToInt32(Projects_Panel.Width * 0.98), 46);
            button.TabIndex = 0;

            Image image = Image.FromFile(@"resource\icons\computer.ico");
            button.BackgroundImageLayout = ImageLayout.None;
            button.BackgroundImage = image;

            button.Text = buttonText;
            button.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            button.Font = new Font("微软雅黑", 12);

            button.MouseHover += new EventHandler(BtnMouseHover);

            
            /** 右键按钮添加事件
             * 
             * */
            /* 启动程序按钮 */
            ContextMenuStrip rightMenu = new ContextMenuStrip();
            ToolStripMenuItem startItem = new ToolStripMenuItem();
            // 默认为不可用状态，在识别到进程运行状态后再进行操作
            startItem.Enabled = false;
            startItem.Name = buttonName + "MouseRightMenu_Start";
            startItem.Text = "启动";
            startItem.Tag = buttonName;
            startItem.Click += new EventHandler(BtnRightStartClick);
            rightMenu.Items.Add(startItem);
            /* 停止程序按钮 */
            ToolStripMenuItem stopItem = new ToolStripMenuItem();
            // 默认为不可用状态
            stopItem.Enabled = false;
            stopItem.Name = buttonName + "MouseRightMenu_Stop";
            stopItem.Text = "停止";
            stopItem.Tag = buttonName;
            stopItem.Click += new EventHandler(BtnRightStopClick);
            rightMenu.Items.Add(stopItem);
            /* 编辑程信息按钮 */
            ToolStripMenuItem updateItem = new ToolStripMenuItem();
            updateItem.Name = buttonName + "MouseRightMenu_Update";
            updateItem.Text = "编辑";
            updateItem.Tag = buttonName;
            updateItem.Click += new EventHandler(BtnRightUpdateClick);
            rightMenu.Items.Add(updateItem);
            /* 装载右键 */
            button.ContextMenuStrip = rightMenu;
            Projects_Panel.Controls.Add(button);
            /**
            * 数据写进缓存
            * */
            ProjectSections.ProjectSection projectSection = new ProjectSections.ProjectSection();
            projectSection.title = buttonText;
            projectSection.jar = jar;
            projectSection.port = port;
            projectSection.heartBeat = heartBeat;
            ProjectSections.updateProjectSection(buttonName, projectSection);
        }

        public void checkSection(String section, String projectTitle, String port)
        {
            // 校验端口结束进程的bat文件是否存在，不存在
            if (!File.Exists(FileUtils.getBatFilePath(projectTitle)))
            {
                ProjectUtils.createStopBat(projectTitle, port);
            }
            // 识别运行状态
            ProjectSections.ProjectSection projectSection = ProjectSections.getProjectBySection(section);
            if (PortUtils.PortInUse(Convert.ToInt16(port)))
            {
                projectSection.isRunning = true;
                            }
            else
            {
                projectSection.isRunning = false;
            }
            // 根据运行状态右键按钮的可操作性调整
            updateButtonEnabledOfMenuStrip(section, projectSection.isRunning);
        }

        /// <summary>
        /// 调整项目按钮上的右键菜单可用性
        /// </summary>
        /// <param name="section">当前按钮编码</param>
        /// <param name="isRunning">当前项目是否运行</param>
        private void updateButtonEnabledOfMenuStrip(String section, Boolean isRunning)
        {
            Button btn = (Button)Projects_Panel.Controls[section];
            if (isRunning)
            {
                // 按钮背景调整为绿色
                btn.BackColor = Color.LightGreen;
                // 运行中，只能停止操作
                btn.ContextMenuStrip.Items[section + "MouseRightMenu_Start"].Enabled = false;
                btn.ContextMenuStrip.Items[section + "MouseRightMenu_Stop"].Enabled = true;
                btn.ContextMenuStrip.Items[section + "MouseRightMenu_Update"].Enabled = false;
            }
            else
            {
                btn.BackColor = Color.LightGray;
                // 未运行，可启动和编辑
                btn.ContextMenuStrip.Items[section + "MouseRightMenu_Start"].Enabled = true;
                btn.ContextMenuStrip.Items[section + "MouseRightMenu_Stop"].Enabled = false;
                btn.ContextMenuStrip.Items[section + "MouseRightMenu_Update"].Enabled = true;
            }
        }

        /// <summary>
        /// 修改按钮
        /// </summary>
        /// <param name="section"></param>
        public void updateButton(String section)
        {
            Button btn = (Button)Projects_Panel.Controls[section];
            ProjectSections.ProjectSection monitorSection = ProjectSections.getProjectBySection(section);
            String title = monitorSection.title;
            String port = monitorSection.port;
            btn.Text = title;
        }
        // 鼠标移动到按钮事件
        ToolTip toolTip1 = new ToolTip();
        private void BtnMouseHover(Object sender, EventArgs e)
        {
            Button currentBtn = (Button)sender;
            String section = currentBtn.Name;
            ProjectSections.ProjectSection monitorSection = ProjectSections.getProjectBySection(section);
            String title = monitorSection.title;

            // 设置显示样式
            //toolTip1.AutoPopDelay = 5000;//提示信息的可见时间
            toolTip1.InitialDelay = 200;//事件触发多久后出现提示
            toolTip1.ReshowDelay = 0;//指针从一个控件移向另一个控件时，经过多久才会显示下一个提示框
            toolTip1.ShowAlways = true;//是否显示提示框
            //  设置伴随的对象.
            toolTip1.SetToolTip(currentBtn, title);
        }
        /**
      * 右键启动
      * */
        private void BtnRightStartClick(Object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            String section = (String)menuItem.Tag;
            // 按钮置为灰，避免重复点击
            menuItem.Enabled = false;
            ProjectSections.ProjectSection monitorSection = ProjectSections.getProjectBySection(section);
            String jdkPath = JDKPath_TextBox.Text;
            String profile = Profile_ComboBox.SelectedItem.ToString();
            String logPath = monitorSection.jar.Substring(0, monitorSection.jar.LastIndexOf("."));
            ProjectUtils.projectStart(jdkPath, monitorSection.jar, profile, logPath, "info.log", "error.log");
        }
        /**
         * 右键停止
         * */
        private void BtnRightStopClick(Object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            menuItem.Enabled = false;
            String section = (String)menuItem.Tag;
            ProjectSections.ProjectSection projectSection = ProjectSections.getProjectBySection(section);
            // 停止
            ProjectUtils.projectStop(projectSection.title, Convert.ToInt16(projectSection.port));
        }

        /**
        * 右键修改按钮点击事件
        * */
        private void BtnRightUpdateClick(Object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            UpdateForm updateForm = new UpdateForm();
            updateForm.mainForm = this;
            updateForm.section = (String)menuItem.Tag;
            updateForm.ShowDialog();
        }

        private void setButtonBackColor(Button button, Color color)
        {
            button.BackColor = color;
        }

        private void ProjectAdd_Button_Click(object sender, EventArgs e)
        {
            AddForm addForm = new AddForm();
            // 先要把主窗口放以弹出窗口中，以便弹出窗口调用主窗口函数
            addForm.mainForm = this;
            addForm.ShowDialog();
        }

        private void JDKPath_Dialog_Button_Click(object sender, EventArgs e)
        {
            JDKPath_FolderBrowserDialog.SelectedPath = Path.GetDirectoryName(JDKPath_TextBox.Text);
            JDKPath_FolderBrowserDialog.ShowNewFolderButton = false;
            if (JDKPath_FolderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                JDKPath_TextBox.Text = JDKPath_FolderBrowserDialog.SelectedPath;
            }
        }

        private void SystemConfig_Save_Button_Click(object sender, EventArgs e)
        {
            String profile = Profile_ComboBox.Text;
            String JDKPath = JDKPath_TextBox.Text;
            long saveProfileResult = iniUtils.IniWriteValue(Config.SystemIniPath, "system", "profile", profile);
            long saveJDKPathresult = iniUtils.IniWriteValue(Config.SystemIniPath, "system", "JDKPath", JDKPath);
            if(saveProfileResult>0 && saveJDKPathresult > 0)
            {
                MessageBox.Show("保存成功！");
            }
            else
            {
                MessageBox.Show("保存失败，请联系作者！");
            }
            
        }

        private void GCTimer_Tick(object sender, EventArgs e)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                //  配置工作使用空间
                SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        private void MonitorTimer_Tick(object sender, EventArgs e)
        {
            foreach (String section in ProjectSections.getAllSections())
            {
                ProjectSections.ProjectSection projectSection = ProjectSections.getProjectBySection(section);
                String heartBeatUrl = projectSection.heartBeat;
                String result = HttpUtils.postRequest(heartBeatUrl, null, null);
                if ("success".Equals(result))
                {
                    // 运行中
                    updateButtonEnabledOfMenuStrip(section, true);
                }
                else
                {
                    // 未运行
                    updateButtonEnabledOfMenuStrip(section, false);
                }
            }
        }
    }
}
