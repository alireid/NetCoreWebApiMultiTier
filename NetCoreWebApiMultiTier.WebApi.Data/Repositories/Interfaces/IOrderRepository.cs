using NetCoreWebApiMultiTier.WebApi.Data.DataTransferObjects;
using NetCoreWebApiMultiTier.WebApi.Data.Models;
using NetCoreWebApiMultiTier.WebApi.ExternalApiServices.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreWebApiMultiTier.WebApi.Data.Repositories.Interfaces
{
    /// <summary>
    /// Interface defining contracts of a data access repository for Customer Order data.
    /// </summary>
    public interface IOrderRepository 
    {
        /// <summary>
        /// Query the database and return the most recent order for the specified customer.
        /// </summary>
        /// <param name="customerRequest">The customer to retrieve order data for.</param>
        /// <returns>The most recent order object or null if one does not exist.</returns>
        Task<List<CustomerOrderItemDto>> GetMostRecentOrderForCustomerAsync(CustomerRequest customerRequest);
    }
}
