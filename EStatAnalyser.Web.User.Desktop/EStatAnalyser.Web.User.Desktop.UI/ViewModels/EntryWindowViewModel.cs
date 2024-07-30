using EStatAnalyser.Web.User.Desktop.Core.Configuration;
using EStatAnalyser.Web.User.Desktop.Model.FileService;
using EStatAnalyser.Web.User.Desktop.NetworkUtils.Converters;
using EStatAnalyser.Web.User.Desktop.NetworkUtils.Requests;
using EStatAnalyser.Web.User.Desktop.UI.AppService;
using System;
using System.Windows;

namespace EStatAnalyser.Web.User.Desktop.UI.ViewModels
{
    public class EntryWindowViewModel : NotifyPropertyChanged
    {
        public Command OpenUploadFile
        {
            get
            {
                return new Command(
                    obj =>
                    {
                        if (!RequestWasLaunched)
                        {
                            AppData.FileData = GetDirectoryInformation.GetFileNamesFromFolder(AppFolders.FormatFiles, true);
                            if (AppData.FileData.Count == 0) { return; }

                            WindowsObjects.OpenUploadingWindow = new();
                            if (WindowsObjects.OpenUploadingWindow.ShowDialog() == true)
                            {
                                WindowsObjects.OpenUploadingWindow.Show();
                            }
                        }
                    }    
                );
            }
        }

        public Command OpenSimpleFile
        {
            get
            {
                return new Command(
                    obj =>
                    {
                        if (!RequestWasLaunched)
                        {
                            AppData.FileData = GetDirectoryInformation.GetFileNamesFromFolder(AppFolders.DataFiles, false);
                            if (AppData.FileData.Count == 0) { return; }

                            WindowsObjects.OpenSimpleDataWindow = new();
                            if (WindowsObjects.OpenSimpleDataWindow.ShowDialog() == true)
                            {
                                WindowsObjects.OpenSimpleDataWindow.Show();
                            }
                        }
                    }
                );
            }
        }

        public Command OpenDataBase
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
                                RequestWasLaunched = true;
                                MessageBox.Show("Сейчас будет выполнен запрос на получение данных. Не закрывайте приложение в течении 7 секунд и не переходите по следующим окнам.");
                                var request = await RequestsLogic.GetAll();
                                if (request == null || String.IsNullOrEmpty(request))
                                {
                                    MessageBox.Show("Запрос к базе данных на получение всего списка объектов не прошёл.");
                                    RequestWasLaunched = false;
                                    return;
                                }

                                var data = UploadDataConverter.ConvertJSONToUploadList(request);
                                AppData.DataList = data;

                                RequestWasLaunched = false;

                                WindowsObjects.DataBaseOperatorWindow = new();
                                if (WindowsObjects.DataBaseOperatorWindow.ShowDialog() == true)
                                {
                                    WindowsObjects.DataBaseOperatorWindow.Show();
                                }
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

        public Command GetInfo
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

        private bool RequestWasLaunched = false;
    }
}