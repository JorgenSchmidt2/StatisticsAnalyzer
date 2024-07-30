using DataGenerator.Admin.Desktop.Core.Configurations;
using DataGenerator.Admin.Desktop.Core.Entities.DataEntities;
using DataGenerator.Admin.Desktop.Core.Entities.GraphicsEntities;

namespace DataGenerator.Admin.Desktop.Model.GraphicsShell
{
    public class PointSketcher
    {
        public static List<PointEntity> GetGraphics(List<Values> Values)
        {
            List<PointEntity> Result = new List<PointEntity>();

            var x_min = Values.MinBy(ent => ent.X).X;
            var x_max = Values.MaxBy(ent => ent.X).X;
            var y_min = Values.MinBy(ent => ent.Y).Y;
            var y_max = Values.MaxBy(ent => ent.Y).Y;

            var x_len = Math.Sqrt(Math.Pow(x_max - x_min, 2));
            var y_len = Math.Sqrt(Math.Pow(y_max - y_min, 2));

            var x_koef = GraphicsShellConfiguration.CanvasWidth / x_len;
            var y_koef = GraphicsShellConfiguration.CanvasHeight / y_len;

            // По закону (t-min)*k, где k - коэффициент поправки на размер полотна по одной из осей
            // k при этом является отношением длины полотна на одной из осей и длины участка значений
            foreach (Values value in Values)
            {
                Result.Add(
                    new PointEntity
                    {
                        X = Convert.ToInt32((value.X - x_min) * x_koef),
                        Y = Convert.ToInt32((value.Y - y_min) * y_koef)
                    }
                );
            }

            return Result;
        }
    }
}