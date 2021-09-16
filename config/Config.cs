using System;

namespace PM.config
{
    class Config
    {
        public static String ProjectsIniPath = "ini/Projects.ini";
        public static String SystemIniPath = "ini/System.ini";
        public static String logFileName;
        public static String AppPath = System.AppDomain.CurrentDomain.BaseDirectory;

        public static bool logSwitch;
        // 主窗口，用于其它窗口或方法中
        public static MainForm mainForm;
        // 单位是秒
        public static int interval;
        // 单位是秒
        public static int timeout;
    }
}
