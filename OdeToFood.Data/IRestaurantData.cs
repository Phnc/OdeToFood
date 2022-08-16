using OdeToFood.Core;

namespace OdeToFood.Data
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetAll();

        IEnumerable<Restaurant> GetRestaurantByName(string name);

        Restaurant GetRestaurantById(int id);

        Restaurant UpdateRestaurant(Restaurant restaurant);

        Restaurant AddRestaurant(Restaurant restaurant);

        Restaurant DeleteRestaurantById(int id);

        int GetRestaurantCount();

        int Commit();
    }
}
