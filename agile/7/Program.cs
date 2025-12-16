using System;

namespace ReportProcessing
{
    class Program
    {
        static void Main()
        {
            var context = new ReportContext
            {
                RowsCount = 100,
                HasSummary = false,
                IsFormatted = false
            };

            var pipeline = new ReportPipeline();

            pipeline.Steps += AddSummary;
            pipeline.Steps += FormatReport;
            pipeline.Steps += ctx => ctx.RowsCount -= 10;

            Console.WriteLine("Первый запуск:");
            pipeline.Run(context);
            Print(context);

            pipeline.Steps -= FormatReport;

            Console.WriteLine("\nВторой запуск:");
            pipeline.Run(context);
            Print(context);
        }

        static void AddSummary(ReportContext context)
        {
            context.HasSummary = true;
        }

        static void FormatReport(ReportContext context)
        {
            context.IsFormatted = true;
        }

        static void Print(ReportContext ctx)
        {
            Console.WriteLine($"RowsCount: {ctx.RowsCount}");
            Console.WriteLine($"HasSummary: {ctx.HasSummary}");
            Console.WriteLine($"IsFormatted: {ctx.IsFormatted}");
        }
    }
}
