using System;
using System.Collections.Generic;

namespace PM.config
{
    class ProjectSections
    {
        private static List<String> sections;
        private static Dictionary<String, ProjectSection> dictionarys;


        public static void clear()
        {
            sections = null;
            dictionarys = null;
        }
        public static List<String> getAllSections()
        {
            return sections;
        }

        public static ProjectSection getProjectBySection(String section)
        {
            if(null == dictionarys)
            {
                return null;
            }
            else
            {
                if (dictionarys.ContainsKey(section))
                {
                    return dictionarys[section];
                }
                else
                {
                    return null;
                }
                
            }
        }

        public static void removeProjectBySection(String section)
        {
            if (null != dictionarys)
            {
                if (dictionarys.ContainsKey(section))
                {
                    dictionarys.Remove(section);
                }
            }
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="section"></param>
        /// <param name="monitor"></param>
        public static void updateProjectSection(String section, ProjectSection projectSection)
        {
            if (dictionarys == null)
            {
                dictionarys = new Dictionary<string, ProjectSection>();
                sections = new List<String>();
            }
            bool isExist = dictionarys.ContainsKey(section);
            if (isExist)
            {
                // 已经存在，修改原数据
                dictionarys[section] = projectSection;
            }
            else
            {
                dictionarys.Add(section, projectSection);
                sections.Add(section);
            }
        }
        public class ProjectSection
        {
            public String title { get; set; }
            public String jar { get; set; }
            public String port { get; set; }
            public String heartBeat { get; set; }
            public Boolean isRunning { get; set; }
        }
    }
}
