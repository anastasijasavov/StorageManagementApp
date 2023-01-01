using StorageManagementApp.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageManagementApp.Contracts.DTOs.Product
{
    public class ProductCreateDto
    {
        [MaxLength(2000)]
        public string? Description { get; set; }
        public string Name { get; set; }
        public float PurchasePrice { get; set; }
        public float RetailPrice { get; set; }
        public int InStock { get; set; }
        public CategoryEnum CategoryId { get; set; }
        public string? ImagePath { get; set; }
        public string Code { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
