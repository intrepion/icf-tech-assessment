using System.Text.Json.Serialization;

namespace InputOutput.Models
{
    public class TestData
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("grant_id")]
        public string GrantId { get; set; }

        [JsonPropertyName("grant_type")]
        public string GrantType { get; set; }
    }
}