using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceMaker.Models
{
    public class WorkDone
    {
        public WorkDone() { }

        public WorkDone(int id, Client client, WorkType workType)
        {
            Id = id;
            Client = client;
            WorkType = workType;
            StartedOn = DateTimeOffset.Now;
        }
        public WorkDone(int id, Client client, WorkType workType, DateTimeOffset startedOn) 
            : this (id, client, workType)
        {
            StartedOn = startedOn;
        }
        public WorkDone(int id, Client client, WorkType workType, DateTimeOffset startedOn, DateTimeOffset? endedOn)
            : this (id, client, workType, startedOn)
        {
            EndedOn = endedOn;
        }

        public int Id { get; set; }
        public decimal Rate { get { return WorkType.Rate; } }
        public int ClientId { get { return Client.Id; } }
        public string ClientName { get { return Client.Name; } }
        public int WorkTypeId { get { return WorkType.Id; } }
        public string WorkTypeName { get { return WorkType.Name; } }
        public DateTimeOffset StartedOn { get; set; }
        public DateTimeOffset? EndedOn { get; set; }

        //public int ClientId { get; set; }
        public Client Client { get; set; }
        //public int WorkTypeId { get; set; }
        public WorkType WorkType { get; set; }
        public int? InvoiceId { get; set; }
        public Invoice Invoice { get; set; }


        public void Finished(DateTimeOffset endedOn)
        {
            if (!EndedOn.HasValue)
            {
                EndedOn = DateTimeOffset.Now;
            }
        }
        public decimal? GetTotal()
        {
            if (!EndedOn.HasValue)
            {
                return null;
            }
            return WorkType.Rate * (decimal)(EndedOn.Value - StartedOn).TotalHours;
        }
    }
}
