using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
    private readonly ILogger<ConvertController> _logger;
    // Definition for internal In and Out files
    private static string sourceFileName = Path.Combine(Path.GetTempPath(), "TestDocument1.xml");
    private static string targetFileName = Path.Combine(Path.GetTempPath(), "TestDocument1.json");

    public ConvertController(ILogger<ConvertController> logger)
    {
      _logger = logger;
      CheckSourceTestFile();
    }

    /// <summary>
    /// Convert source XML from server and return JSON
    /// </summary>
    /// <returns>Converted JSON</returns>
    [HttpGet]
    [Route("[action]")]
    public JsonResult GetJson()
    {
      try
      {
        // Use LINQ to XML for load of source file
        XElement xmlFromFile = XElement.Load(@sourceFileName);
        string jsonString = JsonConvert.SerializeObject(xmlFromFile);
        return new JsonResult(jsonString);
      }
      catch (Exception)
      {
        throw new WebException("Error on GetJson from server", WebExceptionStatus.RequestCanceled);
      }
      
    }

    /// <summary>
    /// Convert source XML on server and save result into  JSON on the server
    /// </summary>
    [HttpGet]
    [Route("[action]")]
    public void GetConvertOnServer()
    {
      try
      {
        // Use LINQ to XML for load of source file
        XElement xmlFromFile = XElement.Load(@sourceFileName);
        string jsonString = JsonConvert.SerializeObject(xmlFromFile);
        System.IO.File.WriteAllText(targetFileName, jsonString);
      }
      catch (Exception)
      {
        throw new WebException("Error on server conversion", WebExceptionStatus.RequestCanceled);
      }
    }

    /// <summary>
    /// Post XML into server, convert and return result if the conversion
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
      string jsonString = JsonConvert.SerializeObject(doc);
      return new JsonResult(jsonString);
    }

    /// <summary>
    /// Check and create source file in defined path
    /// </summary>
    private static void CheckSourceTestFile()
    {
      if (!System.IO.File.Exists(sourceFileName))
      {
        // Create the test file
        using XmlWriter writer = XmlWriter.Create(sourceFileName);
        writer.WriteStartElement("Test");
        writer.WriteElementString("Col1", "Test data 1");
        writer.WriteElementString("Col2", "Test data 2");
        writer.WriteElementString("Col3", "Test data 3");
        writer.WriteEndElement();
        writer.Flush();
      }
    }
  }


}
