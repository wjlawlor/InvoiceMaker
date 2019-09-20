using System;
using System.Collections.Generic;

namespace InvoiceMaker.Models
{
    public class Invoice
    {
        public Invoice() { }

        public Invoice(int id, string invoiceNumber, Client client)
        {
            Id = id;
            InvoiceNumber = invoiceNumber;
            Client = client;
            OpenedOn = DateTime.Now;
            LineItems = new List<ILineItem>();
            Status = InvoiceStatus.Open;
        }

        public Invoice(int id, string invoiceNumber, Client client, InvoiceStatus status)
          : this(id, invoiceNumber, client)
        {
            Status = status;
        }

        public Invoice(int id, string invoiceNumber, Client client, InvoiceStatus status, DateTime openedOn)
            : this(id, invoiceNumber, client, status)
        {
            OpenedOn = openedOn;
        }

        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public InvoiceStatus Status { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public DateTime OpenedOn { get; set; }

        public List<ILineItem> LineItems { get; set; }
        public List<WorkDone> WorkDones { get; set; }
        //public List<FeeLineItem> FeeLineItems { get; set; }

        public void AddWorkLineItem(WorkDone workDone)
        {
            LineItems.Add(new WorkLineItem(workDone));
        }
        public void AddFeeLineItem(decimal amount, string description, DateTimeOffset when)
        {
            LineItems.Add(new FeeLineItem(amount, description, when));
        }
        public void FinalizeInvoice()
        {
            if (Status == InvoiceStatus.Open)
            {
                Status = InvoiceStatus.Finalized;
            }
        }
        public void CloseInvoice()
        {
            if (Status == InvoiceStatus.Finalized)
            {
                Status = InvoiceStatus.Closed;
            }
        }
    }
}