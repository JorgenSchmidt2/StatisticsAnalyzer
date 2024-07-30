using EStatAnalyser.Web.User.Desktop.Core.Configuration;
using EStatAnalyser.Web.User.Desktop.Core.Entities.DataEntities;
using EStatAnalyser.Web.User.Desktop.Model.ConverterService;
using EStatAnalyser.Web.User.Desktop.Model.FileService;
using EStatAnalyser.Web.User.Desktop.Model.MathService;
using EStatAnalyser.Web.User.Desktop.NetworkUtils.Converters;
using EStatAnalyser.Web.User.Desktop.NetworkUtils.Requests;
using EStatAnalyser.Web.User.Desktop.UI.AppService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace EStatAnalyser.Web.User.Desktop.UI.ViewModels.DataViews
{
    public class OpenUploadingViewModel : NotifyPropertyChanged
    {
        #region Query

        public string? query;
        public string? Query
        {
            get { return query; }
            set
            {
                query = value;
                CheckChanges();
            }
        }

        public Command Search
        {
            get
            {
                return new Command(
                    obj =>
                    {
                        if (!RequestWasLaunched)
                        {

                        }
                    }    
                );
            }
        }

        public Command Reset
        {
            get
            {
                return new Command(
                    obj =>
                    {
                        if (!RequestWasLaunched)
                        {

                        }
                    }
                );
            }
        }

        #endregion

        #region Data

        public List<FileData>? fileDatas = AppData.FileData;
        public List<FileData>? FileDatas
        {
            get { return fileDatas; }
            set
            {
                fileDatas = value;
                CheckChanges();
            }
        }

        #endregion

        #region Main operations

        public int id;
        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                CheckChanges();
            }
        }

        public Command Analysis
        {
            get
            {
                return new Command(
                    obj =>
                    {
                        if (!RequestWasLaunched)
                        {
                            if (FileDatas == null || FileDatas.Count == 0 || !FileDatas.Exists(x => x.Id == Id))
                            {
                                MessageBox.Show("В списке файлов не найдено ID с таким номером.");
                                return;
                            }

                            var CurrentFileData = FileDatas.FirstOrDefault(x => x.Id == Id);
                            if (CurrentFileData == null)
                            {
                                MessageBox.Show("Не удалось правильно загрузить объект Upload Data с указанным ID.");
                                return;
                            }

                            var CurrentContentData = ContentGetters.GetContentFromFile(CurrentFileData.FullPath, true);
                            if (String.IsNullOrEmpty(CurrentContentData))
                            {
                                MessageBox.Show("Не удалось правильно загрузить контент файла с указанным ID.");
                                return;
                            }

                            var CurrentUploadData = Converters.ConvertContentToUploadData(CurrentContentData);
                            AppData.AnalysisData = Converters.CopyUploadData(CurrentUploadData);
                            AppData.RewriteAnalysisData = Converters.CopyUploadData(CurrentUploadData);
                            AppData.DescriptionStatistics = StatisticsOperation.GetDescriptionStatistics(AppData.RewriteAnalysisData.Values);

                            WindowsObjects.DataAnalysisWindow = new();
                            if (WindowsObjects.DataAnalysisWindow.ShowDialog() == true)
                            {
                                WindowsObjects.DataAnalysisWindow.Show();
                            }
                        }
                    }
                );
            }
        }

        public Command SendByID
        {
            get
            {
                return new Command(
                    async (obj) =>
                    {
                        try
                        {
                            if (!RequestWasLaunched)
                            {
                                if (FileDatas == null || FileDatas.Count == 0 || !FileDatas.Exists(x => x.Id == Id))
                                {
                                    MessageBox.Show("В списке файлов не найдено ID с таким номером.");
                                    return;
                                }

                                var InputContent = ContentGetters.GetContentFromFile(FileDatas[Id - 1].FullPath, true);
                                if (String.IsNullOrEmpty(InputContent)) { return; }

                                var UploadData = Converters.ConvertContentToUploadData(InputContent);
                                if (UploadData == null) { return; }

                                var JSONData = UploadDataConverter.ConvertUploadToJSON(UploadData, true);
                                if (String.IsNullOrEmpty(JSONData))
                                {
                                    MessageBox.Show("Не удалось сформировать JSON файл.");
                                    return;
                                }

                                RequestWasLaunched = true;

                                MessageBox.Show("Сейчас будет выполнен запрос на получение данных. Не закрывайте приложение в течении 7 секунд и не переходите по следующим окнам.");
                                var RequestStatus = await RequestsLogic.AddData(JSONData);
                                if (!RequestStatus.Success)
                                {
                                    MessageBox.Show("Не удалось выполнить запрос на добавление данных.\nОшибка:\n\n" + RequestStatus.Message);
                                    RequestWasLaunched = false;
                                    return;
                                }

                                var CurrentFilePath = Environment.CurrentDirectory 
                                    + "\\" + AppFolders.FormatFiles
                                    + "\\" + FileDatas[Id -1].FileName;

                                var TargetFilePath = Environment.CurrentDirectory
                                    + "\\" + AppFolders.FormatFiles
                                    + "\\Used"
                                    + "\\" + FileDatas[Id - 1].FileName;

                                FileMovers.MoveFile(CurrentFilePath, TargetFilePath, true);
                                AppData.FileData = GetDirectoryInformation.GetFileNamesFromFolder(AppFolders.FormatFiles, true);
                                FileDatas = AppData.FileData;

                                RequestWasLaunched = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            RequestWasLaunched = false;
                            MessageBox.Show("Ошибка: \n" + ex.Message);
                        }
                    }
                );
            }
        }

        public Command SendAll
        {
            get
            {
                return new Command(
                    obj =>
                    {
                        if (!RequestWasLaunched)
                        {

                        }
                    }
                );
            }
        }

        #endregion

        #region Optional

        public Command Help
        {
            get
            {
                return new Command(
                    obj =>
                    {
                        if (!RequestWasLaunched)
                        {
                            var Content = "Help";
                            MessageBox.Show(Content);
                        }
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
                        if (!RequestWasLaunched)
                        {
                            AppData.FileData.Clear();
                            WindowsObjects.OpenUploadingWindow.Close();
                            WindowsObjects.OpenUploadingWindow = null;
                        }
                    }
                );
            }
        }

        #endregion

        private bool RequestWasLaunched = false;
    }
}