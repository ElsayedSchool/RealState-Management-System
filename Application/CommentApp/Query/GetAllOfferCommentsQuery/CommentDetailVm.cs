using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CommentApp.Query.GetAllCommentsQuery
{
    public class CommentDetailVm
    {
        public int Id { get; set; }
        public string Detail { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeId { get; set; }
        public int OfferId { get; set; }
        public DateTime Date { get; set; }
    }
}
