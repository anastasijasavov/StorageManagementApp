using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageManagementApp.Contracts.DTOs.Product
{
    public class ProductQuery
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
        public int? CategoryId{ get; set; }
        public int PageSize { get; set; } = 5;
    }
}
