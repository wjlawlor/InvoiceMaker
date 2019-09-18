using System;
using System.Collections.Generic;

namespace InvoiceMaker.Models
{
    public class Invoice
    {
        public Invoice(string invoiceNumber)
        {
            InvoiceNumber = invoiceNumber;
            LineItems = new List<ILineItem>();
            Status = InvoiceStatus.Open;
        }

        public Invoice(string invoiceNumber, InvoiceStatus status)
          : this(invoiceNumber)
        {
            Status = status;
        }

        public InvoiceStatus Status { get; private set; }
        public string InvoiceNumber { get; private set; }
        public List<ILineItem> LineItems { get; private set; }

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