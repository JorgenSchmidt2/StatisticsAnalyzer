using DataGenerator.Admin.Desktop.Core.Configurations;
using DataGenerator.Admin.Desktop.Core.Entities.DataEntities;
using DataGenerator.Admin.Desktop.Core.Entities.GraphicsEntities;
using DataGenerator.Admin.Desktop.Core.Enums;
using DataGenerator.Admin.Desktop.Model.DataServices;
using DataGenerator.Admin.Desktop.Model.FileServices;
using DataGenerator.Admin.Desktop.Model.GraphicsShell;
using DataGenerator.Admin.Desktop.UI.AppService;
using System;
using System.Collections.Generic;
using System.Windows;

namespace DataGenerator.Admin.Desktop.UI.ViewModels
{
    public class AppWindowViewModel : NotifyPropertyChanged
    {
        #region Конфигурация части окна для отрисовки элементов (высота, ширина окна отрисовки)
        public int frameHeight = GraphicsShellConfiguration.CanvasHeight + GraphicsShellConfiguration.PointRadius + 2;
        public int FrameHeight
        {
            get { return frameHeight; }
            set
            {
                frameHeight = value;
                CheckChanges();
            }
        }

        public int frameWidth = GraphicsShellConfiguration.CanvasWidth + GraphicsShellConfiguration.PointRadius + 2;
        public int FrameWidth
        {
            get { return frameWidth; }
            set
            {
                frameWidth = value;
                CheckChanges();
            }
        }

        public int canvasHeight = GraphicsShellConfiguration.CanvasHeight + GraphicsShellConfiguration.PointRadius;
        public int CanvasHeight
        {
            get { return canvasHeight; }
            set
            {
                canvasHeight = value;
                CheckChanges();
            }
        }

        public int canvasWidth = GraphicsShellConfiguration.CanvasWidth + GraphicsShellConfiguration.PointRadius;
        public int CanvasWidth
        {
            get { return canvasWidth; }
            set
            {
                canvasWidth = value;
                CheckChanges();
            }
        }
        #endregion

        #region Графика
        public List<PointEntity> dataPoints = new List<PointEntity>();
        public List<PointEntity> DataPoints
        {
            get
            {
                for (int i = 0; i < dataPoints.Count; i++)
                {
                    dataPoints[i].SphereMargin = ToThickness.Convert(dataPoints[i].X, dataPoints[i].Y);
                }
                return dataPoints;
            }
            set
            {
                dataPoints = value;
                CheckChanges();
            }
        }
        #endregion

        #region Вводимые данные
        public string? fileName;
        public string? FileName
        {
            get { return fileName; }
            set
            {
                fileName = value;
                CheckChanges();
            }
        }

        public string? type;
        public string? Type
        {
            get { return type; }
            set
            {
                type = value;
                CheckChanges();
            }
        }

        public string? description;
        public string? Description
        {
            get { return description; }
            set
            {
                description = value;
                CheckChanges();
            }
        }

        public string? xFieldName;
        public string? XFieldName
        {
            get { return xFieldName; }
            set
            {
                xFieldName = value;
                CheckChanges();
            }
        }

        public string? yFieldName;
        public string? YFieldName
        {
            get { return yFieldName; }
            set
            {
                yFieldName = value;
                CheckChanges();
            }
        }

        public int pointsCount;
        public int PointsCount
        {
            get { return pointsCount; }
            set
            {
                pointsCount = value;
                CheckChanges();
            }
        }

        public double x_Min;
        public double X_Min
        {
            get { return x_Min; }
            set
            {
                x_Min = value;
                CheckChanges();
            }
        }

        public double x_Max;
        public double X_Max
        {
            get { return x_Max; }
            set
            {
                x_Max = value;
                CheckChanges();
            }
        }

        public double y_Min;
        public double Y_Min
        {
            get { return y_Min; }
            set
            {
                y_Min = value;
                CheckChanges();
            }
        }
        public double y_Max;
        public double Y_Max
        {
            get { return y_Max; }
            set
            {
                y_Max = value;
                CheckChanges();
            }
        }

        public double trustInterval;
        public double TrustInterval
        {
            get { return trustInterval; }
            set
            {
                trustInterval = value;
                CheckChanges();
            }
        }
        #endregion

        #region Управление

        public GenerateModes CurrentMode = GenerateModes.RandomMode;

        public bool randomModeIsEnabled = false;
        public bool RandomModeIsEnabled
        {
            get { return randomModeIsEnabled; }
            set
            {
                randomModeIsEnabled = value;
                CheckChanges();
            }
        }
        public Command RandomMode
        {
            get
            {
                return new Command(
                    obj =>
                    {
                        RandomModeIsEnabled = false;
                        DescendingModeIsEnabled = true;
                        AscendingModeIsEnabled = true;
                        CurrentMode = GenerateModes.RandomMode;
                    }
                );
            }
        }

