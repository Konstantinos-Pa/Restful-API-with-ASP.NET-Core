namespace FoodDeliveryapi.Models
{
    public class Rating
    {
        public int ratingid { get; }

        public int userid { get; set; }

        public int restaurantid { get; set; }

        public int rating { get; set; }
    }
}
