using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreWebApiMultiTier.WebApi.Services.DataTransferObjects
{
    /// <summary>
    /// Data transfer object defining complete CustomerOrderDto. 
    /// </summary>
    public class CustomerOrderDto
    {
        public CustomerOrderDto()
        {
            Customer = new CustomerDto();
        }

        public CustomerDto Customer { get; set; }
        public OrderDto Order { get; set; }
    }
}
