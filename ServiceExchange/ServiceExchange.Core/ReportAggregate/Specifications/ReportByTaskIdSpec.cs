using Ardalis.Specification;
using ServiceExchange.Core.CategoryAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceExchange.Core.ReportAggregate.Specifications
{
    public class ReportByTaskIdSpec : Specification<Report>
    {
        public ReportByTaskIdSpec(Guid taskId)
        {
            Query.Where(report => report.TaskId == taskId);
        }
    }
}
