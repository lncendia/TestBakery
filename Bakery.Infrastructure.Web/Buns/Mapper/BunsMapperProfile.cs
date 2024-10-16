using Bakery.Application.Commands.Buns;
using Bakery.Application.Queries.Buns;
using Bakery.Infrastructure.Web.Buns.InputModels;

namespace Bakery.Infrastructure.Web.Buns.Mapper;

/// <summary>
/// Класс для маппинга входных моделей для работы с булочками в команды
/// </summary>
public class BunsMapperProfile : AutoMapper.Profile
{
    /// <summary>
    /// Маппинг входных моделей в команды
    /// </summary>
    public BunsMapperProfile()
    {
        // Карта для GetBunsInputModel в GetBunsQuery
        CreateMap<GetBunsInputModel, GetBunsQuery>();
        
        // Карта для StartBakingInputModel в StartBakingCommand
        CreateMap<StartBakingInputModel, StartBakingCommand>();
    }
}