using System;
using static System.Console;

namespace ChatBot
{
	class Program
	{

		static void Main(string[] args)
        {
			var perf = new Performer();
			try
			{
				perf.DoWork(args);
			}
			catch (Exception ex)
			{
				WriteLine("Something wrong:");
				WriteLine(ex.Message);
			}
			finally
			{
				perf.Dispose();
			}
		}

    }
}
