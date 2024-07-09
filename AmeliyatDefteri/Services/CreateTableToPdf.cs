using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
namespace AmeliyatDefteri.Services
{




public class PdfGenerator{
    public void GeneratePdf()
    {
        QuestPDF.Settings.License = LicenseType.Community;
        var document = new TableDocument();
        document.GeneratePdf("TableDocument.pdf");
    }
}
    
    public class TableDocument: IDocument
{
    public void Compose(IDocumentContainer container)
    {
        
        container.Page(page =>
        {
            page.Margin(50);

            page.Header()
                .Text("Table Example PDF Document")
                .FontSize(20)
                .Bold()
                .AlignCenter();

            page.Content()
                .PaddingVertical(10)
                .Table(table =>
                {
                    // Define columns
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(50);
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                    });

                    // Define header row
                    table.Header(header =>
                    {
                        header.Cell().Element(CellStyle).Text("ID");
                        header.Cell().Element(CellStyle).Text("Name");
                        header.Cell().Element(CellStyle).Text("Description");

                        static IContainer CellStyle(IContainer container) =>
                            container.DefaultTextStyle(x => x.Bold()).PaddingVertical(5).BorderBottom(1);
                    });

                    // Define data rows
                    for (int i = 1; i <= 10; i++)
                    {
                        table.Cell().Element(CellStyle).Text(i.ToString());
                        table.Cell().Element(CellStyle).Text($"Item {i}");
                        table.Cell().Element(CellStyle).Text($"Description for Item {i}");

                        static IContainer CellStyle(IContainer container) =>
                            container.BorderBottom(1).PaddingVertical(5);
                    }
                });

            page.Footer()
                .AlignCenter()
                .Text(x =>
                {
                    x.Span("Page ");
                    x.CurrentPageNumber();
                });
        });
    }
}
}