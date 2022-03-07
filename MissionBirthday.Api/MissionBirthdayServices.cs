using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MissionBirthday.Contracts;
using MissionBirthday.Contracts.Models;
using MissionBirthday.Contracts.Ocr;
using MissionBirthday.Contracts.Repositories;
using MissionBirthday.Logic;
using MissionBirthday.Logic.Ocr;
using MissionBirthday.Persistence.Repositories;

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
            services.AddScoped<IEventManager, EventManager>();
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IEventRepository, EventRepository>();
            return services;
        }
    }
}
