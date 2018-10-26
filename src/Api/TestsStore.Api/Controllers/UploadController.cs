using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestsStore.Api.Infrastructure.Parsers;
using TestsStore.Api.Infrastructure.Services;

namespace TestsStore.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UploadController : Controller
	{
		private readonly IHostingEnvironment hostingEnvironment;

		public UploadController(IHostingEnvironment hostingEnvironment)
		{
			this.hostingEnvironment = hostingEnvironment;
		}

		// POSt api/upload/trx
		[HttpPost, DisableRequestSizeLimit]
		[Route("trx")]
		public async Task<ActionResult> TrxUpload()
		{
			IFormFile file = Request.Form.Files?.FirstOrDefault();
			if (file == null)
			{
				return BadRequest("Trx file should be included");
			}

			var parser = ParserFactory.Create(ParserType.Trx);
			var parseResult = parser.Parse(file.OpenReadStream());
			var resultServices = new TrxResultService(hostingEnvironment);
//			await resultServices.HandleAsync(file.OpenReadStream());

			return Ok();
		}
	}
}