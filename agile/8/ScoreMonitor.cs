using System;

namespace ScoreCounterApp
{
    public class ScoreMonitor
    {
        public delegate void ScoreEventHandler(ScoreMonitor sender, int score);

        public event ScoreEventHandler? ScoreChanged;

        private EventHandler<int>? _milestoneReached;

        public event EventHandler<int> MilestoneReached
        {
            add
            {
                _milestoneReached += value;
                Console.WriteLine("Подписчик добавлен к MilestoneReached");
            }
            remove
            {
                _milestoneReached -= value;
                Console.WriteLine("Подписчик удалён из MilestoneReached");
            }
        }

        private int _score = 0;
        private int _lastMilestone = 0;
        private readonly Random _random = new Random();

        public void Start()
        {
            int pickupsCount = _random.Next(10, 15);

            for (int i = 0; i < pickupsCount; i++)
            {
                int add = _random.Next(5, 51);
                int previousScore = _score;

                _score += add;

                ScoreChanged?.Invoke(this, _score);

                int previousHundreds = previousScore / 100;
                int currentHundreds = _score / 100;

                for (int h = previousHundreds + 1; h <= currentHundreds; h++)
                {
                    int milestone = h * 100;
                    _milestoneReached?.Invoke(this, milestone);
                }
            }
        }
    }
}