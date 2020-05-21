using System.Collections.Generic;
using OdeToFood.Core;
using System.Linq;

namespace OdeToFood.Data
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetRestaurantsByName(string name);
    }

    public class InMemoryRestaurantData : IRestaurantData
    {
        readonly List<Restaurant> restaurants;
        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>()
            {
                new Restaurant {Id = 1, Name = "Dillon's Pizza", Location = "Pittsburgh", Cuisine = CuisineType.Italian},
                new Restaurant {Id = 2, Name = "Suzy's", Location = "Mt. Pleasant", Cuisine = CuisineType.None},
                new Restaurant {Id = 3, Name = "Emporio", Location = "Pittsburgh", Cuisine = CuisineType.Italian}
            };
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name = null)
        {
            return from r in restaurants // using LINQ to filter data
                   where string.IsNullOrEmpty(name) || r.Name.StartsWith(name)
                   orderby r.Name
                   select r;
        }
    }
}
