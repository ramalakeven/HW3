using System;

namespace ScoreCounterApp
{
    public class ComboStats
    {
        private int _largePickups = 0;
        private int _lastScore = 0;

        public void OnScoreChanged(ScoreMonitor sender, int score)
        {
            int delta = score - _lastScore;

            if (delta >= 30)
            {
                _largePickups++;
            }

            _lastScore = score;
        }

        public void Report()
        {
            Console.WriteLine($"Крупных подборов (больше 30) было {_largePickups}");
        }
    }
}