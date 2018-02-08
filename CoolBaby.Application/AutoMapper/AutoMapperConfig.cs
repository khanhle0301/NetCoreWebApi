using AutoMapper;

namespace CoolBaby.Application.AutoMapper
{
    /// <summary>
    /// Auto Mapper Config class
    /// </summary>
    public class AutoMapperConfig
    {
        #region Methods
        /// <summary>
        /// Register Mappings
        /// </summary>
        /// <returns></returns>
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DomainToViewModelMappingProfile());
                cfg.AddProfile(new ViewModelToDomainMappingProfile());
            });
        }
        #endregion
    }
}