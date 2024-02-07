namespace FoodDeliveryapi.Models
{
    public class Order
    {
        public int orderid { get; }

        public int userid { get; set; }

        public int restaurantid { get; set; }

        public float ordertotal { get; set; }

        public string deliverystatus { get; set; }
    }
}
