using System.Text.Json;
using System.Text.Json.Serialization;
using Aspire.ProjectTemplate.Core.Models;

namespace Aspire.ProjectTemplate.Web.Converters;

public class PagedListConverter<T> : JsonConverter<PagedList<T>>
{
    public override PagedList<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // Create a temporary object to hold the deserialized data
        var pageNumber = 0;
        var pageSize = 0;
        var totalCount = 0;
        var items = new List<T>();

        while(reader.Read())
        {
            if(reader.TokenType == JsonTokenType.EndObject)
            {
                break;
            }

            // Check the property name
            if(reader.TokenType == JsonTokenType.PropertyName)
            {
                var propertyName = reader.GetString();
                reader.Read(); // Move to the value

                switch(propertyName)
                {
                    case "pageNumber":
                        pageNumber = reader.GetInt32();
                        break;
                    case "pageSize":
                        pageSize = reader.GetInt32();
                        break;
                    case "totalCount":
                        totalCount = reader.GetInt32();
                        break;
                    case "items":
                        // Deserialize items
                        using(var doc = JsonDocument.ParseValue(ref reader))
                        {
                            items = JsonSerializer.Deserialize<List<T>>(doc.RootElement.GetRawText(), options);
                        }

                        break;
                }
            }
        }

        return new PagedList<T>(items ?? [], totalCount, pageNumber, pageSize);
    }

    public override void Write(Utf8JsonWriter writer, PagedList<T> value, JsonSerializerOptions options)
    {
        // Implement write logic if necessary (not required for your current scenario)
        throw new NotImplementedException();
    }
}
