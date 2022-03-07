using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MissionBirthday.Contracts.Models;
using MissionBirthday.Contracts.Ocr;

namespace MissionBirthday.Logic.Ocr
{
    public class OcrService : IOcrService
    {
        private static readonly TimeSpan requestDelay = TimeSpan.FromSeconds(1);

        private readonly OcrOptions options;
        private readonly ILogger<OcrService> logger;

        public OcrService(ILogger<OcrService> logger, IOptions<OcrOptions> options)
        {
            this.options = options.Value;
            this.logger = logger;
        }

        public async Task<OcrResults> ReadAsync(Stream imageStream)
        {
            using ComputerVisionClient client = CreateClient();

            return await ReadFileUrl(client, imageStream);
        }

        private ComputerVisionClient CreateClient()
        {
            ComputerVisionClient client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(options.DocReaderKey))
            {
                Endpoint = options.DocReaderEndpoint
            };
            return client;
        }

        private async Task<OcrResults> ReadFileUrl(ComputerVisionClient client, Stream imageStream)
        {
            OcrResults result = null;

            try
            {
                //var textHeaders = await client.ReadAsync(fileUrl);
                var textHeaders = await client.ReadInStreamAsync(imageStream);

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


                if (results.Status == OperationStatusCodes.Succeeded)
                {
                    var textUrlFileResults = results.AnalyzeResult.ReadResults;
                    result = new OcrResults(textUrlFileResults.SelectMany(page => page.Lines).Select(line => line.Text));
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to process image stream");
                throw;
            }

            return result;
        }
    }
}
