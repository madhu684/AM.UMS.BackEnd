using AM.UMS.BackEnd.Data;
using AM.UMS.BackEnd.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AM.UMS.BackEnd.DAL
{
    public class CustomerService : ICustomerService
    {
        private readonly ILogger<AuthenticationService> _logger;
        private readonly UserDbContext _context;
        private readonly AppSettings _appSettings;

        public CustomerService(ILogger<AuthenticationService> logger, UserDbContext context, IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _context = context;
            _appSettings = appSettings.Value;
        }
        public IEnumerable<Customer> Get()
        {
            try
            {
                var customer = _context.Customers
                                .Include(o => o.Orders).ThenInclude(x => x.ItemOrders)
                                .ToList();

                //return null if user is not found
                if (customer == null) { return null; } else { return customer; }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                throw ex;
            }
        }

        public void Post(Customer customer)
        {
            try
            {
                //Insert the customer
                _context.Customers.Add(customer);

                //Commit the transaction
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                throw ex;
            }
        }

        public void Put(int customerID, Customer customer)
        {
            try
            {
                var customerDtl = _context.Customers.Where(x => x.CustomerID == customerID);

                if (customerDtl != null)
                {
                    //Update the customer
                    _context.Customers.Update(customer);

                    //Commit the transaction
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                throw ex;
            }
        }
        public void Delete(int customerID)
        {
            try
            {
                var customer = _context.Customers.Where(x => x.CustomerID == customerID).FirstOrDefault();

                if (customer != null)
                {
                    //Delete the customer
                    _context.Customers.Remove(customer);

                    //Commit the transaction
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                throw ex;
            }
        }
    }
}
