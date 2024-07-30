using EStatAnalyser.Web.User.Desktop.Core.Entities.DataEntities;
using EStatAnalyser.Web.User.Desktop.Model.ConverterService;
using EStatAnalyser.Web.User.Desktop.Model.MathService;
using EStatAnalyser.Web.User.Desktop.NetworkUtils.Converters;
using EStatAnalyser.Web.User.Desktop.NetworkUtils.Requests;
using EStatAnalyser.Web.User.Desktop.UI.AppService;
using System;
using System.Collections.Generic;
using System.Windows;

namespace EStatAnalyser.Web.User.Desktop.UI.ViewModels.DataViews
{
    public class DataBaseOperatorViewModel : NotifyPropertyChanged
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

        public List<UploadData>? uploadDatas = AppData.DataList;
        public List<UploadData>? UploadDatas
        {
            get { return uploadDatas; }
            set
            {
                uploadDatas = value;
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
                    async obj =>
                    {
                        try
                        {
                            if (!RequestWasLaunched)
                            {
                                if (UploadDatas == null || !UploadDatas.Exists(x => x.Id == Id))
                                {
                                    MessageBox.Show("Введите значение Id, существующее в списке.");
                                    return;
                                }

                                RequestWasLaunched = true;
                                MessageBox.Show("Сейчас будет выполнен запрос на получение данных. Не закрывайте приложение в течении 7 секунд и не переходите по следующим окнам.");

                                var request = await RequestsLogic.GetById(Id);
                                if (request == null || String.IsNullOrEmpty(request))
                                {
                                    MessageBox.Show("Запрос к базе данных на получение всего списка объектов не прошёл.");
                                    RequestWasLaunched = false;
                                    return;
                                }

                                var data = UploadDataConverter.ConvertJSONToUpload(request);
                                AppData.AnalysisData = Converters.CopyUploadData(data);
                                AppData.RewriteAnalysisData = Converters.CopyUploadData(data);
                                AppData.DescriptionStatistics = StatisticsOperation.GetDescriptionStatistics(AppData.RewriteAnalysisData.Values);

                                RequestWasLaunched = false;

                                WindowsObjects.DataAnalysisWindow = new();
                                if (WindowsObjects.DataAnalysisWindow.ShowDialog() == true)
                                {
                                    WindowsObjects.DataAnalysisWindow.Show();
                                }
                            }
                        }
                        catch(Exception ex)
                        {
                            RequestWasLaunched = false;
                            MessageBox.Show("Ошибка: \n" + ex.Message);
                        }
                    }
                );
            }
        }

        public Command DeleteFromDB
        {
            get
            {
                return new Command(
                    async obj =>
                    {
                        if (!RequestWasLaunched)
                        {
                            if (UploadDatas == null || !UploadDatas.Exists(x => x.Id == Id))
                            {
                                MessageBox.Show("Введите значение Id, существующее в списке.");
                                return;
                            }

                            RequestWasLaunched = true;
                            MessageBox.Show("Сейчас будет выполнен запрос на получение данных. Не закрывайте приложение в течении 7 секунд и не переходите по следующим окнам.");

                            var request = await RequestsLogic.DeleteEntity(Id);
                            if (!request.Success)
                            {
                                MessageBox.Show("Запрос к базе данных на удаление указанного элемента не прошёл. \nОшибка:\n" + request.Message );
                                RequestWasLaunched = false;
                                return;
                            }

                            var JSONrequest = await RequestsLogic.GetAll();
                            if (request == null || String.IsNullOrEmpty(JSONrequest))
                            {
                                MessageBox.Show("Запрос к базе данных на получение всего списка объектов не прошёл.");
                                RequestWasLaunched = false;
                                return;
                            }

                            var data = UploadDataConverter.ConvertJSONToUploadList(JSONrequest);
                            AppData.DataList = data;
                            UploadDatas = AppData.DataList;

                            RequestWasLaunched = false;
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
                            AppData.DataList.Clear();
                            WindowsObjects.DataBaseOperatorWindow.Close();
                            WindowsObjects.DataBaseOperatorWindow = null;
                        }
                    }
                );
            }
        }

        #endregion

        private bool RequestWasLaunched = false;
    }
}