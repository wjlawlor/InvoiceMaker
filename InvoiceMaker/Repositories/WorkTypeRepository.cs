using InvoiceMaker.Data;
using InvoiceMaker.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace InvoiceMaker.Repositories
{
    public class WorkTypeRepository
    {
        private Context _context;
        public WorkTypeRepository(Context context)
        {
            _context = context;
        }

        public List<WorkType> GetWorkTypes()
        {
            return _context.WorkTypes.ToList();
        }

        public void Insert(WorkType workType)
        {
            _context.WorkTypes.Add(workType);
            _context.SaveChanges();
        }

        public void Update(WorkType workType)
        {
            _context.WorkTypes.Attach(workType);
            _context.Entry(workType).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(WorkType workType)
        {
            var goner = new WorkType() { Id = workType.Id };
            _context.Entry(goner).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public WorkType GetById(int id)
        {
            return _context.WorkTypes.SingleOrDefault(c => c.Id == id);
        }
    }
}
