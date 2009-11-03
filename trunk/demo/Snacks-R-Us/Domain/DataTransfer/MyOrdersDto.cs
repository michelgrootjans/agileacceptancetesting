using System.Collections.Generic;
using System.Web.Mvc;

namespace Snacks_R_Us.Domain.DataTransfer
{
    public class MyOrdersDto
    {
        public ViewOrdersDto Orders { get; set; }
        public IEnumerable<SelectListItem> Snacks { get; set; }
    }
}