using System;
using Kontur.Courses.Testing.Implementations;
using NUnit.Framework;

namespace Kontur.Courses.Testing
{
	public class WordsStatistics_Tests
	{
		public Func<IWordsStatistics> createStat = () => new WordsStatistics_CorrectImplementation(); // меняется на разные реализации при запуске exe
		public IWordsStatistics stat;

		[SetUp]
		public void SetUp()
		{
			stat = createStat();
		}

		[Test]
		public void no_stats_if_no_words()
		{
			CollectionAssert.IsEmpty(stat.GetStatistics());
		}

		[Test]
		public void same_word_twice()
		{
			stat.AddWord("xxx");
			stat.AddWord("xxx");
			CollectionAssert.AreEqual(new[] { Tuple.Create(2, "xxx") }, stat.GetStatistics());
		}

		[Test]
		public void single_word_Upper()
		{
			stat.AddWord("HELLO");
			CollectionAssert.AreEqual(new[] { Tuple.Create(1, "hello") }, stat.GetStatistics());
		}

		[Test]
		public void two_same_words_one_other()
        {
            stat.AddWord("hello");
            stat.AddWord("world");
			stat.AddWord("world");
			CollectionAssert.AreEqual(new[] { Tuple.Create(2, "world"), Tuple.Create(1, "hello") }, stat.GetStatistics());
		}
        
        [Test]
        public void two_same_words_two_other()
        {
            stat.AddWord("world");
            stat.AddWord("world");

            stat.AddWord("hello");
            stat.AddWord("hello");
            CollectionAssert.AreEqual(new[] { Tuple.Create(2, "hello"), Tuple.Create(2, "world") }, stat.GetStatistics());
        }

        [Test]
        public void two_words_more_10_symb()
        {
            stat.AddWord("hellohellohellohello");
            stat.AddWord("hellohellohellohello");
            CollectionAssert.AreEqual(new[] { Tuple.Create(2, "hellohello")}, stat.GetStatistics());
        }

        [Test]
        public void with_Empty_String_arg()
        {
            stat.AddWord("");
            CollectionAssert.IsEmpty(stat.GetStatistics());
        }

        [Test]
        public void with_Null_argument()
        {
            stat.AddWord(null);
            CollectionAssert.IsEmpty(stat.GetStatistics());
        }

        [Test]
        public void one_word_with_11_symbols()
        {

            stat.AddWord("hellohelloh");

            CollectionAssert.AreEqual(new[] { Tuple.Create(1, "hellohello") }, stat.GetStatistics());

        }
    }
}