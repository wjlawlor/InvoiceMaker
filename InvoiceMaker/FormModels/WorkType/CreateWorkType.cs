using System.ComponentModel.DataAnnotations;

namespace InvoiceMaker.FormModels
{
    public class CreateWorkType
    {
        [Required]
        public string Name { get; set; }
        public decimal Rate { get; set; }
    }
}