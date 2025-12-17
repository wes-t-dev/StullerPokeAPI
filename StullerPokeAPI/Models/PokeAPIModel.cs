using System.Text.Json.Serialization;

namespace StullerPokeAPI.Models
{
    internal class ApiRequestPokemonType
        {
            public string? PokemonName { get; set; }
        }

    internal class ApiRequestTypeRelations
    {
        public int PokemonType { get; set; }
    }

    internal class ApiResponseTypeRelations
    {
        [JsonPropertyName("damage_relations")]
        public TypeRelations? TypeRelations { get; set; } = null;
        public bool Success { get; set; }
    }
}

