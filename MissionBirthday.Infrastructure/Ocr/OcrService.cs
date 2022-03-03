using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;

namespace MissionBirthday.Infrastructure.Ocr
{
    public class OcrService
    {
        private static readonly TimeSpan requestDelay = TimeSpan.FromSeconds(1);

        public async Task<OcrResults> ReadAsync(string endpoint, string subscriptionKey, string fileUrl)
        {
            using ComputerVisionClient client = Authenticate(endpoint, subscriptionKey);

            return await ReadFileUrl(client, fileUrl);
        }

        private ComputerVisionClient Authenticate(string endpoint, string subscriptionKey)
        {
            ComputerVisionClient client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(subscriptionKey))
            {
                Endpoint = endpoint
            };
            return client;
        }

        private async Task<OcrResults> ReadFileUrl(ComputerVisionClient client, string fileUrl)
        {
            var textHeaders = await client.ReadAsync(fileUrl);

            string operationLocation = textHeaders.OperationLocation;

            // We only need the ID and not the full URL
            const int numberOfCharactersInOperationId = 36;
            string operationId = operationLocation.Substring(operationLocation.Length - numberOfCharactersInOperationId);
            Guid operationGuid = Guid.Parse(operationId);

            // Extract the text
            ReadOperationResult results;

            await Task.Delay(requestDelay);
            do
            {
                // free tier limit of 20 calls per minute
                await Task.Delay(requestDelay);
                results = await client.GetReadResultAsync(operationGuid);
            } while (results.Status == OperationStatusCodes.Running || results.Status == OperationStatusCodes.NotStarted);

            OcrResults result = null;

            if (results.Status == OperationStatusCodes.Succeeded)
            {
                var textUrlFileResults = results.AnalyzeResult.ReadResults;
                result = new OcrResults(textUrlFileResults.SelectMany(page => page.Lines).Select(line => line.Text));
            }

            return result;
        }
    }
}
