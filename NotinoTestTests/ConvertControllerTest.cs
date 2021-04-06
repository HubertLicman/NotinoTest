using Microsoft.AspNetCore.Mvc;
using NotinoTest.Controllers;
using NotinoTest.Services;
using System;
using System.Net.Http;
using System.Text;
using Xunit;

namespace NotinoTestTests
{
  public class ConvertControllerTest
  {
    IConvertService _service;
    ConvertController _controller;

    public ConvertControllerTest() {
      _service = new ConvertService();
      _controller = new ConvertController(_service);
  }

    [Fact]
    public void GetJson_WhenCalled_ReturnsOkResult()
    {
      // Act
      var okResult = _controller.GetJson();

      // Assert
      Assert.Equal(200, okResult.StatusCode);
    }

    [Fact]
    public void GetJson_WhenCalled_ReturnsJsonResult()
    {
      // Act
      var jsonResult = _controller.GetJson();

      // Assert
      Assert.IsType<JsonResult>(jsonResult);
    }

    [Fact]
    public void PostXmlToJson_WhenCalled_ReturnsOkResult()
    {
      // Arrange
      var content = new StringContent("<root><value>Hello World</value></root>", Encoding.UTF8, "text/xml");
      var req = new System.Net.Http.HttpRequestMessage();
      req.Content = content;

      // Act
      var jsonResult = _controller.PostXmlToJson(req);

      // Assert
      Assert.Equal(200, jsonResult.StatusCode);
    }

    [Fact]
    public void PostXmlToJson_WhenCalled_ReturnsJsonResult()
    {
      // Arrange
      var content = new StringContent("<root><value>Hello World</value></root>", Encoding.UTF8, "text/xml");
      var req = new System.Net.Http.HttpRequestMessage();
      req.Content = content;

      // Act
      var jsonResult = _controller.PostXmlToJson(req);

      // Assert
      Assert.IsType<JsonResult>(jsonResult);
    }
  }
}
