using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentScheduler;
using EcobpMQ.TaskSet;

namespace ecojobscheduler
{
     class ApiJobRegistry : Registry
    {
        public ApiJobRegistry()
        {   
            int startHour = Config.StartHour;
            int startMinute = Config.StartMinute;

            Schedule<EcobpMQ.TaskSet.DB_AutoBackupJob>().ToRunEvery(1).Days().At(startHour, startMinute);

        }
    
    }
}
