using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SampleProject.Core.Dto;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BasicAuthenticationController : ControllerBase
    {
        
        private readonly ILogger<BasicAuthenticationController> _logger;

        public BasicAuthenticationController(ILogger<BasicAuthenticationController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("validate")]
        [Authorize]
        public BasicValidateResponseDto PostValidate()
        {
            _logger.LogInformation("Basic auth validado");
            return new BasicValidateResponseDto
            {
                Msg = "Basic auth validado!"
            };
        }
    }
}
