using System.Text.Json.Serialization;

namespace testWorkStudioTG.Models
{
    public class GameInfoResponse : NewGameRequest
    {
        [JsonPropertyName("game_id")]
        public Guid GameID { get; set; }
        [JsonPropertyName("completed")]
        public bool Completed { get; set; }
        [JsonPropertyName("field")]
        public string[][] Field { get; set; }
    }
}
