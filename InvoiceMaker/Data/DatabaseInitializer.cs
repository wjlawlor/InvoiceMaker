using InvoiceMaker.Models;
using System;
using System.Data.Entity;

namespace InvoiceMaker.Data
{
    public class DatabaseInitializer : DropCreateDatabaseAlways<Context>
    {
        protected override void Seed(Context context)
        {
            // Clients
            var mo = new Client()
            {
                Name = "Mo",
                IsActive = true
            };
            context.Clients.Add(mo);
            var jason = new Client()
            {
                Name = "Jason",
                IsActive = true
            };
            context.Clients.Add(jason);
            var omer = new Client()
            {
                Name = "Omer",
                IsActive = true
            };
            context.Clients.Add(omer);
            var james = new Client()
            {
                Name = "James",
                IsActive = true
            };
            context.Clients.Add(james);
            var john = new Client()
            {
                Name = "John",
                IsActive = true
            };
            context.Clients.Add(john);

            // WorkTypes
            var wosEmployee = new WorkType()
            {
                Name = "WOS Employee",
                Rate = 500
            };
            context.WorkTypes.Add(wosEmployee);
            var teaching = new WorkType()
            {
                Name = "Teaching",
                Rate = 2000
            };
            context.WorkTypes.Add(teaching);
            var dbCreation = new WorkType()
            {
                Name = "DB Creation",
                Rate = 15.65m
            };
            context.WorkTypes.Add(dbCreation);
            var crime = new WorkType()
            {
                Name = "Crime",
                Rate = 0
            };
            context.WorkTypes.Add(crime);

            // WorkDones
            var workDone1 = new WorkDone()
            {
                Client = james,
                WorkType = teaching,
                StartedOn = new DateTime(2019,8,5)
            };
            context.WorkDones.Add(workDone1);
            var workDone2 = new WorkDone()
            {
                Client = jason,
                WorkType = dbCreation,
                StartedOn = new DateTime(2019, 9, 16),
                EndedOn = new DateTime(2019,9,20)
            };
            context.WorkDones.Add(workDone2);
            var workDone3 = new WorkDone()
            {
                Client = mo,
                WorkType = crime,
                StartedOn = new DateTime(1997, 1, 1)
            };
            context.WorkDones.Add(workDone3);
            var workDone4 = new WorkDone()
            {
                Client = omer,
                WorkType = wosEmployee,
                StartedOn = new DateTime(2019, 8, 5)
            };
            context.WorkDones.Add(workDone4);

            // Invoices
            var invoice1 = new Invoice()
            {
                InvoiceNumber = "9NG5T43B",
                Status = InvoiceStatus.Open,
                Client = jason,
                OpenedOn = new DateTime(2019, 9, 20)
            };
            context.Invoices.Add(invoice1);
            var invoice2 = new Invoice()
            {
                InvoiceNumber = "8YG4L09L",
                Status = InvoiceStatus.Open,
                Client = james,
                OpenedOn = new DateTime(2019, 9, 20)
            };
            context.Invoices.Add(invoice2);

            // Commit Data
            context.SaveChanges();
        }
    }
}