namespace Employee_CRUD.Models
{
    public class UpdateEmployeeViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateEmployment { get; set; }
        public DateTime? DateDismissal { get; set; }
    }
}
