using System.ComponentModel.DataAnnotations;

namespace EmployeeWebClient.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public string Office { get; set; }

        public int Salary { get; set; }
    }
}
