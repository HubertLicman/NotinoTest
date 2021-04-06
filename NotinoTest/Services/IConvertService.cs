using Microsoft.AspNetCore.Mvc;
using System.Xml;

namespace NotinoTest.Services
{
  public interface IConvertService
  {
    JsonResult GetJson();
    void ConvertOnServer();
    JsonResult XmlToJson(XmlDocument doc);
  }
}