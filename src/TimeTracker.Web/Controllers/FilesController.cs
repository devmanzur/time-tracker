﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeTracker.Core.Shared.Utils;

namespace TimeTracker.Web.Controllers;

public class FilesController : BaseApiController
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public FilesController(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }
    
    [AllowAnonymous]
    [HttpPost("send-email-attachment")]
    public async Task<IActionResult> GeneratePdfAndSendEmailWithAttachment()
    {
        HtmlToPdfConverter.ConvertHtmlToPdf(_webHostEnvironment.WebRootPath,"demo-template","myoutputfile");
        return Ok();
    }
}