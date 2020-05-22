using System.Collections.Generic;
using OdeToFood.Core;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace OdeToFood.Data
{
    public class SqlRestaurantData : IRestaurantData
    {
        private readonly OdeToFoodDbContext db;

        public SqlRestaurantData(OdeToFoodDbContext db)
        {
            this.db = db;
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            db.Restaurants.Add(newRestaurant);
            return newRestaurant;
        }

        public int Commit()
        {
            return db.SaveChanges();
        }

        public Restaurant Delete(int id)
        {
            var restaurantToDelete = GetById(id);
            if (restaurantToDelete != null)
            {
                db.Restaurants.Remove(restaurantToDelete);
            }
            return restaurantToDelete;
        }

        public Restaurant GetById(int id)
        {
            return db.Restaurants.Find(id);
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name)
        {
            var query = from r in db.Restaurants // LINQ query
                        where r.Name.StartsWith(name) || string.IsNullOrEmpty(name)
                        orderby r.Name
                        select r;
            return query;
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            // Attach entity to EF, tell EF entity has been modified, EF assumes everything about this restaurant has changed, so when Commit() is ran, it will run UPDATE queries to update this
            var entity = db.Restaurants.Attach(updatedRestaurant); // Attach() tells EF "i'm about to give you an object, that represents info already in the DB, but I want you to start tracking changes about this object, so I'm not adding a new restaurant or trying to retrieve it, I already have it" 
            entity.State = EntityState.Modified; // state of this entity is modified
            return updatedRestaurant;
        }
    }
}
