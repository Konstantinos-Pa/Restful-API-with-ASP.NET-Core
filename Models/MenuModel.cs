namespace FoodDeliveryapi.Models
{
    public class Menu
    {
        public int menuid { get; }

        public int restaurantid { get; set; }

        public string itemname { get; set; }

        public float price { get; set; }
    }
}
