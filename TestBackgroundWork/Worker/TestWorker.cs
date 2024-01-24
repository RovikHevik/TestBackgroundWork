namespace TestBackgroundWork.Worker;

public class TestWorker
{
    private CancellationTokenSource? _cts;
    private ILogger<TestWorker> _logger;
    private int _counter = 0;
    
    public TestWorker(ILogger<TestWorker> logger)
    {
        _logger = logger;
    }
    
    public void Start()
    {
        _logger.LogInformation("Starting TestWorker");
        _cts = new CancellationTokenSource();
        Task.Run(DoWork).ConfigureAwait(false);
    }
    
    public bool Stop()
    {
        _logger.LogInformation("Stopping TestWorker");
        if (_cts == null || _cts.IsCancellationRequested)
        {
            return false;
        }
        
        _cts.Cancel();
        return true;

    }
    
    private async Task DoWork()
    {
        while (!_cts.IsCancellationRequested)
        {
            _logger.LogInformation($"TestWorker is running: {_counter++}");
            await Task.Delay(1000, _cts.Token);
        }
    }
}