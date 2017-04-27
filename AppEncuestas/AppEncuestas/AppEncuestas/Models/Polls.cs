using System;

namespace AppEncuestas.Models
{
    public class Poll
    {
        public int ContactId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }


        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }

        public string FullName { get { return string.Format("{0} {1}", FirstName, LastName); }  }


        public string BathroomsNumber { get; set; }
        public string Independentworkers { get; set; }
        public DateTime PollDate { get; set; }
        public string NumberPeople { get; set; }

        public bool RelativesAbroad { get; set; }

        public override int GetHashCode()
        {
            return ContactId;  
        }
    }
}
