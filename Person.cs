using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ovning16._1
{
    public class Person
    {        
        public List <Address> myAddress;
        public List<PhonNr> myPhonNr;

        private string firsName;
        private string lastName;
        private string socialSecurityNumber;


        public Person(string firsName, string lastName, string socialSecurityNumber)
        {
            FirstName = firsName;
            LastName = lastName;
            SocialSecurityNumber = socialSecurityNumber;
            this.myPhonNr = new List<PhonNr>();
            this.myAddress = new List<Address>();
        }

        public string FirstName
        {
            get { return firsName; }
            set { firsName = value; }
        }

        public string LastName
        {
            get {return lastName; }
            set {lastName = value; }
        }

        public string SocialSecurityNumber
        {
            get { return socialSecurityNumber; }
            set { socialSecurityNumber = value; }
        }

        public ControlContats ControlContats
        {
            get => default(ControlContats);
            set
            {
            }
        }
    }
}