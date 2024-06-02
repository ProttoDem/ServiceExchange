using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.SharedKernel;
using ServiceExchange.Core.UserAggregate;

namespace ServiceExchange.Core.ReportAggregate
{
    public class Report : BaseEntity<Guid>, IAggregateRoot
    {
        public int Rate { get; set; } = 0;
        public Task Task { get; set; } = null!;
        public Guid TaskId { get; set; }
        public User User { get; set; } = null!;
        public Guid UserId { get; set; }
        public string Text { get; set; } = "";
        public DateTime Date { get; set; }
    }
}
