using EthereumAPI.BackgroundService;
using EthereumAPI.Contracts;
using EthereumAPI.Logger;
using EthereumAPI.Models;
using Newtonsoft.Json;
using System.Collections.Concurrent;

namespace EthereumAPI.Scheduler
{
    public class GetNewBlocks : ScheduledProcessor
    {
        //public Queue
        public static string _filterKey;
        private readonly ILoggerManager _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private ConcurrentQueue<JsonRpc<EthBlockDetail>> BlockQueue = new ConcurrentQueue<JsonRpc<EthBlockDetail>>();

        public GetNewBlocks(IServiceScopeFactory serviceScopeFactory, ILoggerManager logger) : base(serviceScopeFactory)
        {
            _filterKey = string.Empty;
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;

            //Task = new Task();
            Task.Factory.StartNew(() => WatichQueue());
        }

        protected override string Schedule => "*/5 * * * * *";

        public override async Task ProcessInScope(IServiceProvider serviceProvider)
        {
            try
            {
                using (IServiceScope scope = serviceProvider.CreateScope())
                {
                    IEthereumClient service = serviceProvider.GetRequiredService<IEthereumClient>();

                    if (string.IsNullOrEmpty(_filterKey))
                    {
                        var createResponse = await service.CreateNewBlockFilter();
                        _filterKey = createResponse.Result;
                    }
                    
                    var blocksResponse = await service.GetNewBlocks(_filterKey);

                    foreach (var hash in blocksResponse.Result)
                    {
                        var block = await service.GetBlcokDetailByHash(hash);
                        if (block != null && string.IsNullOrEmpty(block.Error?.Message))
                            BlockQueue.Enqueue(block);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"[!!!Error!!!]  {ex.Message}");
            }
        }

        public void WatichQueue()
        {
            try
            {
                while (true)
                {
                    while (BlockQueue.TryDequeue(out JsonRpc<EthBlockDetail> block))
                    {
                        _logger.LogInfo("==============================================================");
                        _logger.LogInfo("=========================Queue start===========================");
                        _logger.LogInfo("==============================================================");

                        var contractData = block.Result.Transactions.
                                               Where(e => e.TransactionState == Models.TransactionState.TokenTransfer);

                        var noneContractData = block.Result.Transactions.
                                                Where(e => e.TransactionState != Models.TransactionState.TokenTransfer);

                        Task.Factory.StartNew(async () =>
                        {
                            if (contractData != null && contractData.Any())
                            {
                                foreach (var item in contractData)
                                {
                                    using (IServiceScope scope = _serviceScopeFactory.CreateScope())
                                    {
                                        IEthereumClient service = scope.ServiceProvider.GetRequiredService<IEthereumClient>();

                                        var transaction = await service.GetTransactionReciptByHash(item.TransactionHash);
                                        if (transaction != null && transaction.Result != null && transaction.Result.Logs.Any())
                                        {
                                            foreach (Models.Log log in transaction.Result?.Logs)
                                            {
                                                DateTime now = DateTime.UtcNow;
                                                DateTime timeStamp = HexToDateTime(block.Result.Timestamp);
                                                _logger.LogInfo($"\t[{item.TransactionState.ToString()}] \t || NowUTC : {now} || TimeStamp : {timeStamp} || TimeDiff : {now.Subtract(timeStamp)} \r\n \t\t\t\t\t\t From : {item.From} || To : {log.Address}\r\n \t\t\t\t\t\t TransactionHash : {item.TransactionHash}");
                                            }
                                        }
                                    }
                                }
                            }
                        });

                        Task.Factory.StartNew(() =>
                        {
                            foreach (var transaction in noneContractData)
                            {
                                DateTime now = DateTime.UtcNow;
                                DateTime timeStamp = HexToDateTime(block.Result.Timestamp);
                                _logger.LogInfo($"\t[{transaction.TransactionState.ToString()}] \t || NowUTC : {now} || TimeStamp : {timeStamp} || TimeDiff : {now.Subtract(timeStamp)}  \r\n \t\t\t\t\t\t From : {transaction.From} || To : {transaction.To}\r\n \t\t\t\t\t\t TransactionHash : {transaction.TransactionHash}");
                            }
                        });
                    }
                    Thread.Sleep(500);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error! : {ex.Message}");
            }
        }

        private DateTime HexToDateTime(string hexDate)
        {
            try
            {
                long convertedLong = Convert.ToInt64(hexDate, 16);
                return DateTime.UnixEpoch.AddSeconds(convertedLong);
            }
            catch (Exception)
            {
                return DateTime.UtcNow;
            }
        }
    }



}
