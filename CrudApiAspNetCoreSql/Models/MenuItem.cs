using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CrudApiAspNetCoreSql.Models
{
    [Table("MenuItems")]
    public class MenuItem
    {
        [Key]
        public int MenuItemID { get; set; }
        public string MenuItemDescription { get; set; }
        public string MenuItemLargePortionName { get; set; }
        public string MenuItemName { get; set; }
        public decimal MenuItemPriceLarge { get; set; }
        public decimal MenuItemPriceSmall { get; set; }
        public string MenuItemShortName { get; set; }
        public string MenuItemSmallPortionName { get; set; }
        public string MenuItemImagePath { get; set; }

        [ForeignKey("MenuItemCategory")]
        public int MenuItemCategoryIdFk { get; set; }
        public Category MenuItemCategory { get; set; }

        [NotMapped]
        public IFormFile MenuItemImageFile { get; set; }
    }
}