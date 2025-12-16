using System;

namespace ScoreCounterApp
{
    public class ConsoleAnnouncer
    {
        public void OnScoreChanged(ScoreMonitor sender, int score)
        {
            Console.WriteLine($"Счёт: {score}");
        }

        public void OnMilestoneReached(object? sender, int milestone)
        {
            Console.WriteLine($"Достигнут рубеж: {milestone}");
        }
    }
}