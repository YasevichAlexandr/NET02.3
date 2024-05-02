using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace MyApp.Listeners
{
    public class WordListener : IListener
    {
        private string filePath;

        public void Configure(IConfiguration configuration)
        {
            filePath = configuration["WordListener:FilePath"];
        }

        public void WriteMessage(string message)
        {
            using (var wordDocument = WordprocessingDocument.Open(filePath, true))
            {
                var body = wordDocument.MainDocumentPart.Document.Body;
                var paragraph = new Paragraph(new Run(new DocumentFormat.OpenXml.Wordprocessing.Text(message)));
                body.Append(paragraph);
                wordDocument.Save();
            }
        }
    }
}