        public bool descendingModeIsEnabled = true;
        public bool DescendingModeIsEnabled
        {
            get { return descendingModeIsEnabled; }
            set
            {
                descendingModeIsEnabled = value;
                CheckChanges();
            }
        }
        public Command DescendingMode
        {
            get
            {
                return new Command(
                    obj =>
                    {
                        RandomModeIsEnabled = true;
                        DescendingModeIsEnabled = false;
                        AscendingModeIsEnabled = true;
                        CurrentMode = GenerateModes.DescendingMode;
                    }
                );
            }
        }

        public bool ascendingModeIsEnabled = true;
        public bool AscendingModeIsEnabled
        {
            get { return ascendingModeIsEnabled; }
            set
            {
                ascendingModeIsEnabled = value;
                CheckChanges();
            }
        }
        public Command AscendingMode
        {
            get
            {
                return new Command(
                    obj =>
                    {
                        RandomModeIsEnabled = true;
                        DescendingModeIsEnabled = true;
                        AscendingModeIsEnabled = false;
                        CurrentMode = GenerateModes.AscendingMode;
                    }
                );
            }
        }

        public Data GeneratedData = new Data
        {
            Values = new List<Values>()
        };
        public Command Calculate
        {
            get
            {
                return new Command(
                    obj =>
                    {
                        if (PointsCount < 50)
                        {
                            MessageBox.Show("Выборка должна состоять более чем из 50 элементов. Иначе выборка будет считаться непрезентативной.");
                            return;
                        }
                        if (X_Min >= X_Max || Y_Min >= Y_Max)
                        {
                            MessageBox.Show("Минимальное значение генерации по оси X или Y не может быть больше или равно максимальному.");
                            return;
                        }

                        var x_length = Math.Sqrt(Math.Pow(X_Max - X_Min, 2));
                        var y_length = Math.Sqrt(Math.Pow(Y_Max - Y_Min, 2));

                        switch (CurrentMode)
                        {
                            case GenerateModes.RandomMode:
                                GeneratedData.Values = DataGenerateService.GenerateRandomData(PointsCount, X_Min, X_Max, Y_Min, Y_Max);
                                break;
                            case GenerateModes.DescendingMode:
                                if (TrustInterval <= 0)
                                {
                                    MessageBox.Show("Значение доверительного интервала не может быть равно 0, или быть меньше чем 0.");
                                    return;
                                }
                                if (TrustInterval > x_length || TrustInterval > y_length)
                                {
                                    MessageBox.Show("Рекомендуется для доверительного интервала ввести значение меньшее чем, длина обоих осей.\nНесмотря на данную ошибку при вводе, расчёт произведён будет.");
                                }
                                GeneratedData.Values = DataGenerateService.GenerateDescendingData(PointsCount, X_Min, X_Max, Y_Min, Y_Max, TrustInterval);
                                break;
                            case GenerateModes.AscendingMode:
                                if (TrustInterval <= 0)
                                {
                                    MessageBox.Show("Значение доверительного интервала не может быть равно 0, или быть меньше чем 0.");
                                    return;
                                }
                                if (TrustInterval > x_length || TrustInterval > y_length)
                                {
                                    MessageBox.Show("Рекомендуется для доверительного интервала ввести значение меньшее чем, длина обоих осей.\nНесмотря на данную ошибку при вводе, расчёт произведён будет.");
                                }
                                GeneratedData.Values = DataGenerateService.GenerateAscendingData(PointsCount, X_Min, X_Max, Y_Min, Y_Max, TrustInterval);
                                break;
                        }

                        DataPoints = PointSketcher.GetGraphics(GeneratedData.Values);
                    }
                );
            }
        }

        public Command WriteToFile
        {
            get
            {
                return new Command(
                    obj =>
                    {
                        if (GeneratedData.Values.Count == 0)
                        {
                            MessageBox.Show("Прежде чем сохранять данные в файл, их нужно сгенерировать.");
                            return;
                        }
                        if (string.IsNullOrEmpty(XFieldName))
                        {
                            MessageBox.Show("Введите имя поля Х.");
                            return;
                        }
                        if (string.IsNullOrEmpty(YFieldName))
                        {
                            MessageBox.Show("Введите имя поля Y.");
                            return;
                        }
                        if (string.IsNullOrEmpty(Type))
                        {
                            MessageBox.Show("Введите название типа для генерируемых данных (например, к какой предметной области относятся, \n для данного поля лучше использовать не более 3-5 слов.)");
                            return;
                        }
                        if (string.IsNullOrEmpty(Description))
                        {
                            MessageBox.Show("Введите описание данных.");
                            return;
                        }
                        if (string.IsNullOrEmpty(FileName))
                        {
                            MessageBox.Show("Введите имя файла.");
                            return;
                        }

                        GeneratedData.FieldName_X = XFieldName;
                        GeneratedData.FieldName_Y = YFieldName;
                        GeneratedData.Type = Type;
                        GeneratedData.Description = Description;

                        FileRecorder.Record(GeneratedData, FileName);

                        MessageBox.Show("Файл " + FileName + ".txt успешно записан в директорию с программой.");
                    }
                );
            }
        }
        #endregion

    }
}
