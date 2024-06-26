﻿using E_commercial_Web_RESTAPI.Data;
using E_commercial_Web_RESTAPI.Helpers;
using E_commercial_Web_RESTAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace E_commercial_Web_RESTAPI.Repositories.Repository_Impl
{
    public class CustomerRepository_Impl :Repository_Impl<Cart>,ICustomerRepository

    {
        private readonly ApplicationDBcontext _context;

        public CustomerRepository_Impl(ApplicationDBcontext context) : base(context) 
        {
            _context = context;


        }

        public async Task<bool> DoesCustomerExists(Expression<Func<Customer, bool>> predicate)
        {
            return await _context.customers.AnyAsync(predicate);    
        }

       
        public async Task<Customer> CreateCustomer(Customer customer)
        {
            if(customer is null) throw new ArgumentNullException(nameof(customer)); 
            await _context.customers.AddAsync(customer);
            await _context.SaveChangesAsync();
            return customer;

        }

        public async Task<Customer?> FindCustomerById(long id)
        {
           return await _context.customers.Include(s => s.payments).ThenInclude(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
           // return await _context.customers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Customer>> GetAllCustomers(CustomerQueryObject query)
        {
            var customers =   _context.customers.Include(s => s.payments).ThenInclude(x => x.User)
                .Include(x => x.orders).AsQueryable();
            //var customers = _context.customers.AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                customers = customers.Where(s => s.Name.Equals(query.Name));

            }
            if (!string.IsNullOrWhiteSpace(query.PhoneNumber))
            {
                customers = customers.Where(s => s.PhoneNumber.Equals(query.PhoneNumber));
            }
            return await customers.ToListAsync();

        }

        public async Task<List<Customer>> GetCustomers() // for unit testing purposes
        {
            return await _context.customers.ToListAsync();
        }

        
    }
}
