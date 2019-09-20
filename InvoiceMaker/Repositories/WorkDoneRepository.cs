using InvoiceMaker.Data;
using InvoiceMaker.Models;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace InvoiceMaker.Repositories
{
    public class WorkDoneRepository
    {
        private Context _context;
        public WorkDoneRepository(Context context)
        {
            _context = context;
        }

        public List<WorkDone> GetWorkDones()
        {
            return _context.WorkDones
                .Include(wd => wd.Client)
                .Include(wd => wd.WorkType)
                .ToList();
        }

        public void Insert(WorkDone workDone)
        {
            _context.WorkDones.Add(workDone);
            _context.SaveChanges();
        }

        public void Update(WorkDone workDone)
        {
            _context.WorkDones.Attach(workDone);
            _context.Entry(workDone).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public WorkDone GetById(int id)
        {
            return _context.WorkDones
                .Include(wd => wd.Client)
                .Include(wd => wd.WorkType)
                .SingleOrDefault(wd => wd.Id == id);
        }
    }
}