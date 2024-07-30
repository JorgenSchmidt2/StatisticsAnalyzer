using System.Windows;

namespace DataGenerator.Admin.Desktop.UI.AppService
{
    public class ToThickness
    {
        public static object Convert(int x, int y)
        {
            return new Thickness(x, 0, 0, y);
        }
    }
}
