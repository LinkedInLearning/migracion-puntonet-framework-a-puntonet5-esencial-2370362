using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MatchingGame.Services
{
	public class SoundService : ISoundService
	{

		public enum Sound
		{
			FlipCard,
			TimerFlip,
			Match,
			NotMatch,
			Moving,
			Edge
		}

		public async Task PlaySound(Sound sound)
		{
			string fileName;
			switch (sound)
			{
				case Sound.FlipCard: fileName = "FlipCard"; break;
				case Sound.TimerFlip: fileName = "TimerFlip"; break;
				case Sound.Match: fileName = "Match"; break;
				case Sound.NotMatch: fileName = "NotMatch"; break;
				case Sound.Moving: fileName = "Moving"; break;
				case Sound.Edge: fileName = "Edge"; break;
				default: throw new ArgumentOutOfRangeException(nameof(sound));
			}

			using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"MatchingGame.Resources.Sounds.{fileName}.wav"))
			{
				using (SoundPlayer player = new SoundPlayer())
				{
					player.Stream = stream;
					await Task.Run(() => player.PlaySync());
				}
			}
		}
	}
}