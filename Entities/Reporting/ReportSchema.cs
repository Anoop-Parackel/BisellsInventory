using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Reporting
{
    public class ReportSchema
    {
        public int ReportSchemaId { get; set; }
        public int ReportId { get; set; }
        public int ColumnIndex { get; set; }
        public string ColumnName { get; set; }
        public string DisplayName { get; set; }
        public string Render { get; set; }
        public bool IsSummable { get; set; }
        public ReportSchema(int SchemaId)
        {
            this.ReportSchemaId = SchemaId;
        }
        public ReportSchema()
        {

        }
    }
}
