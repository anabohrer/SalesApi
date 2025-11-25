using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SalesApi;

/// <summary>
/// Swagger operation filter that configures file upload parameters for OpenAPI documentation.
/// This filter converts IFormFile parameters to multipart/form-data binary uploads in the Swagger UI.
/// </summary>
public class SwaggerFileOperationFilter : IOperationFilter
{
    /// <summary>
    /// Applies the filter to the OpenAPI operation, converting IFormFile parameters to proper file upload schema.
    /// </summary>
    /// <param name="operation">The OpenAPI operation to modify.</param>
    /// <param name="context">The operation filter context containing API metadata.</param>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        // Find all parameters that are IFormFile types
        var fileParams = context.ApiDescription.ParameterDescriptions
            .Where(p => p.ModelMetadata?.ModelType == typeof(IFormFile))
            .ToList();

        if (!fileParams.Any())
            return;

        // Remove IFormFile parameters from the default parameter list
        // as they will be added to the request body instead
        if (operation.Parameters != null)
        {
            foreach (var fileParam in fileParams)
            {
                var paramToRemove = operation.Parameters.FirstOrDefault(p => p.Name == fileParam.Name);
                if (paramToRemove != null)
                    operation.Parameters.Remove(paramToRemove);
            }
        }

        // Configure the request body as multipart/form-data with binary file uploads
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
