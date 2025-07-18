using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPPR_Perfume.Models
{
    internal class AppData
    {
        public static DataTable CurrentDataTable { get; set; }
        public static double[,] ListMatrix { get; set; }
        public static double[] LastResult { get; set; }
    }
}
