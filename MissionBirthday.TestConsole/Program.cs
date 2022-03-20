using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MissionBirthday.Contracts.Models;
using MissionBirthday.Logic.AzureAi;

namespace MissionBirthday.TestConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder//.AddFilter("MissionBirthday.TestConsole.Program", LogLevel.Debug)
                    .AddConsole();                
            });

            //await TestOcr();
            await TestLanguage(loggerFactory.CreateLogger<EntityExtractionService>());
        }

        private static async Task TestOcr()
        {
            const string imagePath = @"C:\Users\user\Pictures\image.jpg";

            using var imageStream = File.OpenRead(imagePath);

            var service = new OcrService(null, Options.Create(
                new OcrOptions
                {
                    DocReaderEndpoint = "https://mb-docreader.cognitiveservices.azure.com/",
                    DocReaderKey = "Insert Key Here"    // NOTE: do not check in with actual key
                }));

            var results = await service.ReadAsync(imageStream);

            foreach (var line in results.TextLines)
                Console.WriteLine(line);
        }

        private static async Task TestLanguage(ILogger<EntityExtractionService> logger)
        {
            const string document = @"St. Vincent de Paul
Food Donations
1103 NE Elm St
Prineville, OR 97754
Contact us at: 775-555-2354 or visit us online at www.sierrabiblechurch.org
Here's what to bring:
- peanut butter
- jelly
- bread
- chili
- soup
- bottled water";

            var service = new EntityExtractionService(logger, Options.Create(
                new LanguageServiceOptions
                {
                    MbLanguageEndpoint = "https://mb-language.cognitiveservices.azure.com/",
                    MbLanguageKey = "Insert Key Here"    // NOTE: do not check in with actual key
                }));

            var result = await service.AnalyzeAsync(document);

            foreach (var entity in result)
            {
                Console.WriteLine($"{entity.Category} ({entity.SubCategory}), score={entity.ConfidenceScore}, text={entity.Text}");
            }
        }
    }
}
