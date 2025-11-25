using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SalesApi;

public class SwaggerFileOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var fileParams = context.ApiDescription.ParameterDescriptions
            .Where(p => p.ModelMetadata?.ModelType == typeof(IFormFile))
            .ToList();

        if (!fileParams.Any())
            return;

        if (operation.Parameters != null)
        {
            foreach (var fileParam in fileParams)
            {
                var paramToRemove = operation.Parameters.FirstOrDefault(p => p.Name == fileParam.Name);
                if (paramToRemove != null)
                    operation.Parameters.Remove(paramToRemove);
            }
        }

        operation.RequestBody = new OpenApiRequestBody
        {
            Content = new Dictionary<string, OpenApiMediaType>
            {
                ["multipart/form-data"] = new OpenApiMediaType
                {
                    Schema = new OpenApiSchema
                    {
                        Type = JsonSchemaType.Object,
                        Properties = new Dictionary<string, IOpenApiSchema>(
                            fileParams.Select(p => new KeyValuePair<string, IOpenApiSchema>(
                                p.Name,
                                new OpenApiSchema
                                {
                                    Type = JsonSchemaType.String,
                                    Format = "binary"
                                }
                            ))
                        ),
                        Required = fileParams.Select(p => p.Name).ToHashSet()
                    }
                }
            }
        };
    }
}
