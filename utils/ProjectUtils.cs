using PM.config;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PM.utils
{
    class ProjectUtils
    {
        public static String profile;
        public static String jdkPath;
        /** 项目启动
         * 
         * */
        public static void projectStart(String projectTitle)
        {
            Process.Start(FileUtils.getBatFilePath(projectTitle, Config.BAT_FILE_TYPE_START));
        }
        /**
         * 创建杀进程的脚本文件
         * 
         * */
        public static void createStartBat(String projectTitle, String jarPath, int port, String LogFilePath, String LogFileName, String ErrorLogFileName)
        {
            if(null == profile || "".Equals(profile))
            {
                MessageBox.Show("运行环境未配置或未保存！");
                return;
            }
            if (null == jdkPath || "".Equals(jdkPath))
            {
                MessageBox.Show("JDK未配置或未保存！");
                return;
            }
            if (!FileUtils.Boo_DirExist(jdkPath))
            {
                MessageBox.Show("JDK路径不存在，请重新配置！");
                return;
            }
            StringBuilder script = new StringBuilder();
            Boolean DirIsExist = FileUtils.Boo_DirExist(LogFilePath);
            if (!DirIsExist)
            {
                FileUtils.DirCreate(LogFilePath);
            }
            // jdk路径
            Boolean JDKPathIsExist = FileUtils.Boo_DirExist(jdkPath);
            if (JDKPathIsExist)
            {
                script.Append("\"").Append(jdkPath).Append("\\bin\\java.exe\"");
            }
            // jar包路径
            script.Append(" -jar ").Append(jarPath);
            // profile配置
            script.Append(" --spring.profiles.active=").Append(profile);
            // 指定启动端口
            script.Append(" --server.port=").Append(port);
            // 日志文件
            script.Append(" > ").Append(LogFilePath).Append(LogFileName);
            // 异常日志
            script.Append(" 2>").Append(LogFilePath).Append(ErrorLogFileName).Append(" &").ToString();
            script.Append("\r\n");
            // 窗口不关闭
            script.Append("pause");
            FileUtils.createFile(FileUtils.getBatFilePath(projectTitle, Config.BAT_FILE_TYPE_START), script.ToString());
        }

        /**
         * 系统结束
         * 
         */
        public static void projectStop(String title)
        {
            Process.Start(FileUtils.getBatFilePath(title, Config.BAT_FILE_TYPE_STOP));
        }

        /**
         * 创建杀进程的脚本文件
         * 
         * */
        public static void createStopBat(String title, String port)
        {
            StringBuilder script = new StringBuilder();
            script.Append("@echo off\r\n");
            script.Append("setlocal enabledelayedexpansion\r\n");
            script.Append("for /f \"tokens=1-5\" %%a in ('netstat -ano ^| find \":").Append(port).Append("\"') do (\r\n");
            script.Append("    if \"%%e%\" == \"\" (\r\n");
            script.Append("        set pid=%%d\r\n");
            script.Append("    ) else (\r\n");
            script.Append("        set pid=%%e\r\n");
            script.Append("    )\r\n");
            script.Append("    echo will kill java progress:!pid!\r\n");
            script.Append("    taskkill -f -pid !pid!\r\n");
            script.Append("    echo kill java progress success\r\n");
            script.Append(")\r\n");
            FileUtils.createFile(FileUtils.getBatFilePath(title, Config.BAT_FILE_TYPE_STOP), script.ToString());
        }
    }
}
