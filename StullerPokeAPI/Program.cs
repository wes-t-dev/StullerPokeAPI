/**************************************************
 * 
 * Weston Twiner  12/12/2025                       
 * StullerPokeAPI Assignment Project               
  
 * This file contains the core program for the StullerPokeAPI project.
 * 
**************************************************/


using System;
using StullerPokeAPI.Services;
using StullerPokeAPI.Models;
using System.Runtime.CompilerServices;


PokeAPIService pokeAPIService = new();
string pokemonName, user;

Welcome();
//GetPokemonName();
//PokemonLookup(pokemonName);
//WritePokemonTypeInformation();




void Welcome()
{
    Console.WriteLine(@"Hello, and welcome to the wonderful world of Pokémon! 
    What is your name?");

    var newUser = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(newUser))
    {
        user = "Mysterious Trainer.";
    }
    else
    {
        user = newUser;
    }
    Console.WriteLine($"Nice to meet you {user}");
    GetPokemonName();
}


void GetPokemonName()
{
    Console.WriteLine(@"If you provide the name of a Pokémon, I can tell you its type, strengths, and weaknesses.
    Which Pokémon would you like to learn about today?");
    Console.Write(": ");
    string? pokemonNameInput = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(pokemonNameInput))
    {
        Console.WriteLine("I didn't CATCH that. Please try again!");
        GetPokemonName();
    }
    else
    {
        pokemonName = pokemonNameInput;
        Console.WriteLine($"Great choice! Let me gather the information for {pokemonName}...");
        PokemonLookup(pokemonName);
    }
}

void PokemonLookup(string pokemonName)
{

    // Placeholder for API call and response handling

    var type = pokeAPIService.SendPokemonTypeRequestAsync(pokemonName).Result;
    var typeRelations = pokeAPIService.SendPokemonTypeRelationsRequestAsync(type).Result.TypeRelations;


    // Build strengths and weaknesses from typeRelations
    var strengths = typeRelations != null ? $"{typeRelations.NoDamageFrom}, {typeRelations.HalfDamageFrom}, {typeRelations.DoubleDamageTo}" : "[No Type Relations Found]";
    var weaknesses = typeRelations != null ? $"{typeRelations.NoDamageTo}, {typeRelations.HalfDamageTo}, {typeRelations.DoubleDamageFrom}" : "[No Type Relations Found]";

    WritePokemonTypeInformation(type, strengths, weaknesses);
}


void WritePokemonTypeInformation(Dictionary<int, string> pokemonType, string strengths, string weaknesses)
{
    Console.WriteLine($"Here is the information for {pokemonName}:");
    Console.WriteLine($"Type: {pokemonType.Values}");
    Console.WriteLine($"Strengths: {strengths}");
    Console.WriteLine($"Weaknesses: {weaknesses}");
    Console.WriteLine("Thank you for using the StullerPokeAPI! Would you like to look up another Pokémon? (y/n)");

    var continueInput = Console.ReadLine();
    if (continueInput?.ToLower() == "y")
    {
        // Logic to restart the process
        Console.WriteLine("Wonderful, I'm glad you are excited to learn more!");
        GetPokemonName();
    }
    else
    {
        Console.WriteLine($"Thank you for visiting, we hope to see you again soon {user}!");
    }
}