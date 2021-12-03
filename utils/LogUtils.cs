using PM.config;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace PM.utils
{
    class LogUtils
    {
        public static void writeLog(String log)
        {
            writeLog(log, Config.logFileName);
        }
        /// <summary>
        /// 同步锁写日志文件
        /// </summary>
        /// <param name="log"></param>
        /// <param name="logFile"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void writeLog(String log, String logFile)
        {
            if (!Config.logSwitch)
            {
                return;
            }
            FileStream fs = null;
            StreamWriter sw = null;
            try
            {
                String logFileDirectory = Path.GetDirectoryName(logFile);
                if (!FileUtils.Boo_DirExist(logFileDirectory))
                {
                    // 目录不存在，创建目录
                    FileUtils.DirCreate(logFileDirectory);
                }
                // 验证文件是否存在，有则追加，无则创建
                if (File.Exists(logFile))
                                {
                    fs = new FileStream(@logFile, FileMode.Append, FileAccess.Write);
                }
                else
                {
                    fs = new FileStream(@logFile, FileMode.Create, FileAccess.Write);
                }
                sw = new StreamWriter(fs);
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "   ---   " + log);
            }catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                if (null != sw)
                {
                    sw.Close();
                }
                if (null != fs)
                {
                    fs.Close();
                }
            }
           
        }
    }
}
