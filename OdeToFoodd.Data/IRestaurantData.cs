using System.Collections.Generic;
using OdeToFood.Core;
using System.Linq;

namespace OdeToFood.Data
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetRestaurantsByName(string name);
        Restaurant GetById(int id);
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

        public Restaurant GetById(int id)
        {
            return restaurants.SingleOrDefault(r => r.Id == id); // using LINQ to return the restaurant matching the id passed in or the default value (null)
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
