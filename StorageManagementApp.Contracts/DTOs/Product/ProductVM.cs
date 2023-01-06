using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageManagementApp.Contracts.DTOs.Product
{
    public class ProductVM
    {
        public List<ProductViewDto> Products { get; set; }
        public ProductQuery Query { get; set; }

    }
}
