using EthereumAPI.Contracts;

namespace EthereumAPI.BackgroundService
{
    public abstract class BackgroundService : IHostedService
    {
        private Task _executingTask;
        private readonly CancellationTokenSource _stoppingCts = new CancellationTokenSource();

        public virtual Task StartAsync(CancellationToken cancellationToken)
        {
            //excutiong 한것을 저장하기
            _executingTask = ExecuteAsync(_stoppingCts.Token);

            if (_executingTask.IsCanceled)
                return _executingTask;

            return Task.CompletedTask;
        }

        public virtual async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_executingTask == null)
            {
                return;
            }

            try
            {
                _stoppingCts.Cancel();
            }
            finally
            {
                await Task.WhenAny(_executingTask, Task.Delay(Timeout.Infinite,  cancellationToken));
            }
        }

        protected virtual async Task ExecuteAsync(CancellationToken stoppingToken)
        {            
            do
            {
                await Process();

                await Task.Delay(5000, stoppingToken); //5 seconds delay
            }
            while (!stoppingToken.IsCancellationRequested);
        }

        protected abstract Task Process();

    }
}
