using System;
using System.Collections.Generic;

namespace EFPractice.Domain
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public virtual ContactDetail ContactDetail { get; set; }

        private ICollection<Order> _orders;
        public virtual ICollection<Order> Orders
        {
            get { return _orders; }
            set
            {
                _orders = value; 
            }
        }

        private ICollection<Address> _addresses;
        public virtual ICollection<Address> Addresses
        {
            get { return _addresses; }
            set { _addresses = value; }
        }

        public Customer()
        {
            _orders = new List<Order>();
            _addresses = new List<Address>();
        }
    }
}