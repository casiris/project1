using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

namespace project1.Models
{
    public partial class Customer
    {
        //public Customer()
        //{
        //    Orders = new HashSet<Order>();
        //}

        public int CustomerId { get; set; }        // this really doesn't need to be displayed, since the customer doesn't need to know it
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string StateInitials { get; set; }
        public int? Zipcode { get; set; }

        // apparently this is for a one to many relationship (one customer can have many orders)
        // not sure how that relates to inputting data though
        //public virtual ICollection<Order> Orders { get; set; }

        shoppingAppContext db = new shoppingAppContext();

        public List<Customer> GetAllCustomers()
        {
            // can just get a list directly, instead of having to do the query syntax stuff
            // db have various options, and db.Customers has other options. seems very useful
            List<Customer> customers = db.Customers.ToList<Customer>();

            return customers.ToList<Customer>();
        }

        public Customer GetCustomerById(int id)
        {
            Customer customer = db.Customers.Find(id);

            return customer;
        }

        public string AddCustomer(Customer customer)
        {
            db.Customers.Add(customer);
            db.SaveChanges();

            return "Customer added";
        }
    }
}
