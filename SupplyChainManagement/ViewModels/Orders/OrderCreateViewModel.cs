using Microsoft.AspNetCore.Mvc.Rendering;
using SupplyChainManagement.Models;
using System.ComponentModel.DataAnnotations;

namespace SupplyChainManagement.ViewModels.Orders
{
    public class OrderCreateViewModel
    {
        public IEnumerable<SelectListItem> CustomersList { get; set; }
        public IEnumerable<SelectListItem> ProductsList { get; set; }
    }
}
