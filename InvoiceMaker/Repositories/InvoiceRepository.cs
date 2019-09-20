using InvoiceMaker.Data;
using InvoiceMaker.Models;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace InvoiceMaker.Repositories
{
    public class InvoiceRepository
    {
        private Context _context;
        public InvoiceRepository(Context context)
        {
            _context = context;
        }

        public List<Invoice> GetInvoices()
        {
            return _context.Invoices
                .Include(i => i.Client)
                .ToList();
        }

        public void Insert(Invoice invoice)
        {
            _context.Invoices.Add(invoice);
            _context.SaveChanges();
        }

        public void Update(Invoice invoice)
        {
            _context.Invoices.Attach(invoice);
            _context.Entry(invoice).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public Invoice GetById(int id)
        {
            return _context.Invoices.SingleOrDefault(i => i.Id == id);
        }
    }
}