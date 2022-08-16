using OdeToFood.Core;

namespace OdeToFood.Data
{
    public class InMemoryRestaurantData : IRestaurantData
    {
        List<Restaurant> restaurants;

        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>()
            {
                new Restaurant() { Id = 1, Name = "Pizza Paulo", Location = "Caruaru", Cuisine = CuisineType.Italian },
                new Restaurant() { Id = 2, Name = "Cinnamon Club", Location = "Recife", Cuisine = CuisineType.Indian },
                new Restaurant() { Id = 3, Name = "La Costa", Location = "São Paulo", Cuisine = CuisineType.Mexican }
            };
        }

        public IEnumerable<Restaurant> GetRestaurantByName(string name)
        {
            return from r in restaurants
                   where string.IsNullOrEmpty(name) || r.Name.StartsWith(name)
                   orderby r.Name
                   select r;
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return from r in restaurants
                   orderby r.Name
                   select r;
        }

        public Restaurant GetRestaurantById(int id) => restaurants.SingleOrDefault(r => r.Id == id);

        public Restaurant UpdateRestaurant(Restaurant restaurant)
        {
            var res = restaurants.SingleOrDefault(r => r.Id == restaurant.Id);
            if(res != null)
            {
                res.Name = restaurant.Name;
                res.Location = restaurant.Location;
                res.Cuisine = restaurant.Cuisine;
            }
            return res;
        }

        public int Commit()
        {
            return 0;
        }

        public Restaurant AddRestaurant(Restaurant restaurant)
        {
            restaurant.Id = restaurants.Max(r => r.Id) + 1;
            restaurants.Add(restaurant);
            return restaurant;
        }

        public Restaurant DeleteRestaurantById(int id)
        {
            var restaurant = restaurants.FirstOrDefault(r => r.Id == id);

            if (restaurant != null)
            {
                restaurants.Remove(restaurant);
            }

            return restaurant;
        }

        public int GetRestaurantCount()
        {
            return restaurants.Count();
        }
    }
}
