using System.Text.Json.Serialization;

namespace InputOutput.Models
{
    public class TestSample
    {
        [JsonPropertyName("testData")]
        public List<TestData> TestData { get; set; }
    }
}
