using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLibraryNetFX
{
	public class OldService : IService
	{
		public Task<int[]> GetNPrimeNumbersAsync(int numbers)
		{
			return Task.FromResult(new int[0]);
		}
	}
}
