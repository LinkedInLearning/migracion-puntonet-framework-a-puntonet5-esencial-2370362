using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace PlatformCompatibilityTest
{
	public class WindowsSoundHandler
	{

		[SupportedOSPlatform("windows")]
		public void PlaySound(string soundPath)
		{
			Console.WriteLine($"Reproduciendo el sonido {soundPath}");
		}
	}
}
