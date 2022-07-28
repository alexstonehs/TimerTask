using System;
using System.Collections.Generic;
using System.Text;

namespace TaskTimerApp
{
    /// <summary>
    /// Main execute class, use this class to initialize the frequently jobs
    /// </summary>
    public class TaskExecute
    {
        private TimerTask _timerTask = new TimerTask();

        public delegate void TaskExecuteEventHandler(TaskType type);

        public event TaskExecuteEventHandler TaskExecuteEvent;
        public TaskExecute()
        {
            _timerTask.MinRunEvent += _timerTask_MinRunEvent;
        }
        public void StartRun()
        {
            _timerTask.Start();
        }

        private void _timerTask_MinRunEvent(DateTime time)
        {
            if (time.Second == 0 && time.Minute == 0 && time.Hour == 0) //发送日数据
            {
                TaskExecuteEvent?.Invoke(TaskType.DailyTask);
            }
            else if (time.Second == 0 && time.Minute == 0) //发送小时数据
            {
                TaskExecuteEvent?.Invoke(TaskType.HourTask);
            }
            else if (time.Second == 0 && time.Minute % 10 == 0)
            {
                TaskExecuteEvent?.Invoke(TaskType.TenMinTask);
            }
            else if (time.Second == 0 && time.Minute % 5 == 0) //发送5分钟数据
            {
                TaskExecuteEvent?.Invoke(TaskType.FiveMinTask);
            }
            else if (time.Second == 0) //发送分钟数据
            {
                TaskExecuteEvent?.Invoke(TaskType.MinTask);
            }
        }
    }
}
