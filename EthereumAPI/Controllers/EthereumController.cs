using EthereumAPI.Contracts;
using EthereumAPI.Models.Filters;
using Microsoft.AspNetCore.Mvc;

namespace EthereumAPI.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("blocks")]
    public class EthereumController : ControllerBase
    {
        private readonly IEthereumClient _ethereumClient;
        public EthereumController(IEthereumClient ethereumClient) => _ethereumClient = ethereumClient;

        [HttpPost("new-blocks", Name = "CreateNewFilter")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateNewFilter()
        {
            var response = await _ethereumClient.CreateNewBlockFilter();
            return Ok(response);
        }

        [HttpGet("new-blocks", Name = "GetNewBlocks")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetNewBlocks([FromQuery] NewBlocksParameter parameter)
        {
            var response = await _ethereumClient.GetNewBlocks(parameter.FilterId);
            return Ok(response);
        }

        [HttpGet("{hash}", Name = "GetBlockByHash")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetBlockByHash(string hash)
        {
            var response = await _ethereumClient.GetBlcokByHash(hash);
            return Ok(response);
        }

        [HttpGet("transactions/{hash}", Name = "GetTransactionByHash")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTransactionByHash(string hash)
        {
            var response = await _ethereumClient.GetTransactionReciptByHash(hash);
            return Ok(response);
        }
    }
}
