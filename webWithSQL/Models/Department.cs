
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace webWithSQL.Models
{
    [Table("Department",Schema="dbo")]
    public class Department
    {   //to clear it as primary key in db
        [Key]
        //to auto genrate the key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //display name is Department ID
        [Display(Name ="Department ID")]
        public int DepartmentId { get; set; }

        [Required]
        [Column(TypeName="varchar(150)")]
        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; }

        [Column(TypeName ="varchar(5)")]
        [Display(Name ="Department Abberviation")]
        public string DepartmentAbbr { get; set; }
    }
}
