namespace EStatAnalyser.Web.User.Desktop.Core.Entities.DataEntities
{
    public class RegressionData
    {
        public List<RegressionValue>? RegressionValues { get; set; }
        public List<RegressionValue>? TrustMinValues { get; set; }
        public List<RegressionValue>? TrustMaxValues { get; set; }

        public double a0 { get; set; }
        public double a1 { get; set; }
    }
}
