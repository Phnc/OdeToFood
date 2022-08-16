using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class EditModel : PageModel
    {
        private readonly IRestaurantData _restaurantData;
        private readonly IHtmlHelper _htmlHelper;

        [BindProperty]
        public Restaurant Restaurant { get; set; }

        public IEnumerable<SelectListItem> CuisineTypes { get; set; }

        public EditModel(IRestaurantData restaurantData, IHtmlHelper htmlHelper)
        {
            _restaurantData = restaurantData;
            _htmlHelper = htmlHelper;
        }

        public IActionResult OnGet(int? restaurantId)
        {
            if(restaurantId != null)
            {
                Restaurant = _restaurantData.GetRestaurantById(restaurantId.Value);
            }
            else
            {
                Restaurant = new Restaurant();
            }

            if (Restaurant == null)
            {
                return RedirectToPage("./NotFound");
            }

            PopulateCuisineTypesSelect();

            return Page();
        }

        public void PopulateCuisineTypesSelect()
        {
            CuisineTypes = _htmlHelper.GetEnumSelectList<CuisineType>();
        }

        public IActionResult OnPost()
        {
            if(!ModelState.IsValid)
            {
                PopulateCuisineTypesSelect();
                return Page();
            }

            if(Restaurant.Id > 0)
            {
                _restaurantData.UpdateRestaurant(Restaurant);
                _restaurantData.Commit();
                return RedirectToPage("./Detail", new { restaurantId = Restaurant.Id });
            }
            else
            {
                _restaurantData.AddRestaurant(Restaurant);
            }

            _restaurantData.Commit();
            TempData["Message"] = "Restaurant Saved!";
            return RedirectToPage("./Detail", new { restaurantId = Restaurant.Id });
        }
    }
}
