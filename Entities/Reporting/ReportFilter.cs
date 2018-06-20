using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Reporting
{
    public class ReportFilter
    {
        public int ReportFilterId { get; set; }
        public int ReportId { get; set; }
        public string FilterName { get; set; }
        public string Value1 { get; set; }
        public string Value2 { get; set; }
        public string Label { get; set; }
        public int ColumnIndex { get; set; }
        public string ColumnName { get; set; }
        public string ColumnDataType { get; set; }
        public string DataTextField { get; set; }
        public string DataValueField { get; set; }
        public int DataTextFieldId { get; set; }
        public int DataValueFieldId { get; set; }
        /// <summary>
        /// Range	0
        /// Exact Value	1
        /// Contains Value	2
        /// </summary>
        public int Type { get; set; }
        public string TypeAsName
        {
            get
            {
                switch (this.Type)
                {
                    case 0:
                        return "Range";
                    case 1:
                        return "Exact Value";
                    case 2:
                        return "Contains Value";
                    default:
                        return string.Empty;
                }
            }
            }
     
        /// <summary>
        /// List	0
        /// Typable	1
        /// Date	2
        /// Time	3
        /// Date&Time	4
        /// </summary>
        public int ControlType { get; set; }
        public string RenderAs
        {
            get
            {
                switch (this.ControlType)
                {
                    case 0:
                        return "List";
                    case 1:
                        return "Typable";
                    case 2:
                        return "Date";
                    default:
                        return string.Empty;
                }
            }
        }

        public ReportFilter(int FilterId,string FilterName)
        {
            this.ReportFilterId = FilterId;
        }
        public ReportFilter()
        {

        }
    }
}
