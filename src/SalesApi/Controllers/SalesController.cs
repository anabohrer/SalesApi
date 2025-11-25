using Microsoft.AspNetCore.Mvc;
using SalesApi.Application;

namespace SalesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class SalesController(ISalesSummaryUseCase useCase) : ControllerBase
    {
        private readonly ISalesSummaryUseCase useCase = useCase;

        [HttpPost("summary")]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadAndSummarize(IFormFile file, CancellationToken cancellationToken)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Please upload a CSV file in form field 'file'.");

            await using var stream = file.OpenReadStream();
            var result = await useCase.ComputeSummaryAsync(stream, cancellationToken);
            return Ok(result);
        }
    }
}
