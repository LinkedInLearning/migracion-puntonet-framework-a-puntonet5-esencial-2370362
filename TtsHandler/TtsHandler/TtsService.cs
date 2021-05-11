using System;
using System.IO;
using System.Speech.AudioFormat;
using System.Speech.Synthesis;

namespace TtsHandler
{
	public class TtsService : ITtsService
	{

		public MemoryStream CreateWavFile(string text)
		{
			var mmStream = new MemoryStream();
			var ss = new SpeechSynthesizer();
			ss.Volume = 100;
			ss.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Adult);
			ss.SetOutputToWaveStream(mmStream);
			ss.Speak(text);
			return mmStream;
		}
	}
}