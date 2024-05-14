using System;

namespace ExordiumGamesAssignment.Scripts.Api.Models
{
    [Serializable]
    public class Item
    {
        public int id;
        public string name;
        public string description;
        public string image_url;
        public double price;
        public int retailer_id;
        public int item_category_id;
    }
}
