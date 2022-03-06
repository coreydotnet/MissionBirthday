using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MissionBirthday.Contracts.Models;
using MissionBirthday.Contracts.Ocr;
using MissionBirthday.Logic.Ocr;

namespace MissionBirthday.Api
{
    public static class MissionBirthdayServices
    {
        public static IServiceCollection ConfigureMissionBirthdayOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<OcrOptions>(configuration.GetSection(nameof(OcrOptions)));

            return services;
        }

        public static IServiceCollection AddMissionBirthdayServices(this IServiceCollection services)
        {
            services.AddSingleton<IOcrService, OcrService>();
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services;
        }
    }
}
