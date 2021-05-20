using System.Threading.Tasks;

namespace MatchingGame.Services
{
	public interface ISoundService
	{
		void PlaySound(SoundService.Sound sound);
	}
}