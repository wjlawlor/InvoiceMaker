using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceMaker.Models
{
    public class WorkLineItem :ILineItem
    {
        public WorkLineItem(WorkDone workDone)
        {
            _workDone = workDone;
        }

        public decimal Amount
        {
            get
            {
                decimal? total = _workDone.GetTotal();
                if (total.HasValue)
                {
                    return total.Value;
                }
                return 0;
            }
        }
        public string Description
        {
            get
            {
                return _workDone.WorkTypeName;
            }
        }
        public DateTimeOffset When
        {
            get
            {
                return _workDone.StartedOn;
            }
        }

        private WorkDone _workDone;
    }
}