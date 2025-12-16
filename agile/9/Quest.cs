using System;
using System.Collections.Generic;

namespace QuestJournal
{
    public sealed class Quest
    {
        private readonly List<Objective> _objectives;

        public string Id { get; }
        public string Title { get; }
        public Difficulty Difficulty { get; set; }
        public IReadOnlyList<Objective> Objectives => _objectives.AsReadOnly();

        public Quest(string id, string title, Difficulty difficulty, IEnumerable<Objective> initialObjectives = null)
        {
            if (id is null) throw new ArgumentNullException(nameof(id));
            if (title is null) throw new ArgumentNullException(nameof(title));
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("Id не может быть пустым.", nameof(id));
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title не может быть пустым.", nameof(title));

            Id = id;
            Title = title;
            Difficulty = difficulty;
            _objectives = initialObjectives != null ? new List<Objective>(initialObjectives) : new List<Objective>();
        }

        internal void AddObjective(Objective objective)
        {
            if (objective is null) throw new ArgumentNullException(nameof(objective));
            _objectives.Add(objective);
        }

        public override string ToString()
        {
            var objs = string.Join("; ", _objectives);
            return $"{Title} [{Difficulty.ToRussian()}] {_objectives.Count} задачи квеста: {objs}";
        }
    }
}