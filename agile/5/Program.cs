using System;

namespace PrintQueueSystem
{
    class Program
    {
        static void Main()
        {
            SimpleJob simpleJob = new SimpleJob { Title = "Документ 1", Pages = 10 };
            simpleJob.Cancel();
            Console.WriteLine($"'{simpleJob.Title}' отменён? {simpleJob.IsCanceled}\n");

            UrgentJob urgentLow = new UrgentJob { Title = "Срочный отчёт", Pages = 5, Priority = 3 };
            urgentLow.Cancel();
            Console.WriteLine($"'{urgentLow.Title}' отменён? {urgentLow.IsCanceled}");

            urgentLow.Bump();
            urgentLow.Bump();
            Console.WriteLine($"Текущий приоритет: {urgentLow.Priority}");

            urgentLow.Bump(); 
            urgentLow.Cancel();
            Console.WriteLine($"'{urgentLow.Title}' отменён? {urgentLow.IsCanceled}");
        }
    }
}
