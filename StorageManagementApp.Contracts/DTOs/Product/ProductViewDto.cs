using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageManagementApp.Contracts.DTOs.Product
{
    public class ProductViewDto : ResponseTemplateDto<ProductViewDto>
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public float PurchasePrice { get; set; }
        public float RetailPrice { get; set; }
        public int InStock { get; set; }
        public string CategoryName { get; set; }
        public string ImagePath { get; set; }
    }
}
