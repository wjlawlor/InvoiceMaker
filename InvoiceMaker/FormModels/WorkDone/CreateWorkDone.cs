using InvoiceMaker.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace InvoiceMaker.FormModels
{
    public class CreateWorkDone
    {
        public CreateWorkDone() { }

        public CreateWorkDone(List<Client> clients, List<WorkType> workTypes)
        {
            SetClientList(clients);
            SetWorkTypeList(workTypes);
        }

        public int Id { get; set; }
        public Client Client { get; set; }
        public WorkType WorkType { get; set; }

        [Display(Name = "Client")]
        public int ClientId { get; set; }

        [Display(Name = "Work Type")]
        public int WorkTypeId { get; set; }
        public DateTimeOffset StartedOn { get; set; }

        public SelectList ClientSelectList { get; private set; }
        public void SetClientList(List<Client> clients)
        {
            ClientSelectList = new SelectList(clients, "Id", "Name");
        }

        public SelectList WorkTypeSelectList { get; private set; }
        public void SetWorkTypeList(List<WorkType> workTypes)
        {
            WorkTypeSelectList = new SelectList(workTypes, "Id", "Name");
        }
    }
}