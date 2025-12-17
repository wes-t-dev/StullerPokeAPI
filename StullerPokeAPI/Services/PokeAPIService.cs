using StullerPokeAPI.Models;
using System.Text.Json;

namespace StullerPokeAPI.Services
{
    internal class PokeAPIService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://pokeapi.co/api/v2/";


        internal PokeAPIService(string? baseUrl = null)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseUrl ?? _baseUrl)
            };
        }


        internal async Task<Dictionary<string, string>?> SendPokemonTypeRequestAsync(string pokemonName)
        {
            Dictionary<string, string>? pokemonTypes = [];
            
            var request = new ApiRequestPokemonType { PokemonName = pokemonName };

            try
            {
                var endpoint = new Uri($"{_httpClient.BaseAddress}pokemon/{request.PokemonName}");
                var response = await _httpClient.GetAsync(endpoint);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    using JsonDocument doc = JsonDocument.Parse(jsonResponse);
                    JsonElement root = doc.RootElement;

                    string? name = root.GetProperty("name").GetString();

                    JsonElement types = root.GetProperty("types");
                    foreach (JsonElement type in types.EnumerateArray())
                    {
                        string typeName = type.GetProperty("type").GetProperty("name").GetString() ?? "";
                        string typeUrl = type.GetProperty("type").GetProperty("url").GetString() ?? "";

                        pokemonTypes[typeName] = typeUrl;
                    }

                    return pokemonTypes;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($" Failed to catch Pokemon: '{request.PokemonName}'! Please try again.");
                    Console.ResetColor();
                    return null;
                }
            }

            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($" Request failed: {ex.Message}");
                Console.ResetColor();
                return null;
            }
        }


        internal async Task<ApiResponseTypeRelations?> SendPokemonTypeRelationsRequestAsync(Dictionary<string, string>? types, ApiResponseTypeRelations? pokemonTypeRelations = null)
        {
            try
            {
                if (types != null)
                {
                    foreach (var t in types)
                    {
                        var endpoint = new Uri(t.Value);
                        var response = await _httpClient.GetAsync(endpoint);

                        if (response.IsSuccessStatusCode)
                        {
                            string jsonResponse = await response.Content.ReadAsStringAsync();
                            var result = JsonSerializer.Deserialize<ApiResponseTypeRelations>(jsonResponse);
                            if (result != null)
                            {
                                result.Success = true;
                                pokemonTypeRelations = result;
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($" Failed to find damage relations for type '{t.Key}'! Please try again.");
                            Console.ResetColor();
                            return null;
                        }
                    }
                    
                 }

                return pokemonTypeRelations;
            }

            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($" Request failed: {ex.Message}");
                Console.ResetColor();
                return null;
            }
        }
    }
}