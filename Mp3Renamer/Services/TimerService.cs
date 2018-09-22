using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Mp3Renamer.Interfaces;

namespace Mp3Renamer.Services
{
    public class TimerService<T> : ITimerService<T>
    {
        public void MeasureExecutionTime(Action<T> func, T parameter)
        {
            var timer = new Stopwatch();
            timer.Start();

            func(parameter);

            timer.Stop();
            Console.WriteLine(timer.ElapsedMilliseconds);
        }

        public async Task MeasureExecutionTimeAsync(Func<T, Task> func, T parameter)
        {
            var timer = new Stopwatch();
            timer.Start();

            await func(parameter);

            timer.Stop();
            Console.WriteLine(timer.ElapsedMilliseconds);
        }
    }
}
