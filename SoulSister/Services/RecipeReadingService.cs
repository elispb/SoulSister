using Microsoft.AspNetCore.Http;
using SoulSister.DataAccess;
using System.Threading.Tasks;
using Tesseract;

namespace SoulSister.Services;

public class RecipeReadingService : IRecipeReadingService
{
    private IRecipeDataAccess _recipeDataAccess;

    public RecipeReadingService(IRecipeDataAccess dataAccess)
    {
        _recipeDataAccess = dataAccess;
    }

    public async Task<string> RecipeImageToRecipe(IFormFile recipeImage)
    {
        //Save to temp Drive
        //await _recipeDataAccess.SaveRawRecipe(recipeImage);

        using var fileStream = recipeImage.OpenReadStream();
        byte[] bytes = new byte[recipeImage.Length];
        fileStream.Read(bytes, 0, (int)recipeImage.Length);

        using var engine = new TesseractEngine("", "eng");
        using var img = Pix.LoadFromMemory(bytes);
        using var page = engine.Process(img);

        return page.GetText();
    }

}
