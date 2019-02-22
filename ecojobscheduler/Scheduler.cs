using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentScheduler;

namespace ecojobscheduler
{
    class Scheduler
    {   
        /// <summary>
        /// 启动定时任务
        /// </summary>
        public void Start()
        {   
            JobManager.Initialize(new ApiJobRegistry());
        }

        /// <summary>
        /// 停止定时任务
        /// </summary>
        public void Stop()
        {
            JobManager.Stop();
        }
    }
}
