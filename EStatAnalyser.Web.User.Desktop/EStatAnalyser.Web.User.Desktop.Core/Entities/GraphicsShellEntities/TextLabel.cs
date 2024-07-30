using EStatAnalyser.Web.User.Desktop.Core.Configuration;

namespace EStatAnalyser.Web.User.Desktop.Core.Entities.GraphicsShellEntities
{
    public class TextLabel
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string? Content { get; set; }
        public object? LabelMargin { get; set; }
        public int FontSize
        {
            get
            {
                return GraphicsShellConfiguration.NumLabelFontSize;
            }
        }
    }
}