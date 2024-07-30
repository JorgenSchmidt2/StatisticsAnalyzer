using DataGenerator.Admin.Desktop.Core.Configurations;
using System.Windows.Media;

namespace DataGenerator.Admin.Desktop.Core.Entities.GraphicsEntities
{
    public class PointEntity
    {
        public int X { get; set; }
        public int Y { get; set; }

        // Параметры графической составляющей, используемой при отображении UI
        public object SphereMargin { get; set; } // Отвечает за местоположение объекта на plot'е

        // Readonly свойства
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

        public int Radius
        {
            get
            {
                return GraphicsShellConfiguration.PointRadius;
            }
        }
    }
}
