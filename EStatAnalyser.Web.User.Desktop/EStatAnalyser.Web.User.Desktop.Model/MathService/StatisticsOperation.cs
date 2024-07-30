using EStatAnalyser.Web.User.Desktop.Core.Configuration;
using EStatAnalyser.Web.User.Desktop.Core.Entities.DataEntities;
using EStatAnalyser.Web.User.Desktop.Core.Entities.GraphicsShellEntities;

namespace EStatAnalyser.Web.User.Desktop.Model.MathService
{
    public class StatisticsOperation
    {
        public static List<SimpleData> GetZFilter(List<SimpleData> data, DescriptionStatistics descStat, double z)
        {
            var Result = new List<SimpleData>();

            foreach (var item in data)
            {
                var zx = (item.X - descStat.X_med) / descStat.Sx;
                var zy = (item.Y - descStat.Y_med) / descStat.Sy; 

                if ( (-1 * z <= zx && zx <= z) && (-1 * z <= zy && zy <= z))
                {
                    Result.Add(new SimpleData { X = item.X, Y = item.Y});
                }
            }

            return Result;
        } 

        public static DescriptionStatistics GetDescriptionStatistics(List<SimpleData> data)
        {
            DescriptionStatistics Result = new DescriptionStatistics();

            Result.Count = data.Count();

            Result.X_min = data.Min(x => x.X);
            Result.X_max = data.Max(x => x.X);
            Result.Y_min = data.Min(x => x.Y);
            Result.Y_max = data.Max(x => x.Y);

            Result.X_med = data.Average(x => x.X);
            Result.Y_med = data.Average(y => y.Y);

            double disp_sum_x = 0;
            double disp_sum_y = 0;
            double assym_sum_x = 0;
            double assym_sum_y = 0;
            double excess_sum_x = 0;
            double excess_sum_y = 0;

            foreach (var item in data)
            {
                disp_sum_x += Math.Pow(item.X - Result.X_med, 2);
                assym_sum_x += Math.Pow(item.X - Result.X_med, 3);
                excess_sum_x += Math.Pow(item.X - Result.X_med, 4);

                disp_sum_y += Math.Pow(item.Y - Result.Y_med, 2);
                assym_sum_y += Math.Pow(item.Y - Result.Y_med, 3);
                excess_sum_y += Math.Pow(item.Y - Result.Y_med, 4);
            }

            Result.Sx = Math.Sqrt( 
                    disp_sum_x / (Result.Count - 1)
                );
            Result.Sy = Math.Sqrt(
                    disp_sum_y / (Result.Count - 1)
                );

            Result.Ax = assym_sum_x / (Math.Pow(Result.Sx, 3) * Result.Count);
            Result.Ay = assym_sum_y / (Math.Pow(Result.Sy, 3) * Result.Count);

            Result.Ex = (excess_sum_x / (Math.Pow(Result.Sx, 4) * Result.Count)) - 3;
            Result.Ey = (excess_sum_y / (Math.Pow(Result.Sy, 4) * Result.Count)) - 3;

            return Result;
        }

        public static RegressionData GetRegressionData (List<Sphere> data, DescriptionStatistics desc_stat, double threshold)
        {
            RegressionData Result = new RegressionData();
            Result.RegressionValues = new List<RegressionValue>();
            Result.TrustMinValues = new List<RegressionValue>();
            Result.TrustMaxValues = new List<RegressionValue>();

            var RegressionValues = new List<RegressionValue>();
            var TrustMinValues = new List<RegressionValue>();
            var TrustMaxValues = new List<RegressionValue>();

            double r_sum = 0;
            foreach (var item in data)
            {
                r_sum += (item.X - desc_stat.X_med) * (item.Y - desc_stat.Y_med);
            }
            

            var r_xy = r_sum / (desc_stat.Count * desc_stat.Sx * desc_stat.Sy);

            Result.a1 = (r_xy * desc_stat.Sy) / desc_stat.Sx;
            Result.a0 = desc_stat.Y_med - Result.a1 * desc_stat.X_med;

            var x_step = Math.Sqrt( Math.Pow(desc_stat.X_max - desc_stat.X_min, 2) ) / desc_stat.Count;

            var s_ost_sum = 0.0;
            var mY_sum = 0.0;

            foreach (var item in data)
            {
                var x = item.X;
                var y = Result.a0 + Result.a1 * x;

                RegressionValues.Add(
                    new RegressionValue { X = x, Y = y}
                );

                s_ost_sum += Math.Pow( item.Y - y, 2);
                mY_sum += Math.Pow( x - desc_stat.X_med, 2 );

                x += Math.Round(x_step);
            }

            var min = RegressionValues.FirstOrDefault(x => x.X == RegressionValues.Min(x => x.X));
            var max = RegressionValues.FirstOrDefault(x => x.X == RegressionValues.Max(x => x.X));
            Result.RegressionValues.Add(min);
            Result.RegressionValues.Add(max);

            var S_ost = Math.Sqrt(s_ost_sum / (desc_stat.Count - 2));
            var mY = S_ost * Math.Sqrt(
                1 + 
                Math.Pow(desc_stat.X_min - desc_stat.X_med, 2)
                / mY_sum
            );

            var dY = threshold * mY;

            foreach (var item in RegressionValues)
            {
                var gamma_y_min = item.Y - dY;
                var gamma_y_max = item.Y + dY;

                if (gamma_y_min > desc_stat.Y_min)
                {
                    TrustMinValues.Add(
                        new RegressionValue { X = item.X, Y = gamma_y_min }
                    );
                }

                if (gamma_y_max < desc_stat.Y_max)
                {
                    TrustMaxValues.Add(
                        new RegressionValue { X = item.X, Y = gamma_y_max }
                    );
                }
            }

            var trust_low_min = TrustMinValues.FirstOrDefault(x => x.X == TrustMinValues.Min(x => x.X));
            var trust_low_max = TrustMinValues.FirstOrDefault(x => x.X == TrustMinValues.Max(x => x.X));
            Result.TrustMinValues.Add(trust_low_min);
            Result.TrustMinValues.Add(trust_low_max);

            var trust_hight_min = TrustMaxValues.FirstOrDefault(x => x.X == TrustMaxValues.Min(x => x.X));
            var trust_hight_max = TrustMaxValues.FirstOrDefault(x => x.X == TrustMaxValues.Max(x => x.X));
            Result.TrustMaxValues.Add(trust_hight_min);
            Result.TrustMaxValues.Add(trust_hight_max);

            return Result;
        }
    }
}