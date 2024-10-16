using Bakery.Application.Commands.Authentication;
using Bakery.Infrastructure.Web.Account.InputModels;

namespace Bakery.Infrastructure.Web.Account.Mapper;

/// <summary>
/// Класс для маппинга входных моделей для работы с аккаунтом
/// </summary>
public class AccountMapperProfile : AutoMapper.Profile
{
    /// <summary>
    /// Маппинг входных моделей в команды
    /// </summary>
    public AccountMapperProfile()
    {
        // Карта для LoginInputModel в AuthenticateCommand
        CreateMap<LoginInputModel, AuthenticateCommand>();
    }
}