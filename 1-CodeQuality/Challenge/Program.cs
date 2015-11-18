using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using Kontur.Courses.Testing.Implementations;
using NUnit.Framework;

namespace Kontur.Courses.Testing
{
	class Program
	{
		static void Main()
		{
			if (!CheckTests()) return;
			var implementations = GetImplementations();
			CheckIncorrectImplementationsFail(implementations);
		}

		private static void CheckIncorrectImplementationsFail(IEnumerable<Type> implementations)
		{
			foreach (var implementation in implementations)
			{
				var isCorrectImplementation = implementation == typeof (WordsStatistics_CorrectImplementation);
				var failed = GetFailedTests(implementation, isCorrectImplementation).ToList();
				Console.Write(implementation.Name + "\t");
				if (failed.Any())
				{
					Console.ForegroundColor = ConsoleColor.Green;
					Console.WriteLine("fails on tests: " + string.Join(", ", failed));
					Console.ForegroundColor = ConsoleColor.Gray;
				}
				else
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("pass all tests :(");
					Console.ForegroundColor = ConsoleColor.Gray;
				}
			}
		}

		private static IEnumerable<Type> GetImplementations()
		{
			return 
				Assembly.GetExecutingAssembly().GetTypes()
				.Where(typeof (IWordsStatistics).IsAssignableFrom)
				.Where(t => !t.IsAbstract && !t.IsInterface)
				.Where(t => t != typeof(WordsStatistics_CorrectImplementation));
		}

		private static bool CheckTests()
		{
			Console.WriteLine("Check all tests pass with correct implementation...");
			var failed = GetFailedTests(typeof(WordsStatistics_CorrectImplementation), true).ToList();
			if (failed.Any())
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Incorrect tests detected: " + string.Join(", ", failed));
				Console.ForegroundColor = ConsoleColor.Gray;
				return false;
			}
			else
			{
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("Tests are OK!");
				Console.WriteLine();
				Console.ForegroundColor = ConsoleColor.Gray;
				return true;
			}
		}

		private static IEnumerable<string> GetFailedTests(Type implementationType, bool printError)
		{
			foreach (var testMethod in GetTestMethods())
			{
				if (!RunTestMethod(implementationType, testMethod, printError))
					yield return testMethod.Name;
			}
		}

		private static bool RunTestMethod(Type implementationType, MethodInfo testMethod, bool printError)
		{
			Func<IWordsStatistics> createImpl = () => (IWordsStatistics)Activator.CreateInstance(implementationType);
			var testObj = new WordsStatistics_Tests { createStat = createImpl };
			testObj.SetUp();
			var timeout = GetTimeout(testMethod);
			try
			{
				Action test = () => testMethod.Invoke(testObj, new object[0]);
				if (timeout > 0)
					RunTestOnOwnThread(timeout, test);
				else test();
			}
			catch (Exception e)
			{
				if (printError)
					Console.WriteLine(e.InnerException);
				return false;
			}
			return true;
		}

		private static int GetTimeout(MethodInfo method)
		{
			return method.GetCustomAttributes<TimeoutAttribute>()
				.Select(attr => (int)attr.Properties["Timeout"])
				.FirstOrDefault();
		}

		private static IEnumerable<MethodInfo> GetTestMethods()
		{
			var testMethods = typeof(WordsStatistics_Tests).GetMethods(BindingFlags.Instance | BindingFlags.Public)
				.Where(m => m.GetCustomAttribute<TestAttribute>() != null);
			return testMethods;
		}

		private static void RunTestOnOwnThread(int timeout, Action action)
		{
			Exception ex = null;
			object locker = new object();
			Thread thread = new Thread(() =>
			{
				try
				{
					action();
				}
				catch (Exception e)
				{
					lock(locker)
						ex = e;
				}
			});
			thread.Start();
			thread.Join(timeout);
			if (!thread.IsAlive)
			{
				lock (locker)
					if (ex != null)
						throw ex;
				return;
			}
			Kill(thread);
			thread.Join();
			throw new TimeoutException();
		}

		public static void Kill(Thread thread)
		{
			try
			{
				thread.Abort();
			}
			catch (ThreadStateException)
			{
#pragma warning disable 618
				thread.Resume();
#pragma warning restore 618
			}

			if ((thread.ThreadState & ThreadState.WaitSleepJoin) != 0)
				thread.Interrupt();
		}
	}
}
