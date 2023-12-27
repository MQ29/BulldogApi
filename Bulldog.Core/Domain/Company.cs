using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Bulldog.Core.Domain
{
    public class Company
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public Address Address { get; protected set; }
        public string? Description { get; protected set; }
        public string PhoneNumber { get; protected set; }
        public IEnumerable<Employee> Employees { get; protected set; }
        public IEnumerable<Opinion> Opinions { get; protected set; }
        protected Company()
        {

        }
        public Company(string name, Address address, string phoneNumber)
        {
            Name = name;
            Address = address;
            PhoneNumber = phoneNumber;
        }

        public void SetName(string name)
        {
            if (Name == name)
            {
                return;
            }
            Name = name;
        }
        
        public void SetAddress(Address address)
        {
            if (Address == address)
            {
                return;
            }
            Address = address;
        }

        public void SetPhoneNumber(string phoneNumber)
        {
            if (PhoneNumber == phoneNumber)
            {
                return;
            }
            PhoneNumber = phoneNumber;

        }
    }

}
