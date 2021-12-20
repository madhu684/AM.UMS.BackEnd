using AM.UMS.BackEnd.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AM.UMS.BackEnd.Services
{
    public interface ICustomerService
    {
        IEnumerable<Customer> Get();
        void Post(Customer customer);
        void Put(int customerID, Customer customer);
        void Delete(int customerID);
    }
}
