using EStatAnalyser.Web.User.Desktop.Core.Configuration;
using EStatAnalyser.Web.User.Desktop.Core.Entities.DataEntities;
using EStatAnalyser.Web.User.Desktop.Core.Entities.GraphicsShellEntities;
using EStatAnalyser.Web.User.Desktop.Model.ConverterService;
using EStatAnalyser.Web.User.Desktop.Model.GraphicsShell;
using EStatAnalyser.Web.User.Desktop.Model.MathService;
using EStatAnalyser.Web.User.Desktop.UI.AppService;
using System;
using System.Collections.Generic;
using System.Windows;

namespace EStatAnalyser.Web.User.Desktop.UI.ViewModels
{
    public class DataAnalysisViewModel : NotifyPropertyChanged
    {
        #region Определение размеров графиков (внутренний)

        public int InternalFrameHeight
        {
            get
            {
                return GraphicsShellConfiguration.InternalCanvasHeight + GraphicsShellConfiguration.PointRadius + 2;
            }
        }

        public int InternalCanvasHeight
        {
            get
            {
                return GraphicsShellConfiguration.InternalCanvasHeight + GraphicsShellConfiguration.PointRadius;
            }
        }

        public int InternalFrameWidth
        {
            get
            {
                return GraphicsShellConfiguration.InternalCanvasWidth + GraphicsShellConfiguration.PointRadius + 2;
            }
        }

        public int InternalCanvasWidth
        {
            get
            {
                return GraphicsShellConfiguration.InternalCanvasWidth + GraphicsShellConfiguration.PointRadius;
            }
        }

        #endregion

        #region Определение размеров графиков (внешний)

        public int ExternalFrameHeight
        {
            get
            {
                return GraphicsShellConfiguration.ExternalCanvasHeight + GraphicsShellConfiguration.PointRadius + 2;
            }
        }

        public int ExternalCanvasHeight
        {
            get
            {
                return GraphicsShellConfiguration.ExternalCanvasHeight + GraphicsShellConfiguration.PointRadius;
            }
        }

        public int ExternalFrameWidth
        {
            get
            {
                return GraphicsShellConfiguration.ExternalCanvasWidth + GraphicsShellConfiguration.PointRadius + 2 + 60;
            }
        }

        public int ExternalCanvasWidth
        {
            get
            {
                return GraphicsShellConfiguration.ExternalCanvasWidth + GraphicsShellConfiguration.PointRadius + 30;
            }
        }

        #endregion

        #region Data

        //public List<SimpleData> data = new List<SimpleData>();

        public List<Sphere> mainData = GraphicsSketchers.GetSpheres(AppData.RewriteAnalysisData.Values);
        public List<Sphere> MainData
        {
            get
            {
                for (int c = 0; c < mainData.Count; c++)
                {
                    mainData[c].SphereMargin = ThicknessGetter.GetTranslatedCoords(
                        mainData[c].X,
                        mainData[c].Y,
                        AppData.DescriptionStatistics.X_min,
                        AppData.DescriptionStatistics.X_max,
                        AppData.DescriptionStatistics.Y_min,
                        AppData.DescriptionStatistics.Y_max
                    );
                }

                return mainData;
            }
            set
            {
                mainData = value;
                CheckChanges();
            }
        }

        public List<TextLabel> labelData = GraphicsSketchers.GetLabels(
                                                AppData.DescriptionStatistics.X_min,
                                                AppData.DescriptionStatistics.X_max,
                                                AppData.DescriptionStatistics.Y_min,
                                                AppData.DescriptionStatistics.Y_max
                                           );
        public List<TextLabel> LabelData
        {
            get 
            {
                for (int c = 0; c < labelData.Count; c++)
                {
                    labelData[c].LabelMargin = ThicknessGetter.GetCoords(
                        labelData[c].X,
                        labelData[c].Y
                    );
                }
                return labelData; 
            }
            set
            {
                labelData = value;
                CheckChanges();
            }
        }

        public List<Line> linesData = new List<Line>();
        public List<Line> LinesData
        {
            get { return linesData; }
            set
            {
                linesData = value;
                CheckChanges();
            }
        }
        #endregion

        #region Diagram data

        public string? xFieldName = AppData.RewriteAnalysisData.XFieldName;
        public string? XFieldName
        {
            get { return xFieldName; }
            set
            {
                xFieldName = value;
                CheckChanges();
            }
        }

        public string? yFieldName = AppData.RewriteAnalysisData.YFieldName;
        public string? YFieldName
        {
            get { return yFieldName; }
            set
            {
                yFieldName = value;
                CheckChanges();
            }
        }

        #endregion

