namespace Sat.Recruitment.Domain.Entities
{
    using Sat.Recruitment.Domain.Enums;
    
    public class User : BaseClass
    {
        public string Name { get; set; }
        
        public string Email { get; set; }
        
        public string Address { get; set; }
        
        public string Phone { get; set; }
        
        public UserType UserType { get; set; }
        
        public decimal Money { get; set; }
    }
}
