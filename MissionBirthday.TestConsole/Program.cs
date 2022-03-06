using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MissionBirthday.Contracts.Models;
using MissionBirthday.Logic.Ocr;

namespace MissionBirthday.TestConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await TestOcr();

            Console.WriteLine("Press enter to quit");
            Console.ReadLine();
        }

        private static async Task TestOcr()
        {
            const string imagePath = @"C:\Users\user\Pictures\image.jpg";

            using var imageStream = File.OpenRead(imagePath);

            var service = new OcrService(Options.Create(
                new OcrOptions
                {
                    DocReaderEndpoint = "https://mb-docreader.cognitiveservices.azure.com/",
                    DocReaderKey = "Insert Key Here"    // NOTE: do not check in with actual key
                }));

            var results = await service.ReadAsync(imageStream);

            foreach (var line in results.TextLines)
                Console.WriteLine(line);
        }
    }
}
