using System;

namespace PlatformCompatibilityTest
{
	public static class Program
	{
		static void Main()
		{
			var soundHandler = new WindowsSoundHandler();
			if (OperatingSystem.IsWindows())
			{
				soundHandler.PlaySound(@"c:\sound.wav");
			}
			Console.WriteLine("Hello World!");
		}
	}
}
