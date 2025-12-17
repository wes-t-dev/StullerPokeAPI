using System.Text.Json.Serialization;

namespace StullerPokeAPI.Models
{
    internal class TypeRelations
    {
        [JsonPropertyName("double_damage_from")]
        public List<Dictionary<string, string>>? DoubleDamageFrom { get; set; } = [];

        [JsonPropertyName("double_damage_to")]
        public List<Dictionary<string, string>>? DoubleDamageTo { get; set; } = [];

        [JsonPropertyName("half_damage_from")]
        public List<Dictionary<string, string>>? HalfDamageFrom { get; set; } = [];

        [JsonPropertyName("half_damage_to")]
        public List<Dictionary<string, string>>? HalfDamageTo { get; set; } = [];

        [JsonPropertyName("no_damage_from")]
        public List<Dictionary<string, string>>? NoDamageFrom { get; set; } = [];

        [JsonPropertyName("no_damage_to")]
        public List<Dictionary<string, string>>? NoDamageTo { get; set; } = [];
    }
}
