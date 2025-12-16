using System;

namespace QuestJournal
{
    internal static class Program
    {
        private static void Main()
        {
            var log = new QuestLog();

            var q1 = new Quest(
                id: "q_kill_wolves",
                title: "Убить волков",
                difficulty: Difficulty.Easy,
                initialObjectives: new[] { new Objective("kill_wolves", "Убить 5 волков", 5) }
            );

            var q2 = new Quest(
                id: "q_gather_herbs",
                title: "Собрать травы",
                difficulty: Difficulty.Trivial
            );
            q2.AddObjective(new Objective("gather_herbs", "Собрать 10 трав", 10));

            var q3 = new Quest(
                id: "q_slay_dragon",
                title: "Убить дракона",
                difficulty: Difficulty.Nightmare,
                initialObjectives: new[]
                {
                    new Objective("find_dragon_lair", "Найти логово дракона"),
                    new Objective("slay_dragon", "Убить дракона", 1)
                }
            );

            log.Add(q1);
            log.Add(q2);
            log.Add(q3);

            Console.WriteLine("Все квесты в журнале:");
            for (int i = 0; i < log.Count; i++)
            {
                Console.WriteLine($"[{i}] {log[i]}");
            }

            Console.WriteLine();
            Console.WriteLine($"Квесты со сложностью не ниже {Difficulty.Normal.ToRussian()}:");
            foreach (var q in log.EnumerateByDifficulty(Difficulty.Normal))
            {
                Console.WriteLine(q);
            }

            Console.WriteLine();
            Console.WriteLine("Доступ по Id:");
            try
            {
                var byId = log["q_kill_wolves"];
                Console.WriteLine(byId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка доступа по id: {ex.Message}");
            }

            Console.WriteLine();
            Console.WriteLine("Удаляем квест по Id 'q_gather_herbs'...");
            var removedById = log.RemoveById("q_gather_herbs");
            Console.WriteLine($"Удалено: {removedById}, текущий Count = {log.Count}");

            Console.WriteLine("Удаляем квест по позиции 0...");
            var removedAt = log.RemoveAt(0);
            Console.WriteLine($"Удалено: {removedAt}, текущий Count = {log.Count}");

            Console.WriteLine();
            Console.WriteLine("Оставшиеся квесты:");
            foreach (var q in log)
            {
                Console.WriteLine(q);
            }

            Console.WriteLine();
            Console.WriteLine("Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}