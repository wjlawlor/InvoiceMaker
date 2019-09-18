using System;

namespace InvoiceMaker.Models
{
    public class WorkDone
    {
        public WorkDone(int id, Client client, WorkType workType)
        {
            Id = id;
            _client = client;
            _type = workType;
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
        public decimal Rate { get { return _type.Rate; } }
        public int ClientId { get { return _client.Id; } }
        public string ClientName { get { return _client.Name; } }
        public int WorkTypeId { get { return _type.Id; } }
        public string WorkTypeName { get { return _type.Name; } }
        public DateTimeOffset StartedOn { get; private set; }
        public DateTimeOffset? EndedOn { get; private set; }

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
            return _type.Rate * (decimal)(EndedOn.Value - StartedOn).TotalHours;
        }

        private Client _client;
        private WorkType _type;
    }
}