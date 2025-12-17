// Models/PokeApiModels.cs
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
        public TypeRelations? TypeRelations { get; set; }
        public bool Success { get; set; }
    }
}

