using NetCoreWebApiMultiTier.WebApi.ExternalApiServices.Models;
using NetCoreWebApiMultiTier.WebApi.Validation.Interfaces;

namespace NetCoreWebApiMultiTier.WebApi.Validation.Implementations
{
    /// <summary>
    /// Validation class for customer http requests.
    /// </summary>
    public class CustomerRequestValidator : ICustomerRequestValidator
    {
        /// <summary>
        /// Validate the supplied request object.
        /// </summary>
        /// <param name="customerRequest">Request containing customer details.</param>
        /// <returns>True if request is good, false if its bad.</returns>
        public bool Validate(CustomerRequest customerRequest)
        {
            if (string.IsNullOrEmpty(customerRequest.User) || string.IsNullOrEmpty(customerRequest.CustomerId))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
