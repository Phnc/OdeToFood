using Microsoft.EntityFrameworkCore;
using OdeToFood.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdeToFood.Data
{
    public class SqlRestaurantData : IRestaurantData
    {
        private readonly OdeToFoodDbContext _dbContext;

        public SqlRestaurantData(OdeToFoodDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Restaurant AddRestaurant(Restaurant restaurant)
        {
            _dbContext.Add(restaurant);
            return restaurant;
            Commit();
        }

        public int Commit()
        {
            return _dbContext.SaveChanges();
        }

        public Restaurant DeleteRestaurantById(int id)
        {
            var res = GetRestaurantById(id);
            
            if(res != null)
            {
                _dbContext.Remove(res);
            }

            Commit();

            return res;
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return _dbContext.Restaurants;
        }

        public int GetRestaurantCount()
        {
            return _dbContext.Restaurants.Count();
        }

        public Restaurant GetRestaurantById(int id)
        {
            return _dbContext.Restaurants.Find(id);
        }

        public IEnumerable<Restaurant> GetRestaurantByName(string name)
        {
            var query = from r in _dbContext.Restaurants
                        where r.Name.StartsWith(name) || string.IsNullOrEmpty(name)
                        orderby r.Name
                        select r;

            return query;
        }

        public Restaurant UpdateRestaurant(Restaurant restaurant)
        {
            var entity = _dbContext.Restaurants.Attach(restaurant);
            entity.State = EntityState.Modified;
            return restaurant;
        }
    }
}
