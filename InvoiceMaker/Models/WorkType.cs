using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceMaker.Models
{
    public class WorkType
    {
        public WorkType() { }
        
        public WorkType(int id, string name, decimal rate)
        {
            Id = id;
            Name = name;
            Rate = rate;
        }

        public int Id { get; set; }
        public string Name { get; private set; }
        public decimal Rate { get; private set; }
    }
}