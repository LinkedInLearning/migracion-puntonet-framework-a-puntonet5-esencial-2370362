using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using TtsHandler;

namespace MigrateToNet5.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SpeakController : ControllerBase
	{

		private readonly ITtsService _ttsService;

		public SpeakController(ITtsService ttsService)
		{
			_ttsService = ttsService;
		}

		// GET api/values
		[HttpGet]
		public IActionResult Get(string text)
		{
			var mmStr = _ttsService.CreateWavFile(text);
			mmStr.Seek(0, SeekOrigin.Begin);
			return File(mmStr, "audio/x-wav", true);
		}
	}
}
