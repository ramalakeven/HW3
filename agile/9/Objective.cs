using System;

namespace QuestJournal
{
    public sealed class Objective
    {
        public string Code { get; }
        public string Description { get; }
        public int RequiredCount { get; }

        public Objective(string code, string description, int requiredCount = 1)
        {
            if (code is null) throw new ArgumentNullException(nameof(code));
            if (string.IsNullOrWhiteSpace(code)) throw new ArgumentException("Не может быть пустым.", nameof(code));
            if (requiredCount < 1) throw new ArgumentOutOfRangeException(nameof(requiredCount), "RequiredCount должен быть >= 1.");

            Code = code;
            Description = description ?? string.Empty;
            RequiredCount = requiredCount;
        }

        public override string ToString() => $"{Description} ({RequiredCount})";
    }
}