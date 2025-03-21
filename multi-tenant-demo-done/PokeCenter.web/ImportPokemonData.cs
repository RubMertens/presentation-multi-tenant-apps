using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.EntityFrameworkCore;
using Pokedex.Data;
using Pokedex.Data.Models;
using Pokedex.Framework.Tenants;

namespace PokeCenter.web;

public class ImportPokemonData(ApplicationDbContext context)
{
    public async Task Import()
    {
        if (await CheckIfDataExists())
            return;

        var pokemonJson = await File.ReadAllTextAsync("pokedex.json");
        var json = JsonSerializer.Deserialize<JsonArray>(pokemonJson);
        var pokemons = json.Select(p => new Pokemon
        {
            Index = p["id"].GetValue<int>(),
            Name = p["name"]["english"].GetValue<string>()
        });

        context.Pokemons.AddRange(pokemons);
        await context.SaveChangesAsync();
    }

    private async Task<bool> CheckIfDataExists()
    {
        return await context.Pokemons.AnyAsync();
    }
}