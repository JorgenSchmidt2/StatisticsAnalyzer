namespace EStatAnalyser.Web.User.Desktop.Core.Entities.DataEntities
{
    public class DescriptionStatistics
    {
        public int Count { get; set; }
        public double X_min { get; set; }
        public double X_max { get; set; }
        public double Y_min { get; set;}
        public double Y_max { get; set;}
        public double X_med { get; set;}
        public double Y_med { get; set;}
        public double Sx { get; set;}
        public double Sy { get; set;}
        public double Ax { get; set;}
        public double Ay { get; set;}
        public double Ex { get; set;}
        public double Ey { get; set;}
    }
}