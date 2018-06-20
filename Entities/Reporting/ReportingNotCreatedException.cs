using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Reporting
{
    public class ReportingNotCreatedException:NotImplementedException
    {
        public ReportingNotCreatedException(int reportId)
        {
            this.ReportId = ReportId;
        }
        public int ReportId { get; set; }
    }
}
