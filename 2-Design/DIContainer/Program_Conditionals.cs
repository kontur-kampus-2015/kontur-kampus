using System;
using System.Collections.Generic;

namespace DIContainer
{
	public class Program_Conditionals
	{
		#region conditionals

		private void Main(string[] args)
		{
			if (args[0] == "one")
				DoFirstThing();
			if (args[0] == "two")
				DoSecondThing();
			DoFinalThing(args[0]);
		}

		private static void DoFinalThing(string commandType)
		{
			if (commandType == "one") Console.WriteLine("Делаем что-то хитрое");
			else Console.WriteLine("Делаем что-то другое хитрое");
		}

		private static void DoFirstThing()
		{
			// Делаем что-то хитрое
		}

		private static void DoSecondThing()
		{
			// Делаем что-то другое хитрое
		}
		#endregion

		#region polymorphism

		private static readonly Dictionary<string, IConsoleCommand> Commands =
			new Dictionary<string, IConsoleCommand>
			{
				{"one", new One()},
				{"two", new Two()}
			};

		private static void Main_Refactored(string[] args)
		{
			var cmd = Commands[args[0]];
			cmd.Do();
			cmd.Finish();
		}

		#endregion
	}

	#region polymorphism

	public interface IConsoleCommand
	{
		void Do();
		void Finish();
	}

	public class One : IConsoleCommand
	{
		public void Do()
		{
			// Делаем что-то хитрое
		}

		public void Finish()
		{
			Console.WriteLine("Делаем что-то хитрое");
		}
	}


	public class Two : IConsoleCommand
	{
		public void Do()
		{
			// Делаем что-то другое хитрое
		}

		public void Finish()
		{
			Console.WriteLine("Делаем что-то другое хитрое");
		}
	}

	#endregion
}