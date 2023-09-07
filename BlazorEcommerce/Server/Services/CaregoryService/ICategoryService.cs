namespace BlazorEcommerce.Server.Services.CaregoryService
{
    public interface ICategoryService
    {
        Task<ServiceResponse<List<Category>>> GetCategories();
    }
}
