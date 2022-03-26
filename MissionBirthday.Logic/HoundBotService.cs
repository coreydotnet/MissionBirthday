using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MissionBirthday.Contracts;
using MissionBirthday.Contracts.Models;
using Newtonsoft.Json;

namespace MissionBirthday.Logic
{
    public class HoundBotService : IHoundBotService
    {
        private readonly HoundBotOptions options;

        public HoundBotService(IOptions<HoundBotOptions> options)
        {
            this.options = options.Value;
        }

        public async Task<HoundBotChatConfig> GetChatConfigAsync()
        {
            using var client = new HttpClient();

            HttpRequestMessage request = new HttpRequestMessage(
                HttpMethod.Post,
                "https://directline.botframework.com/v3/directline/tokens/generate");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", options.DirectLineSecret);

            // Must start with "dl_"
            var userId = $"dl_{Guid.NewGuid()}";

            request.Content = new StringContent(
                JsonConvert.SerializeObject(
                    new
                    {
                        User = new
                        {
                            Id = userId
                        }
                    }),
                    Encoding.UTF8,
                    "application/json");

            var response = await client.SendAsync(request);
            string token = string.Empty;

            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                token = JsonConvert.DeserializeObject<DirectLineToken>(body).token;
            }

            var config = new HoundBotChatConfig()
            {
                Token = token,
                UserId = userId
            };

            return config;
        }

        private class DirectLineToken
        {
            public string conversationId { get; set; }
            public string token { get; set; }
            public int expires_in { get; set; }
        }
    }
}
