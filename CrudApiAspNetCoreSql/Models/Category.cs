using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudApiAspNetCoreSql.Models
{
    public class Category
    {
        public int id { get; set; }
        public string short_name { get; set; }
        public string name { get; set; }
        public string special_instructions { get; set; }
        public string url { get; set; }
        public IList<MenuItem> menu_items { get; set; } = new List<MenuItem>();
    }
}
