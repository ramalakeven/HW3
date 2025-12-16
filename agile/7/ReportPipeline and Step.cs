namespace ReportProcessing
{
    class ReportPipeline
    {
        public ReportStep Steps { get; set; }

        public void Run(ReportContext context)
        {
            Steps?.Invoke(context);
        }
    }

    delegate void ReportStep(ReportContext context);
}


