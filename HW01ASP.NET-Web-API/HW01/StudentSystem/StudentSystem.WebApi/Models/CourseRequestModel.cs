namespace StudentSystem.WebApi.Models
{
    using StudentSystem.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CourseRequestModel
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<Student> Students { get; set; }

        public IEnumerable<Homework> Homeworks { get; set; }
    }
}