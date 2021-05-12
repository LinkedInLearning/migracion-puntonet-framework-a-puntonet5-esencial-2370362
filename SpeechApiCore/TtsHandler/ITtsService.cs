using System.IO;

namespace TtsHandler
{
	public interface ITtsService
	{
		MemoryStream CreateWavFile(string text);
	}
}