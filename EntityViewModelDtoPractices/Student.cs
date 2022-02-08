using System.ComponentModel.DataAnnotations.Schema;

namespace EntityViewModelDtoPractices
{
    public class Student
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public string DateOfBirth { get; set; }
        public string Photo { get; set; }
        public decimal Height { get; set; }
        public decimal Weight { get; set; }

        public Grade Grade { get; set; } 
       
    }
}
