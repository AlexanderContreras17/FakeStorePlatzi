using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeStorePlatzi.Dtos
{
    public class CreateDto
    {
        public string title { get; set; }
        public int price { get; set; }
        public string description { get; set; }
        public List<string> images { get; set; } = new List<string>();
        
        
        public int categoryId { get; set; }
    }
}
