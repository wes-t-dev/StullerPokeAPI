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


PokeAPIService pokeAPIService = new PokeAPIService();

Console.WriteLine("Hello, and welcome to the wonderful world of Pokémon!");
Console.WriteLine("Please enter your name:");

var user = Console.ReadLine();

if (string.IsNullOrWhiteSpace(user))
{
    user = "Mystery Trainer.";
}
Console.WriteLine($"Nice to meet you {user}");


Console.WriteLine(@"If you provide the name of a Pokémon, I can tell you its type, strengths, and weaknesses.
Which Pokémon would you like to learn about today?");

PokemonLookup();





void PokemonLookup()
{
    var pokemonInput = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(pokemonInput))
    {
        Console.WriteLine("You didn't enter a Pokémon name. Please try again:");
        PokemonLookup();
        return;
    }

    Console.WriteLine($"Great choice! Let me fetch the information for {pokemonInput}...");

    // Placeholder for API call and response handling

    var type = pokeAPIService.SendPokemonTypeRequestAsync(pokemonInput).Result.PokemonType;
    var typeRelations = pokeAPIService.SendPokemonTypeRelationsRequestAsync(type).Result.TypeRelations;

    var strengths = "[Strengths Placeholder]";
    var weaknesses = "[Weaknesses Placeholder]";



    Console.WriteLine($"Here is the information for {pokemonInput}:");
    Console.WriteLine($"Type: {type}");
    Console.WriteLine($"Strengths: {strengths}");
    Console.WriteLine($"Weaknesses: {weaknesses}");
    Console.WriteLine("Thank you for using the StullerPokeAPI! Would you like to look up another Pokémon? (y/n)");

    var continueInput = Console.ReadLine();
    if (continueInput?.ToLower() == "y")
    {
        // Logic to restart the process
        Console.WriteLine("Please enter the name of another Pokémon:");
        PokemonLookup();
    }
    else
    {
        Console.WriteLine("Goodbye! Hope to see you again soon.");
    }
}