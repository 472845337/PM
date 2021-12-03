using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace PM.utils
{
    class ProjectUtils
    {
        /** 项目启动
         * 
         * */
        public static void projectStart(String jdkPath, String ProjectPath, String Profile, int port, String LogFilePath, String LogFileName, String ErrorLogFileName)
        {
            Boolean DirIsExist = FileUtils.Boo_DirExist(LogFilePath);
            if (!DirIsExist)
            {
                FileUtils.DirCreate(LogFilePath);
            }

            Boolean JDKPathIsExist = FileUtils.Boo_DirExist(jdkPath);
            StringBuilder Java = new StringBuilder("java");
            if (JDKPathIsExist)
            {
                Java = new StringBuilder("\"").Append(jdkPath).Append("\\bin\\java.exe\"");
            }
            String runScript = Java.Append(" -jar ").Append(ProjectPath).Append(" --spring.profiles.active=").Append(Profile).Append(" --server.port=").Append(port).Append(" > ").Append(LogFilePath).Append(LogFileName).Append(" 2>").Append(LogFilePath).Append(ErrorLogFileName).Append(" &").ToString();
            String[] runScriptArray = new string[] { "C:", runScript };
            ProcessUtils processUtils = new ProcessUtils(runScriptArray);
            Thread thread = new Thread(processUtils.runScript);
            thread.Start();
        }

        /**
         * 系统结束
         * 
         */
        public static void projectStop(String title, Int16 port)
        {
            Process.Start(FileUtils.getBatFilePath(title));
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
            FileUtils.createFile(FileUtils.getBatFilePath(title), script.ToString());
        }
    }
}
