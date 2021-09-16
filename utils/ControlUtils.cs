using PM.config;
using System;
using System.Collections;
using System.Windows.Forms;

namespace PM.utils
{
    class ControlUtils
    {
        /// <summary>
        /// 根据指定容器和控件名字，获得控件
        /// </summary>
        /// <param name="form">窗体</param>
        /// <param name="obj">容器</param>
        /// <param name="strControlName">控件名字</param>
        /// <returns>控件</returns>
        public static object GetControlInstance(object obj, string strControlName)
        {
            IEnumerator Controls = null;//所有控件
            Control c = null;//当前控件
            Object cResult = null;//查找结果
            if (obj.GetType() == Config.mainForm.GetType())//窗体
            {
                Controls = Config.mainForm.Controls.GetEnumerator();
            }
            else//控件
            {
                Controls = ((Control)obj).Controls.GetEnumerator();
            }
            while (Controls.MoveNext())//遍历操作
            {
                c = (Control)Controls.Current;//当前控件
                if (c.HasChildren)//当前控件是个容器
                {
                    cResult = GetControlInstance(c, strControlName);//递归查找
                    if (cResult == null)//当前容器中没有，跳出，继续查找
                        continue;
                    else
                    {
                        //找到控件，跳出循环
                        break;
                    }
                }
                else if (c.Name == strControlName)//不是容器，同时找到控件，返回
                {
                    cResult = c;
                    break;
                }
            }
            return cResult;//控件不存在
        }
    }
}
