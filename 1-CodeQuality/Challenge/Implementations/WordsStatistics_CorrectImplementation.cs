using System;
using System.Collections.Generic;
using System.Linq;

namespace Kontur.Courses.Testing.Implementations
{
	public class WordsStatistics_CorrectImplementation : IWordsStatistics
	{
		private IDictionary<string, int> stats = new Dictionary<string, int>();

		public void AddWord(string word)
		{
			if (string.IsNullOrEmpty(word)) return;
			if (word.Length > 10) word = word.Substring(0, 10);
			int count;
			stats[word.ToLower()] = stats.TryGetValue(word.ToLower(), out count) ? count + 1 : 1;
		}

		/**
		<summary>
		Частотный словарь добавленных слов. 
		Слова сравниваются без учета регистра символов. 
		Порядок — по убыванию частоты слова.
		При одинаковой частоте — в лексикографическом порядке.
		</summary>
		*/
		public IEnumerable<Tuple<int, string>> GetStatistics()
		{
			return stats.OrderByDescending(kv => kv.Value)
				.ThenBy(kv => kv.Key)
				.Select(kv => Tuple.Create(kv.Value, kv.Key));
		}
	}
}