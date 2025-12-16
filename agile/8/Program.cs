using System;

namespace ScoreCounterApp
{
    class Program
    {
        static void Main()
        {
            var monitor = new ScoreMonitor();
            var announcer = new ConsoleAnnouncer();
            var comboStats = new ComboStats();

            monitor.ScoreChanged += announcer.OnScoreChanged;
            monitor.ScoreChanged += comboStats.OnScoreChanged;
            monitor.MilestoneReached += announcer.OnMilestoneReached;

            monitor.Start();

            monitor.MilestoneReached -= announcer.OnMilestoneReached;

            comboStats.Report();

            Console.ReadLine();
        }
    }
}
