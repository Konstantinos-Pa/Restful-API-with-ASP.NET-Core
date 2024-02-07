namespace FoodDeliveryapi.Models
{
    public class Address
    {
        public int addressid { get; }

        public int userid { get; set; }

        public string state { get; set; }

        public string city { get; set; }

        public string street { get; set; }

        public int pincode { get; set; }
    }
}
