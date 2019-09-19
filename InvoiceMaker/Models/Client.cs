using System.Collections.Generic;

namespace InvoiceMaker.Models
{
    public class Client
    {
        public Client()
        {
            WorkDone = new List<WorkDone>();
        }

        public Client(int id, string name, bool isActive)
        {
            Id = id;
            Name = name;
            IsActive = isActive;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public List<WorkDone> WorkDone { get; set; }

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