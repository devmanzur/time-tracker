using System.Text;
using SelectPdf;

namespace TimeTracker.Core.Shared.Utils;

public static class HtmlToPdfConversionHelper
{

    public static void ConvertHtmlToPdf(string directory, string outputPdfFileName,string fileContent)
    {
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
        var doc = pdfConverter.ConvertHtmlString(fileContent);
        doc.Save($"{directory}/{outputPdfFileName}.pdf");
        doc.Close();
    }

    public static string PCITemplateFiller(string htmlSource, PCIReportTemplateData data)
    {
        try
        {
            var stringBuilder = new StringBuilder(htmlSource);
            stringBuilder
                .Replace("@HeaderLine1", data.Title1)
                .Replace("@HeaderLine2", data.Title2)
                .Replace("@HeaderLine3", data.Title3)
                .Replace("@ClaimRef@", data.ClaimReference)
                .Replace("@DoL@", data.DateOfLodgement)
                .Replace("@ACRF@", data.ACRF)
                .Replace("@Location@", data.Location)
                .Replace("@LocationId@", data.LocationId)
                .Replace("@LocationContact@", data.LocationContract)
                .Replace("@PatientName@", data.PatientName)
                .Replace("@ClaimantName@", data.ClaimantName)
                .Replace("@PatientDob@", data.PatientDob)
                .Replace("@ClaimantDob@", data.ClaimantDob)
                .Replace("@PatientMedicareNo@", data.PatientMedicareNumber)
                .Replace("@ClaimantMedicareNo@", data.ClaimantMedicareNumber)
                .Replace("@PatientMedicareIrn@", data.PatientMedicareIRN)
                .Replace("@ClaimantMedicareIrn@", data.ClaimantMedicareIRN)
                .Replace("@ClaimantAddress@", data.ClaimantAddress)
                .Replace("@ServicingProvider@", data.ServiceProvider)
                .Replace("@ServicingProviderNo@", data.ServiceProviderNumber)
                .Replace("@PayeeProvider@", data.PayeeProvider)
                .Replace("@PayeeProviderNo@", data.PayeeProviderNumber)
                .Replace("@ReferrerProvider@", data.ReferralProvider)
                .Replace("@ReferrerProviderNo@", data.ReferralProviderNumber)
                .Replace("@ReferrerPeriod@", data.ReferrerPeriod)
                .Replace("@ReferralDate@", data.ReferralDate)
                .Replace("<div>@ServiceItems@</div>", ServiceItemsBuilder(data.ServiceItems))
                .Replace("@LocationAddress@", data.LocationAddress);

            return stringBuilder.ToString();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private static string? ServiceItemsBuilder(List<ServiceItemTemplateData> serviceItems)
    {
        var sb = new StringBuilder();
        
        foreach (var item in serviceItems)
        {
            sb.Append(ServiceItemsFiller(item));
        }

        return sb.ToString();
    }

    private static StringBuilder ServiceItemsFiller(ServiceItemTemplateData item)
    {
        var template = new StringBuilder(
            "<tr> <td style=\"padding: 8px\">@ServiceDate@</td><td style=\"padding: 8px\">@ItemNo@</td>" +
            "<td style=\"padding: 8px\">@ItemName@</td><td style=\"padding: 8px\">@ChargeAmt@</td>" +
            "<td style=\"padding:8px\">@PatientCont@</td><td style = \"padding: 8px\" > @Benefit@ </td> </tr>");

        return template
            .Replace("@ServiceDate@", item.DateOfService)
            .Replace("@ItemNo@", item.itemNumber)
            .Replace("@ItemName@", item.itemName)
            .Replace("@ChargeAmt@", $"{item.ChargeAmount}")
            .Replace("@PatientCont@", $"{item.PatientCount}")
            .Replace("@Benefit@", $"{item.Benefit}");
    }
}

public class PCIReportTemplateData
{
    public string Title1 { get; set; }
    public string Title2 { get; set; }
    public string Title3 { get; set; }
    //claim details
    public string ClaimReference { get; set; }
    public string DateOfLodgement { get; set; }
    public string ACRF { get; set; }
    public string Location { get; set; }
    public string LocationId { get; set; }
    public string LocationContract { get; set; }
    public string LocationAddress { get; set; }
    
    //patient details
    public string PatientName { get; set; }
    public string PatientDob { get; set; }
    public string PatientMedicareNumber { get; set; }
    public string PatientMedicareIRN { get; set; }
    
    //claimant details
    public string ClaimantName { get; set; }
    public string ClaimantDob { get; set; }
    public string ClaimantMedicareNumber { get; set; }
    public string ClaimantMedicareIRN { get; set; }
    public string ClaimantAddress { get; set; }
    
    //service details
    public string ServiceProvider { get; set; }
    public string ServiceProviderNumber { get; set; }
    public string PayeeProvider { get; set; }
    public string PayeeProviderNumber { get; set; }
    public string ReferralProvider { get; set; }
    public string ReferralProviderNumber { get; set; }
    public string ReferrerPeriod { get; set; }
    public string ReferralDate { get; set; }

    public List<ServiceItemTemplateData> ServiceItems { get; set; }
}

public class ServiceItemTemplateData
{
    public string DateOfService { get; set; }
    public string itemNumber { get; set; }
    public string itemName { get; set; }
    public double ChargeAmount { get; set; }
    public double Benefit { get; set; }
    public int PatientCount { get; set; }
}