using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MissionBirthday.Contracts.Ocr;
using MissionBirthday.Logic.Ocr;

namespace MissionBirthday.Api
{
    public static class ServicesExtensions
    {
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
