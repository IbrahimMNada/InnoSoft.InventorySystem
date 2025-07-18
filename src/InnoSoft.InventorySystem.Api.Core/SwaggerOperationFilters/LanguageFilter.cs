using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace InnoSoft.InventorySystem.Api.Core.SwaggerOperationFilters
{
    public class LanguageFilter : IOperationFilter
    {

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "x-Accept-Language",
                In = ParameterLocation.Header,
                Required = false
            });
        }
    }
}
