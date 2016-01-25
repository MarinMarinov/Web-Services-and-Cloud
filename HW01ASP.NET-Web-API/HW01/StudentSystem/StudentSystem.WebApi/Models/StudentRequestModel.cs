namespace StudentSystem.WebApi.Models
{
    using System.ComponentModel.DataAnnotations;

    public class StudentRequestModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string LastName { get; set; }

        public int Level { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }
    }
}