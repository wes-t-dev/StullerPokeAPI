using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Nodes;
using StullerPokeAPI.Models;

namespace StullerPokeAPI.Services
{
    public class PokeAPIService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://pokeapi.co/api/v2/";

        public PokeAPIService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(_baseUrl)
            };
        }

        public async Task<ApiResponsePokemonType> SendPokemonTypeRequestAsync(string pokemon)
        {
            var request = new ApiRequestPokemonType { PokemonName = pokemon };
            Console.WriteLine($"{_httpClient.BaseAddress}/pokemon/{request.PokemonName}");
            var response = await _httpClient.GetAsync(requestUri: $"{_httpClient.BaseAddress}/pokemon/{request.PokemonName}");
            response.EnsureSuccessStatusCode();

            var pokemonType = await response.Content.ReadFromJsonAsync<ApiResponsePokemonType>();

            return pokemonType;
        }


        public async Task<ApiResponseTypeRelations> SendPokemonTypeRelationsRequestAsync(JsonArray type)
        {
            var request = new ApiRequestTypeRelations { PokemonType = type };
            var response = await _httpClient.GetAsync(requestUri: $"{_baseUrl}{request}");
            response.EnsureSuccessStatusCode();

            var pokemonType = await response.Content.ReadFromJsonAsync<ApiResponseTypeRelations>();

            return pokemonType;
        }
    }
}