using PM.config;
using System;
using System.IO;

namespace PM.utils
{
    class FileUtils
    {
        public static String getBatFilePath(String projectTitle, String type)
        {
            String batTypePath;
            if (Config.BAT_FILE_TYPE_START.Equals(type))
            {
                batTypePath = Config.BAT_FILE_NAME_START;
            }
            else if(Config.BAT_FILE_TYPE_STOP.Equals(type))
            {
                batTypePath = Config.BAT_FILE_NAME_STOP;
            }
            else
            {
                batTypePath = "";
            }
            return Config.BatPath + projectTitle + batTypePath;
        }
        public static Boolean Boo_DirExist(String DirPath)
        {
            return System.IO.Directory.Exists(@DirPath);
        }

        public static Boolean Boo_FileExist(String FilePath)
        {
            return System.IO.File.Exists(@FilePath);
        }

        public static void DirCreate(String DirPath)
        {
            System.IO.Directory.CreateDirectory(@DirPath);
        }

        public static void FileCreate(String FilePath)
        {
            System.IO.File.Create(@FilePath);
        }

        public static void createFile(String filePath, String content)
        {
            String directPath = Path.GetDirectoryName(filePath);
            if (!Boo_DirExist(directPath))
            {
                DirCreate(directPath);
            }
            File.WriteAllText(filePath, content);
        }
    }
}
