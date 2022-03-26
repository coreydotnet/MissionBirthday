using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MissionBirthday.Contracts.Models;
using MissionBirthday.Contracts.AzureAi;
using MissionBirthday.Contracts.Repositories;
using MissionBirthday.Logic.AzureAi;
using MissionBirthday.Persistence.Repositories;
using MissionBirthday.Contracts.Events;
using MissionBirthday.Logic.Events;
using MissionBirthday.Logic;
using MissionBirthday.Contracts;

namespace MissionBirthday.Api
{
    public static class MissionBirthdayServices
    {
        public static IServiceCollection ConfigureMissionBirthdayOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<OcrOptions>(configuration.GetSection(nameof(OcrOptions)));
            services.Configure<LanguageServiceOptions>(configuration.GetSection(nameof(LanguageServiceOptions)));
            services.Configure<HoundBotOptions>(configuration.GetSection(nameof(HoundBotOptions)));

            return services;
        }

        public static IServiceCollection AddMissionBirthdayServices(this IServiceCollection services)
        {
            // Singletons
            services.AddSingleton<IOcrService, OcrService>();
            services.AddSingleton<IEntityExtractionService, EntityExtractionService>();
            services.AddSingleton<IEntitiesToEventConverter, EntitiesToEventConverter>();
            services.AddSingleton<IHoundBotService, HoundBotService>();

            // Scoped (usually require a DB repository)
            services.AddScoped<IEventService, EventService>();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IEventRepository, EventRepository>();
            return services;
        }
    }
}
