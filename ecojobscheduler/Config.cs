using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcobpMQ.TaskSet
{
    public class Config
    {

        public static string ServiceGroup
        {
            get
            {   
                return ConfigInit.GetValue("ServiceGroup");
            }
        }

        public static string DbUser
        {
            get
            {
                return ConfigInit.GetValue("DbUser");
            }
        }
        public static string DbPassword
        {
            get
            {
                return ConfigInit.GetValue("DbPassword");
            }
        }
        public static string BackupDirectory
        {
            get
            {   
                return ConfigInit.GetValue("BackupDirectory");
            }
        }
        public static int StartHour
        {
            get
            {
                return int.Parse(ConfigInit.GetValue("StartHour"));
            }
        }

        public static int StartMinute
        {
            get
            {   
                return int.Parse(ConfigInit.GetValue("StartMinute"));
            }
        }

        public static int Maintain
        {   
            get
            {      
                return int.Parse(ConfigInit.GetValue("Maintain"));
            }
        }
        
    }
}
