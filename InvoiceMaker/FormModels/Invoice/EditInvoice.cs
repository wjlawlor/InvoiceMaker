using InvoiceMaker.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace InvoiceMaker.FormModels.Invoice
{
    public class EditInvoice
    {
        public EditInvoice() { }

        public EditInvoice(List<Client> clients)
        {
            SetClientList(clients);
        }

        public int Id { get; set; }

        [Required]
        [Display(Name = "Invoice Number")]
        public string InvoiceNumber { get; set; }

        [Display(Name = "Client Name")]
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public InvoiceStatus Status { get; set; }

        [Display(Name = "Opened On")]
        public DateTime OpenedOn { get; set; }

        public SelectList ClientSelectList { get; set; }
        public void SetClientList(List<Client> clients)
        {
            ClientSelectList = new SelectList(clients, "Id", "Name");
        }
    }
}