using System;
namespace PrintQueueSystem
{
    public class UrgentJob : IJob, IPrioritizable
    {
        private string title;
        private int pages;
        private bool isCanceled;
        private int priority;

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

        public int Priority
        {
            get => priority;
            set => priority = value;
        }

        public void Cancel()
        {
            if (Priority >= 5)
            {
                Console.WriteLine($"Задание '{Title}' имеет высокий приоритет ({Priority}) и не может быть отменено!");
                IsCanceled = false;
            }
            else
            {
                Console.WriteLine($"Задание '{Title}' отменено (приоритет: {Priority}).");
                IsCanceled = true;
            }
        }

        public void Bump()
        {
            Priority++;
            Console.WriteLine($"Приоритет задания '{Title}' повышен до {Priority}.");
        }
    }
}