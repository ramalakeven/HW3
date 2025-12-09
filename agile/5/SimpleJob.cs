using System;
namespace PrintQueueSystem
{
    public class SimpleJob : IJob
    {
        private string title;
        private int pages;
        private bool isCanceled;

        public string Title
        {
            get => title;
            set => title = value;
        }

        public int Pages
        {
            get => pages;
            set => pages = value;
        }

        public bool IsCanceled
        {
            get => isCanceled;
            set => isCanceled = value;
        }
        void IJob.Cancel() => IsCanceled = true;
        public void Cancel() => ((IJob)this).Cancel();

    }
}
