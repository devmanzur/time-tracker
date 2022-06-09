using iText.Html2pdf;
using iText.IO.Font;
using iText.Layout.Font;
using iText.StyledXmlParser.Resolver.Font;

namespace TimeTracker.Core.Shared.Utils;

public static class HtmlToPdfConverter
{
    private const string MuseoSans = "fonts\\MuseoSans_300.ttf";
    private const string MuseoSansBold = "fonts\\MuseoSans_700.ttf";

    public static void ConvertHtmlToPdf(string directory, string sourceHtmlFileName, string outputPdfFileName)
    {
        using var htmlSource = File.Open($"{directory}/{sourceHtmlFileName}.html", FileMode.Open);
        using var pdfDest = File.Open($"{directory}/{outputPdfFileName}.pdf", FileMode.Create);
        var converterProperties = new ConverterProperties();
        converterProperties.SetFontProvider(GetFontProvider(directory));
        HtmlConverter.ConvertToPdf(htmlSource, pdfDest, converterProperties);
    }

    private static FontProvider GetFontProvider(string directory)
    {
        var museoSans = $"{directory}\\{MuseoSans}";
        var museoSansBold = $"{directory}\\{MuseoSansBold}";
        
        FontProgramFactory.RegisterFont(museoSans,"Museo Sans");
        var fontProgram = FontProgramFactory.CreateRegisteredFont("Museo Sans");

        FontProgramFactory.RegisterFont(museoSansBold,"Museo Sans Bold");
        var fontProgram2 = FontProgramFactory.CreateRegisteredFont("Museo Sans Bold");

        var fontProvider = new BasicFontProvider();
        fontProvider.AddFont(fontProgram);
        fontProvider.AddFont(fontProgram2);
        return fontProvider;
    }
}