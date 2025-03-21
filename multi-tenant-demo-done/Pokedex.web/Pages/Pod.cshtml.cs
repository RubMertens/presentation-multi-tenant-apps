using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pokedex.Data;
using Pokedex.Data.Models;

namespace Pokedex.web.Pages;

public record PokemonAdmissionViewModel
{
    public string? Image { get; init; }
    public required bool IsFull { get; init; }
    public required string Name { get; set; }
    public required string TrainerName { get; set; }
    public required DateTime AdmissionStart { get; set; }
    public required DateTime AdmissionEnd { get; set; }
    public required int PokemonIndex { get; set; }
}

public record PokemonEntryViewModel
{
    public required string Name { get; init; }
    public required int Index { get; init; }
}

public class Pod(ApplicationDbContext context) : PageModel
{
    [BindProperty(SupportsGet = true)] public DateTime Today { get; set; } = DateTime.Now;
    [BindProperty] public int PodId { get; set; }
    [BindProperty] public string TrainerName { get; set; }
    [BindProperty] public int PokemonIndex { get; set; }
    [BindProperty] public DateTime AdmissionStart { get; set; } = DateTime.Now;
    [BindProperty] public DateTime AdmissionEnd { get; set; } = DateTime.Now.AddDays(3);

    public List<PokemonAdmissionViewModel> Admissions { get; set; }
    public List<PokemonEntryViewModel> Pokemons { get; set; }

    public async Task<IActionResult> OnGet(int podId)
    {
        PodId = podId;
        if (!await context.Pods.AnyAsync(p => p.Id == podId))
            return RedirectToPage("Index");

        await BuildViewModels(podId);

        return Page();
    }

    private async Task BuildViewModels(int podId)
    {
        var capacity = await context.Pods
            .Where(p => p.Id == podId)
            .Select(p => p.MaxCapacity)
            .FirstOrDefaultAsync();

        Pokemons = await context.Pokemons
            .Select(p => new PokemonEntryViewModel
            {
                Name = p.Name,
                Index = p.Index
            }).ToListAsync();


        var now = DateTime.Now;
        Admissions = await context.Admissions
            .Where(a => a.PodId == podId)
            .Where(a => a.AdmissionStart <= now && a.AdmissionEnd >= now)
            .Select(a => new PokemonAdmissionViewModel
            {
                Image = GeneratePokemonSpriteUrl(a.Pokemon.Index),
                IsFull = true,
                Name = a.Pokemon.Name,
                TrainerName = a.Trainer,
                AdmissionStart = a.AdmissionStart,
                AdmissionEnd = a.AdmissionEnd,
                PokemonIndex = a.Pokemon.Index
            }).ToListAsync();

        if (Admissions.Count < capacity)
        {
            //add empty slots until capacity
            var emptySlotCount = capacity - Admissions.Count;
            var emptySlots = new List<PokemonAdmissionViewModel>();
            for (var i = 0; i < emptySlotCount; i++)
            {
                emptySlots.Add(new PokemonAdmissionViewModel
                {
                    IsFull = false,
                    Name = "Empty",
                    TrainerName = "Empty",
                    AdmissionStart = DateTime.Now,
                    AdmissionEnd = DateTime.Now,
                    PokemonIndex = 0
                });
            }

            Admissions.AddRange(emptySlots);
        }
    }

    private static string GeneratePokemonSpriteUrl(int index)
    {
        var formattedNumber = index.ToString("0000");
        var uri =
            $"https://raw.githubusercontent.com/PMDCollab/SpriteCollab/master/portrait/{formattedNumber}/Normal.png";
        return uri;
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            await BuildViewModels(PodId);
            return Page();
        }

        var admission = new PokemonAdmission
        {
            AdmissionStart = AdmissionStart,
            AdmissionEnd = AdmissionEnd,
            Trainer = TrainerName,
            PokemonId = PokemonIndex,
            PodId = PodId
        };

        context.Admissions.Add(admission);

        await context.SaveChangesAsync();

        return RedirectToPage("Pod", new { podId = PodId });
    }
}