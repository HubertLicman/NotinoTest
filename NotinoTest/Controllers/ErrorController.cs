using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotinoTest.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ErrorController : ControllerBase
  {
    /// <summary>
    /// Basic Error handler for umanaged errors.
    /// Is used in production envirnment
    /// </summary>
    /// <returns>Detailed error desc.</returns>
    [HttpGet]
    [Route("/error")]
    public IActionResult Error()
    {
      return Problem();
    }
  }
}
