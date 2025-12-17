using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using StullerPokeAPI.Models;

namespace StullerPokeAPI.Services
{
    internal class PokeAPIService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://pokeapi.co/api/v2/";

        //public PokeAPIService()
        //{
        //    _httpClient = new HttpClient
        //    {
        //        BaseAddress = new Uri(_baseUrl)
        //    };
        //}

        internal PokeAPIService(string? baseUrl = null)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseUrl ?? _baseUrl)
            };
        }

        //void PokemonLookup(string? pokemonInput = null)
        //{

        //    if (string.IsNullOrWhiteSpace(pokemonInput))
        //    {
        //        Console.WriteLine("You didn't enter a Pokémon name. Please try again:");
        //        PokemonLookup();
        //        return;
        //    }

        //    Console.WriteLine($"Great choice! Let me fetch the information for {pokemonInput}...");

        //    // Placeholder for API call and response handling

        //    var type = SendPokemonTypeRequestAsync(pokemonInput).Result;
        //    var typeRelations = SendPokemonTypeRelationsRequestAsync(type).Result.TypeRelations;

        //    var strengths = "[Strengths Placeholder]";
        //    var weaknesses = "[Weaknesses Placeholder]";


        //    //write pokemon name input
        //    Console.WriteLine($"Here is the information for {pokemonInput}:");

        //    //write selected pokemon current type
        //    StringBuilder stringBuilder = new();
        //    stringBuilder.Append("Type: ");
        //    stringBuilder.AppendJoin("-", type.Values);

        //        Console.WriteLine($"Type: {type[0]}");




        //    Console.WriteLine($"Strengths: {strengths}");
        //    Console.WriteLine($"Weaknesses: {weaknesses}");
        //    Console.WriteLine("Thank you for using the StullerPokeAPI! Would you like to look up another Pokémon? (y/n)");

        //    var continueInput = Console.ReadLine();
        //    if (continueInput?.ToLower() == "y")
        //    {
        //        // Logic to restart the process
        //        Console.WriteLine("Please enter the name of another Pokémon:");
        //        PokemonLookup();
        //    }
        //    else
        //    {
        //        Console.WriteLine("Goodbye! We hope to see you again soon.");
        //    }
        //}





        internal async Task<Dictionary<int, string>> SendPokemonTypeRequestAsync(string pokemonName)
        {
            Dictionary<int, string> pokemonTypes = [];
            
            var request = new ApiRequestPokemonType { PokemonName = pokemonName };
            Console.WriteLine($"{_httpClient.BaseAddress}pokemon/{request.PokemonName}");

            var endpoint = new Uri($"{_httpClient.BaseAddress}pokemon/{request.PokemonName}");
            string response = await _httpClient.GetStringAsync(endpoint);

            using JsonDocument doc = JsonDocument.Parse(response);
            JsonElement root = doc.RootElement;

            string? name = root.GetProperty("name").GetString();

            Console.WriteLine($"Name: {name}");

            JsonElement types = root.GetProperty("type");
            foreach (JsonElement type in types.EnumerateArray())
            {
                int typeId = type.GetProperty("type").GetProperty("id").GetInt32();
                string? typeName = type.GetProperty("type").GetProperty("name").GetString();
                Console.WriteLine($"TypeId: {typeId} | TypeName: {typeName}");
                pokemonTypes[typeId] = typeName ?? "";
            }

            return pokemonTypes;
        }


        internal async Task<ApiResponseTypeRelations> SendPokemonTypeRelationsRequestAsync(Dictionary<int, string> types)
        {
            ApiResponseTypeRelations? pokemonTypeRelations = null;

            //var jsonstring = JsonSerializer.SerializeAsync(type);

            foreach (var t in types)
            {
                Console.WriteLine($"TypeId: {t.Key} | TypeName: {t.Value}");
            }

            foreach (var t in types)
            {
                Console.WriteLine($"TypeId: {t.Key} | TypeName: {t.Value}");
                var endpoint = new Uri($"{_httpClient.BaseAddress}type/{t.Key}");
                var response = await _httpClient.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<ApiResponseTypeRelations>();
                if (result != null)
                {
                    pokemonTypeRelations = result;
                    //pokemonTypeRelations.TypeRelations.NoDamageFrom.Add(result.TypeRelations ?? new JsonArray());
                }
            }

            // If no successful result, return a default instance to avoid nullability issues
            return pokemonTypeRelations ?? new ApiResponseTypeRelations { TypeRelations = new TypeRelations(), Success = false };
        }
    }
}