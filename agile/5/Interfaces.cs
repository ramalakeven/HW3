using System;
namespace PrintQueueSystem
{
    public interface IJob
    {
        string Title { get; set; }
        int Pages { get; set; }
        bool IsCanceled { get; set; }

        void Cancel()
        {
            IsCanceled = true;
        }
    }

    public interface IPrioritizable
    {
        int Priority { get; set; }
        void Bump();
    }
}