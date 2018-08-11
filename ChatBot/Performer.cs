using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Console;

namespace ChatBot
{
    sealed class Performer : IDisposable
	{

		private Strategy _strategy;
		private StreamReader reader;

		public void DoWork(string[] args)
		{
			ParseInput(args);
			SayWelcome();

			while (true)
			{
				string question = ReadQuestion();
				ShowAnswer(question);
			}
		}

		private void ParseInput(string[] args)
		{
			string str = string.Empty;
			string path = string.Empty;

			for (int i = 0; i < args.Length - 1; i++)
			{
				switch (args[i])
				{
					case "-r":
						str = args[i + 1];
						break;
					case "-f":
						path = args[i + 1];
						break;
				}
			}

			_strategy = ParseStrategy(str) ?? Strategy.Rand;

			if (string.IsNullOrEmpty(path) || !File.Exists(path))
			{
				path = "..\answers.txt";
			}

			OpenFile(path);
		}

		private static void BotSay(string text)
		{
			Write("[бот] ");
			WriteLine(text);
			Write("[Я] ");
		}

		private static Strategy? ParseStrategy(string str)
		{
			Strategy strategy;
			return Enum.TryParse<Strategy>(str, true, out strategy)
				? strategy
				: (Strategy?)null;
		}

		private static string ReadQuestion()
		{
			return ReadLine();
		}

		private void SayWelcome()
		{
			const string welcome = "Привет. Как дела на плюке?";
			BotSay(welcome);
		}

		private void ShowAnswer(string question)
		{
			const string calculate = "calculate:";
			const string strategy = "strategy:";

			var ignore = StringComparison.CurrentCultureIgnoreCase;

			if (!String.IsNullOrWhiteSpace(question))
			{
				if (question.StartsWith(calculate, ignore))
				{
					string expr = question.Substring(calculate.Length);
					ParseExpression(expr);
				}
				else if (question.StartsWith(strategy, ignore))
				{
					string str = question.Substring(strategy.Length);
					Strategy? _str = ParseStrategy(str);
					if (_str.HasValue)
					{
						_strategy = _str.Value;
						BotSay($"Как советовать, так все чатлане. Использую стратегию: {_strategy}");
					}
					else
					{
						BotSay($"Wrong strategy! Leaving strategy: {_strategy}");
					}
				}
				return;
			}

			SaySomething();
		}

		private void ParseExpression(string expr)
		{
			try
			{
				double sum = expr.Split('+').Select(mem => double.Parse(mem)).Sum();
				BotSay($"Я тебя полюбил — я тебя научу: {sum}");
			}
			catch
			{
				SaySomething();
			}
		}

		private void SaySomething()
		{
			switch (_strategy)
			{
				case Strategy.Rand:
					
					break;
				case Strategy.UpSeq:
					string answer = reader.ReadLine();
					BotSay(answer);
					break;
				case Strategy.DownSeq:
					break;
				default:
					throw new NotImplementedException();
			}
		}

		private void OpenFile(string path)
		{
			reader = new StreamReader(path);
		}

		public void Dispose()
		{
			if (reader != null)
			{
				reader.Close();
			}
		}

	}
}
