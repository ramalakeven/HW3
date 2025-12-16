using System;

namespace ReportProcessing
{
    class ReportContext
    {
        public int RowsCount { get; set; }
        public bool HasSummary { get; set; }
        public bool IsFormatted { get; set; }
    }
}

