using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace SampleProject.API.BaseOperationFilters;

public class EnumOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var enumParams = context.MethodInfo.GetParameters()
            .Where(p => p.ParameterType.IsEnum)
            .ToArray();

        foreach (var param in enumParams)
        {
            var enumType = param.ParameterType;
            var enumDescriptions = Enum.GetValues(enumType)
                .Cast<object>()
                .Select(value =>
                {
                    var name = Enum.GetName(enumType, value);
                    var member = enumType.GetMember(name).FirstOrDefault();
                    var description = member?
                        .GetCustomAttribute<System.ComponentModel.DescriptionAttribute>()?
                        .Description;
                    return (name, description);
                })
                .ToDictionary(pair => pair.name, pair => pair.description);

            var enumParameter = operation.Parameters
                .FirstOrDefault(p => p.Name.Equals(param.Name, StringComparison.OrdinalIgnoreCase));

            if (enumParameter != null)
            {
                var enumDescriptionsObject = new OpenApiObject();
                foreach (var kvp in enumDescriptions)
                {
                    enumDescriptionsObject.Add(kvp.Key, new OpenApiString(kvp.Value));
                }
                enumParameter.Extensions.Add("x-enum-descriptions", enumDescriptionsObject);
            }
        }
    }
}