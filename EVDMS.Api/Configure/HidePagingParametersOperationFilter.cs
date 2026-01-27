using System;
using System.Linq;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EVDMS.Api.Configure;

public class HidePagingParametersOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters is null) return;

        operation.Parameters = operation.Parameters
            .Where(p => !string.Equals(p.Name, "skip", StringComparison.OrdinalIgnoreCase)
                     && !string.Equals(p.Name, "take", StringComparison.OrdinalIgnoreCase))
            .ToList();
    }
}
