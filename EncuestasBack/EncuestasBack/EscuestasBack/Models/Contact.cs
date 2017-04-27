using System;
using System.ComponentModel.DataAnnotations;

namespace EncuestasBack.Models
{
    public class Contact
    {
        [Key]
        public int ContactId { get; set; }

        [Required(ErrorMessage = "You must enter a {0}")]
        [StringLength(30, ErrorMessage = "The field {0} can contain maximun {1} and minimum {2} characters", MinimumLength = 1)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "You must enter a {0}")]
        [StringLength(30, ErrorMessage = "The field {0} can contain maximun {1} and minimum {2} characters", MinimumLength = 1)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        [StringLength(100, ErrorMessage = "The field {0} can contain maximun {1} and minimum {2} characters", MinimumLength = 7)]
        public string EmailAddress { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone number")]
        [StringLength(20, ErrorMessage = "The field {0} can contain maximun {1} and minimum {2} characters", MinimumLength = 7)]
        public string PhoneNumber { get; set; }

        
        [Display(Name = "People number")]
        [StringLength(20, ErrorMessage = "The field {0} can contain maximun {1} and minimum {2} characters", MinimumLength = 1)]
        public string NumberPeople { get; set; }


        [DataType(DataType.Date)]
        [Display(Name = "Date poll")]
        [StringLength(20, ErrorMessage = "The field {0} can contain maximun {1} and minimum {2} characters", MinimumLength = 8)]
        public string PollDate { get; set; }

        
        [Display(Name = "Relatives Abroad")]
        public bool RelativesAbroad { get; set; }


        [Display(Name = "Independent workers")]
        [StringLength(20, ErrorMessage = "The field {0} can contain maximun {1} and minimum {2} characters", MinimumLength = 1)]
        public string IndependentWorkers { get; set; }


        
        [Display(Name = "Number bathrooms")]
        [StringLength(20, ErrorMessage = "The field {0} can contain maximun {1} and minimum {2} characters", MinimumLength = 1)]
        public string BathroomsNumber{ get; set; }

        

    }
}