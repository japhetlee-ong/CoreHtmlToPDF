namespace CoreHtmlToPDF.Models
{
    public class SampleModel
    {
        public string Name { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public decimal EstimatedBudgetTotal { get; set; }
        public string ApprovedBy { get; set; } = String.Empty;
        public List<SampleData> ItemList { get; set; } = new List<SampleData>();
    }
}
