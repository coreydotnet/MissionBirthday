using System;
using System.Threading.Tasks;
using MissionBirthday.Infrastructure.Ocr;

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
            const string fileUrl = @"";

            var service = new OcrService();

            var results = await service.ReadAsync(endpoint: "", subscriptionKey: "", fileUrl);

            foreach (var line in results.TextLines)
                Console.WriteLine(line);
        }
    }
}
