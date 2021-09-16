using System;
using System.Net;
using System.Net.NetworkInformation;

namespace PM.utils
{
    class PortUtils
    {
        public static Boolean PortInUse(Int16 port)
        {
            try
            {
                // 查看UDP端口是否被占用
                IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
                IPEndPoint[] ipEndUdpPoints = ipProperties.GetActiveUdpListeners();
                foreach (IPEndPoint ipEndUdpPoint in ipEndUdpPoints)
                {
                    if (ipEndUdpPoint.Port == port)
                    {
                        return true;
                    }
                }

                // 查看TCP端口是否被占用
                IPEndPoint[] ipEndTcpPoints = ipProperties.GetActiveTcpListeners();
                foreach (IPEndPoint endPoint in ipEndTcpPoints)
                {
                    if (endPoint.Port == port)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtils.writeLog(ex.Message);
                return false;
            }
        }
    }
}
