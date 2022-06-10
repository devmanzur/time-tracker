using System.Text;
using SelectPdf;

namespace TimeTracker.Core.Shared.Utils;

public static class HtmlToPdfConverter
{
    public static void ConvertHtmlToPdf(string directory, string sourceHtmlTemplateFileName, string outputPdfFileName,
        Func<string, string> templateFiller)
    {
        var htmlSource = File.ReadAllText($"{directory}/{sourceHtmlTemplateFileName}.html");
        var fileContent = templateFiller(htmlSource);

        var pdfConverter = new HtmlToPdf()
        {
            Options =
            {
                EmbedFonts = true,
                PdfPageSize = PdfPageSize.A4,
                ExternalLinksEnabled = true,
                PageBreaksEnhancedAlgorithm = true,
                RenderingEngine = RenderingEngine.WebKit,
                MarginTop = 16,
                MarginBottom = 16,
                MarginLeft = 16,
                MarginRight = 16,
            }
        };
        PdfDocument doc = pdfConverter.ConvertHtmlString(fileContent);
        doc.Save($"{directory}/{outputPdfFileName}.pdf");
        doc.Close();
    }

    public static string PCITemplateFiller(string htmlSource)
    {
        try
        {
            var stringBuilder = new StringBuilder(htmlSource);
            stringBuilder
                .Replace("@HeaderLine1", "LODGMENT ADVICE")
                .Replace("@HeaderLine2", "Electronic Claim for assessment by Services Australia")
                .Replace("@HeaderLine3",
                    "THIS FORM CANNOT BE USED TO MAKE A CLAIM FOR MEDICARE PAYMENTS. THIS CLAIM HAS ALREADY BEEN SUBMITTED TO MEDICARE ON YOUR BEHALF")
                .Replace("@ClaimRef@", "ION0000020042022110812")
                .Replace("@DoL@", "20/04/2022 11:08 AM")
                .Replace("@ACRF@", "220420-7")
                .Replace("@Location@", "Dhaka, Bangladesh")
                .Replace("@LocationId@", "ION00000")
                .Replace("@LocationContact@", "0300000000")
                .Replace("@PatientName@", "Marrianna-Louise Jones")
                .Replace("@ClaimantName@", "Marrianna-Louise Jones")
                .Replace("@PatientDob@", "18/05/1967")
                .Replace("@ClaimantDob@", "18/05/1967")
                .Replace("@PatientMedicareNo@", "2298039874")
                .Replace("@ClaimantMedicareNo@", "2298039874")
                .Replace("@PatientMedicareIrn@", "1")
                .Replace("@ClaimantMedicareIrn@", "1")
                .Replace("@ClaimantAddress@", "6 Jones Pl ,GOWRIE 2904")
                .Replace("@ServicingProvider@", "Emerson Brantley")
                .Replace("@PayeeProviderNo@", "2442421K")
                .Replace("@PayeeProvider@", "Kathy Humphris")
                .Replace("@ServicingProviderNo@", "2442421K")
                .Replace("@ReferrerProvider@", "Dr Vivian Mortier")
                .Replace("@ReferrerProviderNo@", "2442421K")
                .Replace("@ReferrerPeriod@", "Standard(12 months for a GP, 3 months for a Specialist)")
                .Replace("@ReferralDate@", "21/06/2021")
                .Replace("<div>@ServiceItems@</div>", ServiceItemsBuilder())
                .Replace("@LocationAddress@", "123 demonstartion ave ,MOUNT RICHMOND VIC 3305");

            return stringBuilder.ToString();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private static string? ServiceItemsBuilder()
    {
        var sb = new StringBuilder();
        for (int i = 0; i < 4; i++)
        {
            sb.Append(ServiceItemsFiller());
        }

        return sb.ToString();
    }

    private static StringBuilder ServiceItemsFiller()
    {
        var template = new StringBuilder(
            "<tr> <td style=\"padding: 8px\">@ServiceDate@</td><td style=\"padding: 8px\">@ItemNo@</td>" +
            "<td style=\"padding: 8px\">@ItemName@</td><td style=\"padding: 8px\">@ChargeAmt@</td>" +
            "<td style=\"padding:8px\">@PatientCont@</td><td style = \"padding: 8px\" > @Benefit@ </td></tr > ");

        return template
            .Replace("@ServiceDate@", "20/04/2022")
            .Replace("@ItemNo@", "4")
            .Replace("@ItemName@", "Group attendance item")
            .Replace("@ChargeAmt@", "200")
            .Replace("@PatientCont@", "1")
            .Replace("@Benefit@", "316");
    }
}