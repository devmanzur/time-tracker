using Microsoft.AspNetCore.Authorization;
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
    public async Task<IActionResult> GeneratePdfAndSendEmailWithAttachment([FromBody] PCIReportTemplateData data)
    {
        var htmlSource = await System.IO.File.ReadAllTextAsync($"{_webHostEnvironment.WebRootPath}/pdf-template.html");

        HtmlToPdfConversionHelper.ConvertHtmlToPdf(_webHostEnvironment.WebRootPath, "myoutputfile",
            HtmlToPdfConversionHelper.PCITemplateFiller(htmlSource, data));
        return Ok();
    }
}