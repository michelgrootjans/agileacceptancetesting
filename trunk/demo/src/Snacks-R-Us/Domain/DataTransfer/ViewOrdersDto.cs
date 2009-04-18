using System.Collections;
using System.Collections.Generic;

namespace Snacks_R_Us.Domain.DataTransfer
{
    public class ViewOrdersDto : IEnumerable<ViewOrderDto>
    {
        public string Total     { get; set; }

        public ViewOrdersDto()
        {
            Orders = new List<ViewOrderDto>();
        }

        public List<ViewOrderDto> Orders { get; set; }

        public IEnumerator<ViewOrderDto> GetEnumerator()
        {
            return Orders.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}