using System.Text.Json;

namespace CMaaS.Backend.Models
{
    public class ContentType
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        //Store the schema as a JSON document. This allows for flexible and dynamic content types.
        public JsonDocument Schema { get; set; } = JsonDocument.Parse("{}");

        public int TenantId { get; set; }
        public Tenant? Tenant { get; set; } 

    }
}
