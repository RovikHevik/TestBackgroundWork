using Microsoft.AspNetCore.Mvc;
using TestBackgroundWork.Worker;

namespace TestBackgroundWork.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkController : ControllerBase
{
    private readonly TestWorker _testWorker;

    public WorkController(TestWorker testWorker)
    {
        _testWorker = testWorker;
    }

    [HttpGet("start")]
    public async Task<IActionResult> Start()
    {
        _testWorker.Start();
        return Ok("Started");
    }
    
    [HttpGet("stop")]
    public async Task<IActionResult> Stop()
    {
        bool result = _testWorker.Stop();
        if (!result)
        {
            return BadRequest("Worker not started");
        }
        return Ok("Stopped");
    }
}