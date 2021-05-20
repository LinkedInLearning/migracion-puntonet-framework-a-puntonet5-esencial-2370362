using System;
using System.Threading.Tasks;

namespace MatchingGame.Services
{
	public interface ISoundService : IDisposable
	{
		void PlaySound(SoundService.Sound sound);
	}
}