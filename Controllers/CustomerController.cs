﻿using E_commercial_Web_RESTAPI.Data;
using E_commercial_Web_RESTAPI.DTOS.Customers;
using E_commercial_Web_RESTAPI.Mapper.CustomerMappper;
using E_commercial_Web_RESTAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_commercial_Web_RESTAPI.Controllers
{
    [ApiController]
    [Route("api/customer")]
    public class CustomerController : ControllerBase
    {
        private readonly ApplicationDBcontext _context;
        private readonly ICustomerRepository _customer_repository;

        public CustomerController(ApplicationDBcontext context, ICustomerRepository customer_repository)
        {
            _context = context;
            _customer_repository = customer_repository;
        }


        [HttpPost]

        public async Task<IActionResult> CreateNewCustomer(CustomerInfoRequestDTO customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var customerModel = await _customer_repository.CreateCustomer(customer.ToCustomerFromRequestDTO());
            return CreatedAtAction(nameof(GetById), new { id = customerModel.Id }, customerModel.ToCustomerDTO());

        }

        [HttpGet("{id:long}")]

        public async Task<IActionResult> GetById([FromRoute]long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
                var customerModel = await _customer_repository.FindCustomerById(id);
            if(customerModel == null)
            {
                return NotFound();
            }
            return Ok(customerModel.ToCustomerDTO());
        }

        [HttpGet]

        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customers =  await _customer_repository.GetAllCustomers();
            var customerModel = customers.Select(s => s.ToCustomerDTO()).ToList();
            return Ok(customerModel);

        }

    }
}
