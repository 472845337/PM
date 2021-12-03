using System;

namespace PM.config
{
    class Config
    {
        public static String ProjectsIniPath = "ini/Projects.ini";
        public static String SystemIniPath = "ini/System.ini";
        public static String BatPath = "bat/";
        public static String BAT_FILE_TYPE_START = "start";
        public static String BAT_FILE_NAME_START = "_start.bat";
        public static String BAT_FILE_TYPE_STOP = "stop";
        public static String BAT_FILE_NAME_STOP = "_stop.bat";
        public static String LOG_FILE_INFO = "info.log";
        public static String LOG_FILE_ERROR = "error.log";
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
