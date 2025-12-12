using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;


// Models/PokeApiModels.cs
namespace StullerPokeAPI.Models
{
    public class ApiRequestPokemonType
        {
            public string? PokemonName { get; set; }
        }
    
    public class ApiResponsePokemonType
        {
            public JsonArray? PokemonType { get; set; }
            public bool Success { get; set; }
        }

    public class ApiRequestTypeRelations
    {
        public JsonArray? PokemonType { get; set; }
    }

    public class ApiResponseTypeRelations
    {
        public JsonArray? TypeRelations { get; set; }
        public bool Success { get; set; }
    }
}

