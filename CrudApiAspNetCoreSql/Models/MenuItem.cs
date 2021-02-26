﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CrudApiAspNetCoreSql.Models
{
    [Table("MenuItems")]
    public class MenuItem
    {
        public int MenuItemID { get; set; }
        public string MenuItemDescription { get; set; }
        public string MenuItemLargePortionName { get; set; }
        public string MenuItemName { get; set; }
        public decimal MenuItemPriceLarge { get; set; }
        public decimal MenuItemPriceSmall { get; set; }
        public string MenuItemShortName { get; set; }
        public string MenuItemSmallPortionName { get; set; }

        [ForeignKey("MenuItemCategory")]
        public int MenuItemCategoryIdFk { get; set; }
        public Category MenuItemCategory { get; set; }
    }
}