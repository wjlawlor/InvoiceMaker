using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceMaker.Models
{
    public class Client
    {
        public Client() { }

        public Client(int id, string name, bool isActive)
        {
            Id = id;
            Name = name;
            IsActive = isActive;
        }

        public int Id { get; set; }
        [Required, Column("ClientName"), MaxLength(255)]
        public string Name { get; set; }
        [Column("IsActivated")]
        public bool IsActive { get; set; }

        public void Activate()
        {
            IsActive = true;
        }
        public void Deactivate()
        {
            IsActive = false;
        }
    }
}