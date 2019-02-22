using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.IO;
using System.Text;
using log4net;

namespace ecojobscheduler.common
{   

    public class LogHelper
    {
        
        
        static LogHelper()
        {   
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"log4net.config");

            FileInfo file = new FileInfo(path);
            SetConfig(file);
        }
                
        private static void SetConfig(FileInfo configFile)
        {   
            log4net.Config.XmlConfigurator.Configure(configFile);
        }

        private static readonly log4net.ILog loginfo = log4net.LogManager.GetLogger("loginfo");

        private static readonly log4net.ILog logerror = log4net.LogManager.GetLogger("logerror");

        public static void WriteLog(string info)
        {
            if (loginfo.IsInfoEnabled)
            {
                loginfo.Info(info);
            }
        }

        public static void WriteLog(string info, Exception se)
        {
            if (logerror.IsErrorEnabled)
            {
                logerror.Error(info, se);
            }
        }
                
        /// <summary>
        /// ��¼�ӿڵ�������Ϣ
        /// </summary>
        /// <param name="description">������Ϣ</param>
        /// <param name="args">����</param>
        public static void LogInput(string description,params object[] args)
        {   
            string IsLogInput = "true";

            if (IsLogInput.ToLower() == "true")
            {   
                StringBuilder sbText = new StringBuilder();

                sbText.AppendLine("������Ϣ:" + description);

                string className = System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name;
                sbText.AppendLine("���÷���:" + className);

                int index = 1;
                foreach (object arg in args)
                {   
                    if(arg == null)
                        sbText.AppendLine("����" + index + ": null ");
                    else
                        sbText.AppendLine("����"+ index +":" + Newtonsoft.Json.JsonConvert.SerializeObject(arg));
                    index += 1;
                }
                WriteLog(sbText.ToString());
            }
        }
    }
}
