using System.Linq;
using System.Threading.Tasks;
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
		
		public UploadController()
		{
		}

		// POSt api/upload/trx
		[HttpPost, DisableRequestSizeLimit]
		[Route("trx")]
		public async Task<ActionResult> TrxUpload([FromBody]string projectName)
		{
			IFormFile file = Request.Form.Files?.FirstOrDefault();
			if (file == null)
			{
				return BadRequest("Trx file should be included");
			}

			var uploadTestResultService = new UploadTestResultService();
			await uploadTestResultService.UplodAsync(projectName, file.OpenReadStream(), ParserType.Trx);

			return Ok();
		}
	}
}