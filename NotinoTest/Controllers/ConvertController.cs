using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NotinoTest.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace NotinoTest.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ConvertController : ControllerBase
  {
    private readonly IConvertService _service;
    
    /// <summary>
    /// Using Dependancy injection 
    /// </summary>
    /// <param name="service">Injected ConvertService</param>
    public ConvertController(IConvertService service)
    {
      _service = service;
    }

    /// <summary>
    /// Convert source XML from server and return JSON
    /// </summary>
    /// <returns>Converted JSON</returns>
    [HttpGet]
    [Route("[action]")]
    public JsonResult GetJson()
    {
      return _service.GetJson();
    }

    /// <summary>
    /// Convert source XML on server and save result into  JSON on the server
    /// </summary>
    [HttpGet]
    [Route("[action]")]
    public void GetConvertOnServer()
    {
      _service.ConvertOnServer();
    }

    /// <summary>
    /// Receive XML from client, convert and return result if the conversion
    /// </summary>
    /// <param name="request">XML file into request</param>
    /// <returns>Result of the conversion in JSON format</returns>
    /// <example>
    /// var content = new StringContent("<root><value>Hello World</value></root>", Encoding.UTF8,"text/xml");
    /// var client = new HttpClient();
    /// var result = client.PostAsync("https://localhost:44338/api/Convert/PostXmlToJson", content).Result;
    /// </example>
    [HttpPost]
    [Route("[action]")]
    public JsonResult PostXmlToJson(System.Net.Http.HttpRequestMessage request)
    {
      var doc = new XmlDocument();
      doc.Load(request.Content.ReadAsStreamAsync().Result);
      return _service.XmlToJson(doc);
    }

    
  }


}
