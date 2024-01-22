using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Web.Models
{
    public class CustomerService
    {
        private List<Customer> Customers
        {
            get
            {
                List<Customer> customers;
                if (HttpContext.Current.Session["Customers"] != null)
                {
                    customers = (List<Customer>)HttpContext.Current.Session["Customers"];
                }
                else
                {
                    customers = new List<Customer>();
                    InitCustomerData(customers);
                    HttpContext.Current.Session["Customers"] = customers;
                }
                return customers;
            }
        }

        public Customer GetByID(int customerID)
        {
            return this.Customers.AsQueryable().First(customer => customer.ID == customerID);
        }

        public IQueryable<Customer> GetQueryable()
        {
            return this.Customers.AsQueryable();
        }

        public void Add(Customer customer)
        {
            this.Customers.Add(customer);
        }

        public void Update(Customer customer)
        {

        }

        public void Delete(int customerID)
        {
            this.Customers.RemoveAll(customer => customer.ID == customerID);
        }

        private void InitCustomerData(List<Customer> customers)
        {
            customers.Add(new Customer
            {
                ID = 1,
                FirstName = "John",
                LastName = "Doe",
                Phone = "1111111111",
                Email = "johndoe@gmail.com",
                OrdersPlaced = 5,
                DateOfLastOrder = DateTime.Parse("5/3/2007")
            });

            customers.Add(new Customer
            {
                ID = 2,
                FirstName = "Jane",
                LastName = "Doe",
                Phone = "2222222222",
                Email = "janedoe@gmail.com",
                OrdersPlaced = 3,
                DateOfLastOrder = DateTime.Parse("4/5/2008")
            });

            customers.Add(new Customer
            {
                ID = 3,
                FirstName = "John",
                LastName = "Smith",
                Phone = "3333333333",
                Email = "johnsmith@yahoo.com",
                OrdersPlaced = 25,
                DateOfLastOrder = DateTime.Parse("4/5/2000")
            });


            customers.Add(new Customer
            {
                ID = 4,
                FirstName = "Eddie",
                LastName = "Murphy",
                Phone = "4444444444",
                Email = "eddiem@yahoo.com",
                OrdersPlaced = 1,
                DateOfLastOrder = DateTime.Parse("4/5/2003")
            });


            customers.Add(new Customer
            {
                ID = 5,
                FirstName = "Ziggie",
                LastName = "Ziggler",
                Phone = null,
                Email = "ziggie@hotmail.com",
                OrdersPlaced = 0,
                DateOfLastOrder = null
            });


            customers.Add(new Customer
            {
                ID = 6,
                FirstName = "Michael",
                LastName = "J",
                Phone = "666666666",
                Email = "ziggie@hotmail.com",
                OrdersPlaced = 5,
                DateOfLastOrder = DateTime.Parse("12/3/2007")
            });

        }

    }
}
