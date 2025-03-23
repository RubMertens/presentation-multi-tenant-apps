using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PokeCenter.web.Pages;

public class IndexModel(
    ILogger<IndexModel> logger
) : PageModel
{
    private readonly ILogger<IndexModel> _logger = logger;

    public void OnGet()
    {
    }
}