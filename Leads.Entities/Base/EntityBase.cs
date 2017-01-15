namespace Leads.Entities.Base
{
    using System.ComponentModel.DataAnnotations;

    public abstract class EntityBase
    {
        [Key]
        public int Id { get; set; }

        public bool IsTransient
        {
            get { return Id.Equals(default(int)); }
        }        
    }
}
