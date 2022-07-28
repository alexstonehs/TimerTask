using System;

namespace TaskTimerApp
{
    internal class Program
    {
        /// <summary>
        /// console test
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var t = new TaskExecute();
            t.TaskExecuteEvent += T_TaskExecuteEvent;
            t.StartRun();
            Console.ReadLine();
        }
        /// <summary>
        /// use event type to run specific jobs.
        /// </summary>
        /// <param name="type"></param>
        private static void T_TaskExecuteEvent(TaskType type)
        {
            Console.WriteLine(type.ToString());
        }
    }
}
