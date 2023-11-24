using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment10.Controllers
{
    [Route("api/log")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<LogController> _logger;

        public LogController(IConfiguration configuration, ILogger<LogController> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        [HttpGet("{message}/{logLevel}")]
        public IActionResult Get(string message, string logLevel)
        {
            string levelFromConfig = _configuration.GetValue<string>("Logging:LogLevel:AllowedLevel");

            if (string.IsNullOrEmpty(levelFromConfig))
            {
                return BadRequest($"Invalid log level provided. Allowed log level is {levelFromConfig}");
            }
            else
            {
                switch (logLevel.ToLower())
                {
                    case "debug":
                        _logger.LogDebug($"Debug :: {message}");
                        break;
                    case "warning":
                        _logger.LogWarning($"Warning :: {message}");
                        break;
                    case "error":
                        _logger.LogError($"Error :: {message}");
                        break;
                    case "trace":
                        _logger.LogTrace($"Trace :: {message}");
                        break;
                    case "information":
                        _logger.LogInformation($"Information :: {message}");
                        break;
                    case "critical": 
                        _logger.LogCritical($"Critical :: {message}");
                        break;
                    default:
                        return BadRequest($"Invalid log level: {logLevel}");
                }
            }
            
            return Ok(new { logLevel = logLevel , isPrinted = true});
        }
    }
}
