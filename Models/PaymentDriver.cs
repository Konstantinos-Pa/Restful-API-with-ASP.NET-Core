namespace FoodDeliveryapi.Models
{
    public class Payment
    {
        public int paymentid { get; }

        public int orderid { get; set; }

        public string paymentmethod { get; set; }

        public float amount { get; set; }

        public string status { get; set; }
    }
}
