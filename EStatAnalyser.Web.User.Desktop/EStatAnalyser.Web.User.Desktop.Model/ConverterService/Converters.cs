using EStatAnalyser.Web.User.Desktop.Core.Configuration;
using EStatAnalyser.Web.User.Desktop.Core.Entities.DataEntities;
using EStatAnalyser.Web.User.Desktop.Core.Entities.GraphicsShellEntities;
using EStatAnalyser.Web.User.Desktop.Model.MessageService.MessageClasses;

namespace EStatAnalyser.Web.User.Desktop.Model.ConverterService
{
    public class Converters
    {
        #region Simple data
        public static List<SimpleData> ConvertContentToSimpleData(string content)
        {
            try
            {
                List<SimpleData> Result = new();


                var strings = content.Split("\n");
                var Counter = 0;
                foreach (var item in strings)
                {
                    if (Counter != 0)
                    {
                        var splits = item.Split("\t");
                        if (splits.Length == 2)
                        {
                            Result.Add(
                                new SimpleData
                                {
                                    X = Convert.ToDouble(splits[0].Replace('.', ',')),
                                    Y = Convert.ToDouble(splits[1].Replace('.', ','))
                                }
                            );
                        }
                        else
                        {
                            if (Counter != strings.Length - 1)
                            {
                                throw new Exception("Не удалось конвертировать содержимое файла с указанным ID в объект. Строка №" + Counter);
                            }
                        }
                    }
                    Counter++;
                }

                return Result;
            }
            catch (Exception ex)
            {
                MessageObjects.Sender.SendMessage("Ошибка: \n" + ex.Message);
                return null;
            }
        }
        #endregion

        #region Upload data
        public static UploadData ConvertContentToUploadData(string content)
        {
            try
            {
                UploadData Result = new();
                Result.Values = new List<SimpleData>();

                var strings = content.Split("\n");
                var Counter = 0;
                foreach (var item in strings)
                {
                    if (Counter != 0)
                    {
                        var splits = item.Split('\t');
                        if (splits.Length == 2)
                        {
                            Result.Values.Add(
                                new SimpleData
                                {
                                    X = Convert.ToDouble(splits[0].Replace('.', ',')),
                                    Y = Convert.ToDouble(splits[1].Replace('.', ','))
                                }
                            );
                        }
                        else
                        {
                            if (Counter != strings.Length - 1)
                            {
                                throw new Exception("Не удалось конвертировать содержимое файла с указанным ID в объект. Строка №" + Counter);
                            }
                        }
                    }
                    else
                    {
                        var splits = item.Split('\t');
                        if (splits.Length != 5)
                        {
                            throw new Exception("Заголовочный файл имеет неправильную структуру");
                        }
                        Result.XFieldName = splits[0];
                        Result.YFieldName = splits[1];
                        Result.DataType = splits[2];
                        Result.Description = splits[3];
                        Result.WasAnalysed = false;
                    }
                    Counter++;
                }

                return Result;
            }
            catch (Exception ex)
            {
                MessageObjects.Sender.SendMessage("Ошибка: \n" + ex.Message);
                return null;
            }
        }

        public static UploadData ConvertSimpleToUploadData
            (
             string XFieldName,
             string YFieldName,
             string DataType, 
             string Description,
             List<SimpleData> input
            )
        {
            try
            {
                // Обработка ошибок
                if (input.Count == 0)
                {
                    throw new Exception("Список значений оказался пустым.");
                }

                // Инициализация
                UploadData Result = new UploadData();
                Result.Values = new List<SimpleData>();

                // Операция конвертации
                Result.XFieldName = XFieldName;
                Result.YFieldName = YFieldName;
                Result.DataType = DataType;
                Result.Description = Description;
                Result.WasAnalysed = false;

                foreach (var item in input)
                {
                    Result.Values.Add(new SimpleData { X = item.X, Y = item.Y });
                }
                return Result;
            }
            catch (Exception ex)
            {
                MessageObjects.Sender.SendMessage("Ошибка: \n" + ex.Message);
                return null;
            }
        }

        public static UploadData CopyUploadData(UploadData data)
        {
            UploadData Result = new UploadData();
            Result.Values = new List<SimpleData>();

            Result.XFieldName = data.XFieldName;
            Result.YFieldName = data.YFieldName;
            Result.DataType = data.DataType;
            Result.Description = data.Description;
            Result.WasAnalysed = data.WasAnalysed;

            if (data.Values != null)
            {
                foreach (var item in data.Values)
                {
                    Result.Values.Add(
                        new SimpleData { Id = item.Id, X = item.X, Y = item.Y}    
                    );
                }
            }

            return Result;
        }
        #endregion

        #region String

        public static string ConvertUploadDataToString(UploadData input)
        {
            try
            {
                string Result = "";

                Result += input.XFieldName + "\t";
                Result += input.YFieldName + "\t";
                Result += input.DataType + "\t";
                Result += input.Description.Replace("\n",". ") + "\t";
                Result += input.WasAnalysed + "\n";

                foreach (var item in input.Values)
                {
                    Result += item.X + "\t" + item.Y + "\n";
                }

                return Result;
            }
            catch (Exception ex)
            {
                MessageObjects.Sender.SendMessage("Ошибка: \n" + ex.Message);
                return null;
            }
        }

        public static string ConvertDescritpionStatisticsToString(DescriptionStatistics data)
        {
            string Result = "";

            Result += "X_MIN: " + Math.Round(data.X_min, 1) + "; X_MAX: " + Math.Round(data.X_max, 1) + "; X_AV: " + Math.Round(data.X_med, 1) + "\n";
            Result += "Y_MIN: " + Math.Round(data.Y_min, 1) + "; Y_MAX: " + Math.Round(data.Y_max, 1) + "; Y_AV: " + Math.Round(data.Y_med, 1) + "\n";
            Result += "Sx: " + Math.Round(data.Sx, 1) + "; Ax: " + Math.Round(data.Ax, 1) + "; Ex: " + Math.Round(data.Ex, 1) + "\n";
            Result += "Sy: " + Math.Round(data.Sy, 1) + "; Ay: " + Math.Round(data.Ay, 1) + "; Ey: " + Math.Round(data.Ey, 1) + "\n";

            return Result;
        }

        #endregion

        #region GraphicsShell
        public static List<SimpleData> ConvertSphereInfoToSimpleData(List<Sphere> data)
        {
            List<SimpleData> Result = new List<SimpleData>();

            foreach (var item in data)
            {
                Result.Add(
                    new SimpleData { X = item.X, Y = item.Y }    
                );
            }

            return Result;
        }
        #endregion
    }
}