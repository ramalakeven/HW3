using System;
using System.Collections;
using System.Collections.Generic;

namespace QuestJournal
{
    public sealed class QuestLog : IEnumerable<Quest>
    {
        private readonly List<Quest> _quests = new List<Quest>();
        private readonly Dictionary<string, Quest> _byId = new Dictionary<string, Quest>(StringComparer.Ordinal);

        public int Count => _quests.Count;

        public Quest this[int index]
        {
            get
            {
                if (index < 0 || index >= _quests.Count)
                    throw new ArgumentOutOfRangeException(nameof(index), "Неверный индекс.");
                return _quests[index];
            }
        }

        public Quest this[string id]
        {
            get
            {
                if (id is null) throw new ArgumentNullException(nameof(id));
                if (!_byId.TryGetValue(id, out var quest))
                    throw new KeyNotFoundException($"квест id '{id}' не найден.");
                return quest;
            }
        }

        public void Add(Quest quest)
        {
            if (quest is null) throw new ArgumentNullException(nameof(quest));
            if (quest.Id is null) throw new ArgumentNullException(nameof(quest.Id));
            if (_byId.ContainsKey(quest.Id))
                throw new ArgumentException($"Квест id '{quest.Id}' уже существует.", nameof(quest));

            _quests.Add(quest);
            _byId.Add(quest.Id, quest);
        }

        public bool RemoveAt(int index)
        {
            if (index < 0 || index >= _quests.Count) return false;

            var quest = _quests[index];
            _quests.RemoveAt(index);
            _byId.Remove(quest.Id);
            return true;
        }

        public bool RemoveById(string id)
        {
            if (id is null) throw new ArgumentNullException(nameof(id));
            if (!_byId.TryGetValue(id, out var quest)) return false;

            _byId.Remove(id);
            for (int i = 0; i < _quests.Count; i++)
            {
                if (ReferenceEquals(_quests[i], quest) || _quests[i].Id == id)
                {
                    _quests.RemoveAt(i);
                    break;
                }
            }
            return true;
        }

        public IEnumerable<Quest> EnumerateByDifficulty(Difficulty minDifficulty)
        {
            foreach (var q in _quests)
            {
                if (q.Difficulty >= minDifficulty) yield return q;
            }
        }

        public IEnumerator<Quest> GetEnumerator() => _quests.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}