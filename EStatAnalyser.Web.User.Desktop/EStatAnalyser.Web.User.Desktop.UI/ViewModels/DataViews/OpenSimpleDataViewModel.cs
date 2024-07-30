using EStatAnalyser.Web.User.Desktop.Core.Configuration;
using EStatAnalyser.Web.User.Desktop.Core.Entities.DataEntities;
using EStatAnalyser.Web.User.Desktop.Model.ConverterService;
using EStatAnalyser.Web.User.Desktop.Model.FileService;
using EStatAnalyser.Web.User.Desktop.UI.AppService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace EStatAnalyser.Web.User.Desktop.UI.ViewModels.DataViews
{
    public class OpenSimpleDataViewModel : NotifyPropertyChanged
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

        #region

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

        public string? dataType;
        public string? DataType
        {
            get { return dataType; }
            set 
            { 
                dataType = value; 
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

        public Command Send
        {
            get
            {
                return new Command(
                    obj =>
                    {
                        if (FileDatas.Count == 0)
                        {
                            MessageBox.Show("В папке " + AppFolders.DataFiles + " не осталось файлов.");
                            return;
                        }
                        if (Id == 0 || Id > AppData.FileData.Count)
                        {
                            MessageBox.Show("Id должен быть больше 1 и не равняться ");
                            return;
                        }
                        if (String.IsNullOrEmpty(XFieldName))
                        {
                            MessageBox.Show("Введите обозначение поля Х.");
                            return;
                        }
                        if (String.IsNullOrEmpty(YFieldName))
                        {
                            MessageBox.Show("Введите обозначение поля Y.");
                            return;
                        }
                        if (String.IsNullOrEmpty(DataType))
                        {
                            MessageBox.Show("Введите к какому типу относятся данные.");
                            return;
                        }
                        if (String.IsNullOrEmpty(Description))
                        {
                            MessageBox.Show("Введите описание выборки данных.");
                            return;
                        }

                        var InputContent = ContentGetters.GetContentFromFile(FileDatas[Id - 1].FullPath, true);
                        if (InputContent == null) { return; }

                        var SimpleData = Converters.ConvertContentToSimpleData(InputContent);
                        if (SimpleData == null) { return; }

                        var UploadData = Converters.ConvertSimpleToUploadData
                            (
                                XFieldName, 
                                YFieldName, 
                                DataType,
                                Description,
                                SimpleData
                            );
                        if (UploadData == null) { return; }

                        var OutputContent = Converters.ConvertUploadDataToString(UploadData);
                        if (OutputContent == null) { return; }

                        var FormatedFilePath = Environment.CurrentDirectory
                            + "\\" + AppFolders.FormatFiles
                            + "\\" + FileDatas[Id - 1].FileName.Split('.')[0] + "-formatted.txt";
                        ContentWriters.WriteContentToFile(FormatedFilePath, OutputContent, true);

                        var CurrentFilePath = Environment.CurrentDirectory
                            + "\\" + AppFolders.DataFiles
                            + "\\" + FileDatas[Id - 1].FileName;
                        var TargetFilePath = Environment.CurrentDirectory
                            + "\\" + AppFolders.DataFiles
                            + "\\Used"
                            + "\\" + FileDatas[Id - 1].FileName;
                        FileMovers.MoveFile(CurrentFilePath, TargetFilePath, true);
                        AppData.FileData = GetDirectoryInformation.GetFileNamesFromFolder(AppFolders.DataFiles, false);
                        FileDatas = AppData.FileData;
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
                        AppData.FileData.Clear();
                        WindowsObjects.OpenSimpleDataWindow.Close();
                        WindowsObjects.OpenSimpleDataWindow = null;
                    }
                );
            }
        }

        #endregion
    }
}