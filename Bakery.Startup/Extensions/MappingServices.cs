using Bakery.Infrastructure.Web.Buns.Mapper;

namespace Bakery.Startup.Extensions;

///<summary>
/// Статический класс сервисов мапинга.
///</summary>
public static class MappingServices
{
    ///<summary>
    /// Расширяющий метод для добавления сервисов маппинга в коллекцию служб.
    ///</summary>
    ///<param name="services">Коллекция служб.</param>
    public static void AddMappingServices(this IServiceCollection services)
    {
        // Добавляем AutoMapper в сервисы
        services.AddAutoMapper(cfg =>
        {
            // Регистрируем карты для контроллеров
            cfg.AddMaps(typeof(BunsMapperProfile).Assembly);
        });
    }
}