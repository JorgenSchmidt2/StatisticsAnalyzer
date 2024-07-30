using DataGenerator.Admin.Desktop.Core.Entities.DataEntities;

namespace DataGenerator.Admin.Desktop.Model.DataServices
{
    public class DataGenerateService
    {
        public static List<Values> GenerateRandomData(int PointCount, double x_Min, double x_Max, double y_Min, double y_Max)
        {
            var Result = new List<Values>();
            Random rnd = new Random();

            for (int i = 0; i < PointCount; i++)
            {
                var x = rnd.NextDouble() * (x_Max - x_Min) + x_Min;
                var y = rnd.NextDouble() * (y_Max - y_Min) + y_Min;

                Result.Add(
                    new Values { X = x, Y = y }
                );
            }

            return Result;
        }

        public static List<Values> GenerateDescendingData(int PointCount, double x_Min, double x_Max, double y_Min, double y_Max, double TrustInterval)
        {
            var Result = new List<Values>();
            Random rnd = new Random();

            var x_step = Math.Sqrt(Math.Pow(x_Max - x_Min, 2)) / PointCount;
            var y_step = Math.Sqrt(Math.Pow(y_Max - y_Min, 2)) / PointCount;

            var x_counter = x_Min + TrustInterval;
            var y_counter = y_Max - TrustInterval;
            for (int i = 0; i < PointCount; i++)
            {
                var x = x_counter + rnd.NextDouble() * (TrustInterval - (0 - TrustInterval)) + (0 - TrustInterval);
                var y = y_counter + rnd.NextDouble() * (TrustInterval - (0 - TrustInterval)) + (0 - TrustInterval);

                Result.Add(
                    new Values { X = x, Y = y }
                );

                x_counter += x_step;
                y_counter -= y_step;
            }

            return Result;
        }

        public static List<Values> GenerateAscendingData(int PointCount, double x_Min, double x_Max, double y_Min, double y_Max, double TrustInterval)
        {
            var Result = new List<Values>();
            Random rnd = new Random();

            var x_step = Math.Sqrt(Math.Pow(x_Max - x_Min, 2)) / PointCount;
            var y_step = Math.Sqrt(Math.Pow(y_Max - y_Min, 2)) / PointCount;

            var x_counter = x_Min + TrustInterval;
            var y_counter = y_Min + TrustInterval;
            for (int i = 0; i < PointCount; i++)
            {
                var x = x_counter + rnd.NextDouble() * (TrustInterval - (0 - TrustInterval)) + (0 - TrustInterval);
                var y = y_counter + rnd.NextDouble() * (TrustInterval - (0 - TrustInterval)) + (0 - TrustInterval);

                Result.Add(
                    new Values { X = x, Y = y }
                );

                x_counter += x_step;
                y_counter += y_step;
            }

            return Result;
        }
    }
}