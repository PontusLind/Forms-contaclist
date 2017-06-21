using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ovning16._1
{
    public class Address
    {

        string type;
        string street;
        string zipCode;
        string city;

        public Address(string type, string street, string zipCode, string city)
        {
            Type = type;
            Street = street;
            ZipCode = zipCode;
            City = city;
        }

        public string Type
        {
            get {return type; }
            set { type = value; }          
        }

        public string Street
        {
            get { return street; }
            set { street = value; }          
        }

        public string ZipCode
        {
            get { return zipCode; }
            set { zipCode = value;  }
        }

        public string City
        {
            get { return city; }
            set { city = value; }          
        }
    }
}