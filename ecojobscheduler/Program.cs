using System;
using System.Collections.Generic;
using System.Text;
using Topshelf;
using ecojobscheduler.common;
using EcobpMQ.TaskSet;

namespace ecojobscheduler
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            HostFactory.Run(x =>
            {
                x.Service<Scheduler>(s =>
                {
                    s.ConstructUsing(name => new Scheduler());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });

                x.RunAsLocalSystem();

                x.SetDescription("Oralce数据库自动备份服务");
                x.SetDisplayName("Oralce自动备份-" + Config.ServiceGroup);
                x.SetServiceName("OracleBackup-" + Config.ServiceGroup);
            });
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {      
            Exception ex = (Exception)e.ExceptionObject;

            LogHelper.WriteLog("未捕捉异常", ex);
        }


    }
}
