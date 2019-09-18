using InvoiceMaker.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace InvoiceMaker.FormModels
{
    public class EditWorkDone
    {
        public EditWorkDone() { }

        public EditWorkDone(List<Client> clients, List<WorkType> workTypes)
        {
            SetClientList(clients);
            SetWorkTypeList(workTypes);
        }

        public int Id { get; set; }

        [Display(Name = "Client")]
        public int ClientId { get; set; }

        [Display(Name = "Work Type")]
        public int WorkTypeId { get; set; }

        [Display(Name = "Start Date")]
        public DateTimeOffset StartedOn { get; set; }

        [Display(Name = "End Date")]
        public DateTimeOffset? EndedOn { get; set; }

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