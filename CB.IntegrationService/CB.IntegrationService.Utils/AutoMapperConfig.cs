using AutoMapper;
using CB.IntegrationService.Models;
using CB.IntegrationService.ApiModels;

namespace CB.IntegrationService.BLL.Utils
{
    public static class AutoMapperConfig
    {
        /// <summary>
        /// The mapper configuration
        /// </summary>
        public static MapperConfiguration MapperConfiguration;

        static AutoMapperConfig()
        {
            RegisterMappings();
        }

        /// <summary>
        /// Registers the mappings.
        /// </summary>
        private static void RegisterMappings()
        {
            MapperConfiguration = new MapperConfiguration(cfg =>
              {
                  cfg.CreateMap<NotificationAcknowledgeRequest, NotificationAcknowledgeRequestDTO>();
                  cfg.CreateMap<PublishEventRequest, PublishEventRequestDTO>();
              });
        }
    }
}
