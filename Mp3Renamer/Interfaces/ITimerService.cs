using System;
using System.Threading.Tasks;

namespace Mp3Renamer.Interfaces
{
    public interface ITimerService<T>
    {
        void MeasureExecutionTime(Action<T> func, T parameter);

        Task MeasureExecutionTimeAsync(Func<T, Task> func, T parameter);
    }
}
