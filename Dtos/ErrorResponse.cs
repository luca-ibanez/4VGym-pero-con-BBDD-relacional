using System.Text.Json.Serialization;

namespace _4VGym_pero_con_BBDD_relacional.Dtos
{
    public class ErrorResponse
    {
        [JsonPropertyName("code")]
        public long Code { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        public ErrorResponse(int code, string description)
        {
            this.Code = code;
            this.Description = description;
        }
    }
}
