using InvoiceMaker.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InvoiceMaker.FormModels.Invoice
{
    public class AddWorkDoneInvoice
    {
        public AddWorkDoneInvoice() { }

        public AddWorkDoneInvoice(List<WorkDone> workDones)
        {
            SetClientList(workDones);
        }

        public int Id { get; set; }

        [Required]
        [Display(Name = "Invoice Number")]
        public string InvoiceNumber { get; set; }

        [Display(Name = "Client Name")]
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public InvoiceStatus Status { get; set; }
        public DateTime OpenedOn { get; set; }

        public int WorkDoneId { get; set; }

        public SelectList WorkDoneSelectList { get; set; }
        public void SetClientList(List<WorkDone> workDones)
        {
            WorkDoneSelectList = new SelectList(workDones, "Id", "Id");
        }
    }
}