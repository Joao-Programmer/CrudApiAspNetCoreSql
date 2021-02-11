using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudApiAspNetCoreSql.Models
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryShortName { get; set; }
        public string CategoryName { get; set; }
        public string CategorySpecialInstructions { get; set; }
        public string CategoryUrl { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CategoryCreateDate { get; set; }

        public IList<MenuItem> CategoryMenuItemsList { get; set; } = new List<MenuItem>();
    }
}
