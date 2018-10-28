using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestsStore.Api.Infrastructure.Commands;

namespace TestsStore.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UploadController : Controller
	{
		private readonly UploadTestResultCommandHandler _uploadTestResultCommandHandler;

		public UploadController(UploadTestResultCommandHandler uploadTestResultCommandHandler)
		{
			_uploadTestResultCommandHandler = uploadTestResultCommandHandler;
		}

		// POSt api/upload/trx
		[HttpPost]
		[DisableRequestSizeLimit]
		[Route("trx")]
		public async Task<ActionResult> TrxUpload([FromBody] string projectName, [FromBody] string type)
		{
			var file = Request.Form.Files?.FirstOrDefault();
			if (file == null) return BadRequest("Trx file should be included");

			var uploadTestResultCommand = new UploadTestResultCommand
			{
				ProjectName = projectName,
				FileType = type,
				Stream = file.OpenReadStream()
			};
			var commandResult = await _uploadTestResultCommandHandler.ExecuteAsync(uploadTestResultCommand);

			if (!commandResult.Success)
				return BadRequest();

			return Ok();
		}
	}
}