        #region Main 

        public double thresholdZ = 3;
        public double ThresholdZ
        {
            get { return thresholdZ; }
            set
            {
                thresholdZ = value;
                CheckChanges();
            }
        }

        public Command ZFiltration
        {
            get
            {
                return new Command(
                    obj =>
                    {
                        try
                        {
                            if (ThresholdZ <= 0)
                            {
                                MessageBox.Show("Пороговое значение не может быть меньше нуля или равняться ему.");
                                return;
                            }

                            var list = Converters.ConvertSphereInfoToSimpleData(MainData);
                            AppData.RewriteAnalysisData.Values.Clear();
                            AppData.RewriteAnalysisData.Values = StatisticsOperation.GetZFilter(list, AppData.DescriptionStatistics, ThresholdZ);
                            AppData.DescriptionStatistics = StatisticsOperation.GetDescriptionStatistics(AppData.RewriteAnalysisData.Values);
                            MainData = GraphicsSketchers.GetSpheres(AppData.RewriteAnalysisData.Values);
                            AnalysisResult = Converters.ConvertDescritpionStatisticsToString(AppData.DescriptionStatistics);
                            LabelData = GraphicsSketchers.GetLabels(
                                                    AppData.DescriptionStatistics.X_min,
                                                    AppData.DescriptionStatistics.X_max,
                                                    AppData.DescriptionStatistics.Y_min,
                                                    AppData.DescriptionStatistics.Y_max
                                               );

                            LinesData = new List<Line>();
                            a0 = 0;
                            a1 = 0;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ошибка: \n", ex.Message);
                        }
                    }    
                );
            }
        }

        public Command ResetFiltration
        {
            get
            {
                return new Command(
                    obj =>
                    {
                        try
                        {
                            AppData.RewriteAnalysisData = Converters.CopyUploadData(AppData.AnalysisData);
                            AppData.DescriptionStatistics = StatisticsOperation.GetDescriptionStatistics(AppData.RewriteAnalysisData.Values);
                            MainData = GraphicsSketchers.GetSpheres(AppData.RewriteAnalysisData.Values);
                            AnalysisResult = Converters.ConvertDescritpionStatisticsToString(AppData.DescriptionStatistics);
                            LabelData = GraphicsSketchers.GetLabels(
                                                    AppData.DescriptionStatistics.X_min,
                                                    AppData.DescriptionStatistics.X_max,
                                                    AppData.DescriptionStatistics.Y_min,
                                                    AppData.DescriptionStatistics.Y_max
                                               );

                            LinesData = new List<Line>();
                            a0 = 0;
                            a1 = 0;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ошибка: \n", ex.Message);
                        }
                    }
                );
            }
        }

        public Command Regression
        {
            get
            {
                return new Command(
                    obj =>
                    {
                        var RegressionDatas = StatisticsOperation.GetRegressionData(MainData, AppData.DescriptionStatistics, 2.04);

                        if (RegressionDatas == null)
                        {
                            return;
                        }

                        a0 = RegressionDatas.a0;
                        a1 = RegressionDatas.a1;

                        var Lines = GraphicsSketchers.GetRegressionLines(RegressionDatas, AppData.DescriptionStatistics);
                        LinesData = Lines;
                    }
                );
            }
        }

        public string? analysisResult = Converters.ConvertDescritpionStatisticsToString(AppData.DescriptionStatistics);
        public string? AnalysisResult
        {
            get { return analysisResult; }
            set
            {
                analysisResult = value;
                CheckChanges();
            }
        }

        public string? reportName;
        public string? ReportName
        {
            get { return reportName; }
            set
            {
                reportName = value;
                CheckChanges();
            }
        }

        private double a0 = 0;
        private double a1 = 0;
        public Command CreateReport
        {
            get
            {
                return new Command(
                    obj =>
                    {

                    }    
                );
            }
        }

        #endregion

        #region

        public Command Help
        {
            get
            {
                return new Command(
                    obj =>
                    {
                        var Content = "Help";
                        MessageBox.Show(Content);
                    }
                );
            }
        }

        public Command Close
        {
            get
            {
                return new Command(
                    obj =>
                    {
                        AppData.AnalysisData = null;
                        AppData.AnalysisData = new UploadData();

                        AppData.RewriteAnalysisData = null;
                        AppData.RewriteAnalysisData = new UploadData();

                        AppData.DescriptionStatistics = null;
                        AppData.DescriptionStatistics = new DescriptionStatistics();

                        WindowsObjects.DataAnalysisWindow.Close();
                        WindowsObjects.DataAnalysisWindow = null;
                    }
                );
            }
        }

        #endregion
    }
}