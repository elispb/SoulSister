using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace SoulSister.Services
{
    public interface IRecipeReadingService
    {
        Task<string> RecipeImageToRecipe(IFormFile recipeImage);
    }
}