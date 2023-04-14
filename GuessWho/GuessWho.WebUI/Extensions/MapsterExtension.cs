using GuessWho.Domain.Dtos;
using Mapster;

namespace GuessWho.WebUI.Extensions;

public static class MapsterExtension
{
    public static void AddMapster(this IServiceCollection services)
    {
        var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
        var applicationAssembly = typeof(BaseDto<,>).Assembly;
        typeAdapterConfig.Scan(applicationAssembly);
    }
}