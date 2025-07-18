using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPPR_Perfume.Models
{
    internal class ReportDataDto
    {
        public string ReportDate { get; set; }
        public int PerfumeCount { get; set; }
        public List<string> Headers { get; set; }
        public List<List<object>> Rows { get; set; }
        public string SummaryText { get; set; }
    }
}
