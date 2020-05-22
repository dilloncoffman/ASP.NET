using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class EditModel : PageModel
    {
        private readonly IRestaurantData restaurantData;
        private readonly IHtmlHelper htmlHelper;

        // Use this data service to create a property the view can bind to
        [BindProperty]
        public Restaurant Restaurant { get; set; }
        public IEnumerable<SelectListItem> Cuisines { get; set; }

        public EditModel(IRestaurantData restaurantData, IHtmlHelper htmlHelper)
        {
            this.restaurantData = restaurantData;
            this.htmlHelper = htmlHelper;
        }

        public IActionResult OnGet(int? restaurantId) // ? makes restaurantId nullable
        {
            Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();
            if(restaurantId.HasValue)
            {
                Restaurant = restaurantData.GetById(restaurantId.Value);
            }
            else
            {
                Restaurant = new Restaurant();
            }
            
            if (Restaurant == null)
            {
                return RedirectToPage("./NotFound"); // redirect user to NotFound
            }
            return Page(); // returns normal page
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();
                return Page();
            }

            if(Restaurant.Id > 0)
            {
                Restaurant = restaurantData.Update(Restaurant);
            }
            else
            {
                restaurantData.Add(Restaurant);
            }

            restaurantData.Commit();
            TempData["Message"] = "Restaurant saved!";
            return RedirectToPage("./Detail", new { restaurantId = Restaurant.Id }); // use anonymously typed objects to pass route values
        }
    }
}