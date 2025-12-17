/**************************************************
 * 
 * Weston Twiner  12/12/2025                       
 * StullerPokeAPI Assignment Project               
  
 * This file contains the core program for the StullerPokeAPI project.
 * 
**************************************************/


using StullerPokeAPI.Services;
using StullerPokeAPI.Models;

Console.BackgroundColor = ConsoleColor.DarkBlue;
Console.ForegroundColor = ConsoleColor.White;

PokeAPIService pokeAPIService = new();
string pokemonName, user;

Welcome();
//GetPokemonName();
//PokemonLookup(pokemonName);
//WritePokemonTypeInformation();




void Welcome()
{
    Console.WriteLine(" Hello, and welcome to the wonderful world of Pokémon!");
    Console.WriteLine(" I am your StullerPokeAPI guide, here to help you.");
    Console.WriteLine("\n\n     What is your name?");

    Console.Write(" : ");
    var newUser = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(newUser))
    {
        user = "Mysterious Trainer.";
    }
    else
    {
        user = newUser;
    }
    Console.WriteLine($" Nice to meet you {user}");
    GetPokemonName();
}


void GetPokemonName()
{
    Console.WriteLine(" If you provide the name of a Pokémon, I can tell you its type, strengths, and weaknesses.");
    Console.WriteLine("\n\n     Which Pokémon would you like to learn about today?");
    Console.Write(": ");
    string? pokemonNameInput = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(pokemonNameInput))
    {
        Console.WriteLine(" I didn't CATCH that. Please try again!");
        GetPokemonName();
    }
    else
    {
        pokemonName = pokemonNameInput;
        Console.WriteLine($" Let me gather the information for {pokemonName}...");
        PokemonLookup(pokemonName);
    }
}

void PokemonLookup(string pokemonName)
{

    // Get types and type relations from PokeAPIService

    var types = pokeAPIService.SendPokemonTypeRequestAsync(pokemonName).Result ?? [];
    var responseTypeRelations = pokeAPIService.SendPokemonTypeRelationsRequestAsync(types).Result ?? new ApiResponseTypeRelations();
    var typeRelations = responseTypeRelations.TypeRelations ?? new TypeRelations();

    var requestsSuccessful = types.Count > 0 && responseTypeRelations.Success && typeRelations?.ToString()?.Length > 0;


    // Build strengths and weaknesses from typeRelations

    var noDamageFrom = string.Join(", ", typeRelations?.NoDamageFrom?.ConvertAll(t => t["name"]) ?? []);
    var halfDamageFrom = string.Join(", ", typeRelations?.HalfDamageFrom?.ConvertAll(t => t["name"]) ?? []);
    var doubleDamageTo = string.Join(", ", typeRelations?.DoubleDamageTo?.ConvertAll(t => t["name"]) ?? []);

    var strengths = $"{(string.IsNullOrWhiteSpace(noDamageFrom) ? "" : noDamageFrom)}{(string.IsNullOrWhiteSpace(halfDamageFrom) ? "" : $", {halfDamageFrom}")}{(string.IsNullOrWhiteSpace(doubleDamageTo) ? "" : $", {doubleDamageTo}")}";
    if (strengths == ", , " || strengths.Length == 0)
    {
        strengths = "None";
    }

    var noDamageTo = string.Join(", ", typeRelations?.NoDamageTo?.ConvertAll(t => t["name"]) ?? []);
    var halfDamageTo = string.Join(", ", typeRelations?.HalfDamageTo?.ConvertAll(t => t["name"]) ?? []);
    var doubleDamageFrom = string.Join(", ", typeRelations?.DoubleDamageFrom?.ConvertAll(t => t["name"]) ?? []);
    var weaknesses = $"{(string.IsNullOrWhiteSpace(noDamageTo) ? "" : noDamageTo)}{(string.IsNullOrWhiteSpace(halfDamageTo) ? "" : $", {halfDamageTo}")}{(string.IsNullOrWhiteSpace(doubleDamageFrom) ? "" : $", {doubleDamageFrom}")}";
    if (weaknesses == ", , " || weaknesses.Length == 0)
    {
        weaknesses = "None";
    }

    WritePokemonTypeInformation(requestsSuccessful, types, strengths, weaknesses);
}


void WritePokemonTypeInformation(bool requestSuccess, Dictionary<string, string> pokemonType, string strengths, string weaknesses)
{

    if (requestSuccess == false)
    {
        Console.WriteLine($"\n\n");
        Console.WriteLine($" I'm sorry {user}, but I couldn't find any information for the Pokémon '{pokemonName}'. Please check the name and try again.");
        GetPokemonName();
        return;
    }
    else
    {
        Console.WriteLine($" Great choice! Here is the information for {pokemonName}:");
        Console.WriteLine($" Type: {string.Join(", ", pokemonType.Keys)}");
        Console.WriteLine(strengths == "None" ? " This Pokémon is not strong against any type" : $" Strong Against: {strengths}");
        Console.WriteLine(weaknesses == "None" ? " This Pokémon is not weak against any type" : $" Weak Against: {weaknesses}");
        Console.WriteLine("\n\n     Would you like to look up another Pokémon? (y/n)");

        Console.Write(": ");
        var continueInput = Console.ReadLine();
        if (continueInput?.ToLower() == "y")
        {
            // Logic to restart the process
            Console.WriteLine("\n\n Wonderful, I'm glad you are excited to learn more!");
            GetPokemonName();
        }
        else
        {
            Console.WriteLine($"\n\n    Thank you for using the StullerPokeAPI, we hope to see you again soon {user}!");
        }
    }
}
