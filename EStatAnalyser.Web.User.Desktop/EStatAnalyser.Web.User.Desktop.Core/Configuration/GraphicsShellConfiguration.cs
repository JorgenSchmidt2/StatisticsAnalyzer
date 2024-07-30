﻿using System.Windows.Media;

namespace EStatAnalyser.Web.User.Desktop.Core.Configuration
{
    public class GraphicsShellConfiguration
    {
        // Размеры окна данных
        public static readonly int InternalCanvasWidth = 500;
        public static readonly int InternalCanvasHeight = 500;
        public static readonly int ExternalCanvasWidth = 550;
        public static readonly int ExternalCanvasHeight = 550;

        // Количество точек для построения линии тренда
        public static readonly int TrendPointCount = 20;

        // Количество подписей для координатной оси
        public static readonly int XLabelCount = 10;
        public static readonly int YLabelCount = 10;

        // Конфигурация точек
        public static readonly int PointRadius = 5;
        public static readonly int PointThickness = 1;
        public static readonly SolidColorBrush PointColor = Brushes.Red;

        // Конфигурация численных подписей (по осям X, Y)
        public static readonly int NumLabelFontSize = 10;

        // Конфигурация линий
        public static readonly int LineStrokeThickness = 1;
        public static readonly SolidColorBrush RegressionLinesColor = Brushes.Red;
        public static readonly SolidColorBrush TrustLinesColor = Brushes.Gray;
    }
}