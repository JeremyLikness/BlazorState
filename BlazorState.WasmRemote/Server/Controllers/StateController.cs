using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BlazorState.ViewModel;

namespace BlazorState.WasmRemote.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StateController : ControllerBase
    {
        private static readonly Dictionary<string, HealthModel> _cache =
            new Dictionary<string, HealthModel>();

        private readonly ILogger<StateController> logger;

        public StateController(ILogger<StateController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public HealthModel Get()
        {
            var ip = HttpContext.Connection.RemoteIpAddress.ToString();
            logger.LogInformation("Request from ip {ip}", ip);
            return _cache.ContainsKey(ip) ? _cache[ip] : new HealthModel();
        }

        [HttpPost]
        public void Post([FromBody]HealthModel model)
        {
            var ip = HttpContext.Connection.RemoteIpAddress.ToString();
            logger.LogInformation("Post from ip {ip}", ip);
            _cache[ip] = model;
        }
    }
}
