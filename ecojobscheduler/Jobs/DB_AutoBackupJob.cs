using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentScheduler;
using ecojobscheduler.common;

namespace EcobpMQ.TaskSet
{

    /// <summary>
    /// 数据库自动备份任务
    /// </summary>
    ///<remarks>DisallowConcurrentExecution属性标记任务不可并行，要是上一任务没运行完即使到了运行时间也不会运行</remarks>
    public class DB_AutoBackupJob : IJob
    {

        private string _script = @"/c exp {username}/{password} file={directory}\bak_{username}_{datetime}.dmp owner={username}" + " 2>&1";
        public string Script
        {
            get
            {   
                return _script;
            }
            set
            {   
                _script = value;
            }
        }

        /// <summary>
        /// IJob 接口
        /// </summary>
        /// <param name="context"></param>
        public void Execute()
        {

            try
            {   
                this.Script = this.Script.Replace("{username}", Config.DbUser);
                this.Script = this.Script.Replace("{password}", Config.DbPassword);
                this.Script = this.Script.Replace("{directory}", Config.BackupDirectory);
                
                this.Script = this.Script.Replace("{datetime}", DateTime.Now.ToString("yyyyMMdd_HHmm"));

                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm") + " : 开始备份 》》》");

                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.Arguments = this.Script;

                p.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
                p.StartInfo.RedirectStandardInput = false;//接受来自调用程序的输入信息
                p.StartInfo.RedirectStandardOutput = false;//由调用程序获取输出信息
                p.StartInfo.RedirectStandardError = false;//重定向标准错误输出
                p.StartInfo.CreateNoWindow = true;//不显示程序窗口
                p.Start();

                //向cmd窗口发送输入信息
                //p.StandardInput.WriteLine(this.Script);
                //p.StandardInput.WriteLine("exit");

                //p.StandardInput.WriteLine(this.Script + "&exit");
                //p.StandardInput.AutoFlush = true;
                //string output = p.StandardOutput.ReadToEnd();

                p.WaitForExit(1000 * 60 * 5);

                p.Close();
                
                //Console.WriteLine(output);

                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm") + " : 备份完成 《《《");

                LogHelper.WriteLog("自动备份完成");

            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("自动备份异常", ex);
            }
        }


    }
}
