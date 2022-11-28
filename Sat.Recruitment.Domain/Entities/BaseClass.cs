namespace Sat.Recruitment.Domain.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class BaseClass
    {
        [Key]
        public virtual int Id { get; set; }
    }
}
