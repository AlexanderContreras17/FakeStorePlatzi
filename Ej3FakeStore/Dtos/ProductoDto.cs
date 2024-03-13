
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeStorePlatzi.Dtos
{
    public class ProductDTO
    {
        public int id { get; set; }
        public string title { get; set; }
        public int price { get; set; }
        public string description { get; set; }
        public List<string> images{ get; set; }= new List<string>();
        public DateTime creationAt { get; set; } 
        public DateTime updatedAt { get; set; }
        public CategoryDTO category { get; set; }=new CategoryDTO();
        public int categoryId { get; set; }
    }

    public class CategoryDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public DateTime creationAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}
