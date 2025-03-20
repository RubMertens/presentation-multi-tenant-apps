using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pokedex.Data;
using Pokedex.Data.Models;

namespace Pokedex.web.Pages;

public record PodViewModel
{
    public required int Id { get; init; }
    public int MaxCapacity { get; set; }
    public int CurrentCapacity { get; set; }
}

public class Pods(ApplicationDbContext context) : PageModel
{
    public PodViewModel[] ViewModel { get; set; }

    [BindProperty] public int MaxCapacity { get; set; }

    public async Task OnGet()
    {
        await LoadViewModel();
    }

    private async Task LoadViewModel()
    {
        ViewModel = await context.Pods
            .Select(p => new PodViewModel
            {
                Id = p.Id,
                MaxCapacity = p.MaxCapacity,
                CurrentCapacity = p.Admission
                    .Count(a => a.AdmissionEnd > DateTime.Now && a.AdmissionStart < DateTime.Now)
            }).ToArrayAsync();
    }

    public async Task<IActionResult> OnPost()
    {
        if (MaxCapacity <= 0)
        {
            ModelState.AddModelError(nameof(MaxCapacity), "Max capacity must be greater than 0");
            await LoadViewModel();
            return Page();
        }

        var pod = new HealingPod { MaxCapacity = MaxCapacity };
        context.Pods.Add(pod);
        await context.SaveChangesAsync();
        return RedirectToPage();
    }
}