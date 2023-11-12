using System.Text.Json.Serialization;

namespace testWorkStudioTG.Models
{
    public class NewGameRequest
    {
        [JsonPropertyName("width")]
        public int Width { get; set; } = 10;
        [JsonPropertyName("height")]
        public int Height { get; set; } = 10;
        [JsonPropertyName("mines_count")]
        public int MinesCount { get; set; } = 10;
    }
}
