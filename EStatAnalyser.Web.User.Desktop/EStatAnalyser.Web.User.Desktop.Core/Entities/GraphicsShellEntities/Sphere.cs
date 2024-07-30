using EStatAnalyser.Web.User.Desktop.Core.Configuration;
using System.Windows.Media;

namespace EStatAnalyser.Web.User.Desktop.Core.Entities.GraphicsShellEntities
{
    public class Sphere
    {
        public double X { get; set; }
        public double Y { get; set; }

        // Параметры графической составляющей, используемой при отображении UI
        public object? SphereMargin { get; set; } // Отвечает за местоположение объекта на plot'е

        // Радиус точки
        public int Radius
        {
            get
            {
                return GraphicsShellConfiguration.PointRadius;
            }
        }
        public int StrokeThicknessValue
        {
            get
            {
                return GraphicsShellConfiguration.PointThickness;
            }
        }
        public SolidColorBrush Color
        {
            get
            {
                return GraphicsShellConfiguration.PointColor;
            }
        }
    }
}