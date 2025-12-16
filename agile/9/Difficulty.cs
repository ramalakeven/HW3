using System;

namespace QuestJournal
{
    public enum Difficulty
    {
        Trivial = 0,
        Easy = 1,
        Normal = 2,
        Hard = 3,
        Nightmare = 4
    }

    public static class DifficultyExtensions
    {
        public static string ToRussian(this Difficulty difficulty)
        {
            return difficulty switch
            {
                Difficulty.Trivial => "Тривиальная",
                Difficulty.Easy => "Лёгкая",
                Difficulty.Normal => "Нормальная",
                Difficulty.Hard => "Сложная",
                Difficulty.Nightmare => "Кошмарная",
                _ => difficulty.ToString()
            };
        }
    }
}