using PM.config;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace PM.utils
{
    class HttpUtils
    {
        private static readonly string DefaultUserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
        public static String CONTENT_TYPE_APPLICATION_JSON = "application/json";
        public static String postRequest(String url, String data, String contentType)
        {
            //定义request并设置request的路径
            HttpWebRequest request = HttpWebRequest.CreateHttp(url);
            // 设置超时时间
            request.Timeout = Config.timeout * 1000;
            //定义请求的方式
            request.Method = "POST";
            request.UserAgent = DefaultUserAgent;
            //设置request的MIME类型及内容长度
            request.ContentType = StringUtils.isEmpty(contentType)? "application/x-www-form-urlencoded":contentType;
            request.KeepAlive = false;
            //定义response为前面的request响应
            HttpWebResponse response = null;
            Stream dataStream = null;
            StreamReader reader = null;
            string responseFromServer = "";
            try
            {
                Stream writer = request.GetRequestStream();
                byte[] dataArray = null == data ? new Byte[] { } : Encoding.UTF8.GetBytes(data);
                writer.Write(dataArray, 0, dataArray.Length);
                writer.Flush(); 

                response = (HttpWebResponse)request.GetResponse();

                //定义response字符流
                dataStream = response.GetResponseStream();
                reader = new StreamReader(dataStream);
                responseFromServer = reader.ReadToEnd();//读取所有
                // LogUtils.writeLog(responseFromServer);
            }
            catch (Exception e)
            {
                // LogUtils.writeLog(e.Message);
            }
            finally
            {
                //关闭资源
                if (null != reader)
                {
                    reader.Close();
                }
                if ( null != dataStream)
                {
                    dataStream.Close();
                }
                if (null != response)
                {
                    response.Close();
                }
            }
            
            return responseFromServer;
        }
    }
}
