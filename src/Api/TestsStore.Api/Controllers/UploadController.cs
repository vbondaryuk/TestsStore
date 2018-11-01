using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestsStore.Api.Infrastructure.Commands;
using TestsStore.Api.Infrastructure.Parsers.Trx;

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

		// POSt api/upload
		[HttpPost]
		[DisableRequestSizeLimit]
		public async Task<ActionResult> Upload([FromForm] string projectName)
		{
			var file = Request.Form.Files?.FirstOrDefault();
			if (file == null)
				return BadRequest("File should be included");

			var fileStream = file.OpenReadStream();
			var fileType = Path.GetExtension(file.FileName).Substring(1);
			var uploadTestResultCommand = new UploadTestResultCommand(projectName, fileType, fileStream);
			var commandResult = await _uploadTestResultCommandHandler.ExecuteAsync(uploadTestResultCommand);

			if (!commandResult.Success)
				return BadRequest();

			return Ok();
		}
	}
}