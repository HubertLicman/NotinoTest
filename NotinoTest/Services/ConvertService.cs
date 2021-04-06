using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace NotinoTest.Services
{
  public class ConvertService: IConvertService
  {
    // Definition for internal In and Out files
    private static string sourceFileName = Path.Combine(Path.GetTempPath(), "TestDocument1.xml");
    private static string targetFileName = Path.Combine(Path.GetTempPath(), "TestDocument1.json");

    public ConvertService()
    {
      CheckSourceTestFile();
    }

    /// <summary>
    /// Convert source XML from server and return JSON
    /// </summary>
    /// <returns>Converted JSON</returns>
    public JsonResult GetJson()
    {
      // Use LINQ to XML for load of source file
      XElement xmlFromFile = XElement.Load(@sourceFileName);
      string jsonString = JsonConvert.SerializeObject(xmlFromFile);
      var jsonResult = new JsonResult(jsonString);
      jsonResult.StatusCode = (int?)HttpStatusCode.OK;
      return jsonResult;
    }

    /// <summary>
    /// Convert source XML on server and save result into  JSON on the server
    /// </summary>
    public void ConvertOnServer()
    {
      // Use LINQ to XML for load of source file
      XElement xmlFromFile = XElement.Load(@sourceFileName);
      string jsonString = JsonConvert.SerializeObject(xmlFromFile);
      System.IO.File.WriteAllText(targetFileName, jsonString);
    }

    /// <summary>
    /// Convert XML docuemnt into JSON format
    /// </summary>
    /// <param name="doc">XMLdocument</param>
    /// <returns>JSON</returns>
    public JsonResult XmlToJson(XmlDocument doc)
    {
      string jsonString = JsonConvert.SerializeObject(doc);
      var jsonResult = new JsonResult(jsonString);
      jsonResult.StatusCode = (int?)HttpStatusCode.OK;
      return jsonResult;
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
