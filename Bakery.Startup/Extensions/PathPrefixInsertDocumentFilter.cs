using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bakery.Startup.Extensions;

/// <summary>
/// Фильтр документа, который добавляет префикс к путям в документе Swagger.
/// </summary>
public class PathPrefixInsertDocumentFilter(string prefix) : IDocumentFilter
{
    /// <summary>
    /// Применяет фильтр к документу Swagger.
    /// </summary>
    /// <param name="swaggerDoc">Документ Swagger, к которому применяется фильтр.</param>
    /// <param name="context">Контекст фильтра документа.</param>
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        // Получаем все пути из документа Swagger
        var paths = swaggerDoc.Paths.Keys.ToArray();

        // Проходим по каждому пути
        foreach (var path in paths)
        {
            // Получаем объект пути, который нужно изменить
            var pathToChange = swaggerDoc.Paths[path];

            // Удаляем старый путь из документа
            swaggerDoc.Paths.Remove(path);

            // Добавляем новый путь с префиксом
            swaggerDoc.Paths.Add("/" + prefix + path, pathToChange);
        }
    }
}
