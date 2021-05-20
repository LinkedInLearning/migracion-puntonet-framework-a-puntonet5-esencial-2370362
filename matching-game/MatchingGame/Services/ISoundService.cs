using System.Threading.Tasks;

namespace MatchingGame.Services
{
	public interface ISoundService
	{
		Task PlaySound(SoundService.Sound sound);
	}
}