using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PM.utils
{
    class FileUtils
    {
        public static String getBatFilePath(String projectTitle)
        {
            return "bat/" + projectTitle + ".bat";
        }
        public static Boolean Boo_DirExit(String DirPath)
        {
            return System.IO.Directory.Exists(@DirPath);
        }

        public static Boolean Boo_FileExit(String FilePath)
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
            if (!Boo_DirExit(directPath))
            {
                DirCreate(directPath);
            }
            File.WriteAllText(filePath, content);
        }
    }
}
