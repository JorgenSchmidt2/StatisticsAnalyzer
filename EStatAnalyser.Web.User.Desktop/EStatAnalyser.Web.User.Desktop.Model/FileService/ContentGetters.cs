﻿using EStatAnalyser.Web.User.Desktop.Model.MessageService.MessageClasses;

namespace EStatAnalyser.Web.User.Desktop.Model.FileService
{
    public class ContentGetters
    {
        public static string GetContentFromFile(string FilePath, bool GetMessage)
        {
            try
            {
                var Result = "";

                var file = new FileInfo(FilePath);
                if (!file.Exists || file.Length == 0)
                {
                    throw new Exception("Файл не существует, либо пустой: \n\n" + FilePath);
                }

                Result = File.ReadAllText(FilePath);

                return Result;
            }
            catch (Exception ex) 
            {
                if (GetMessage) { MessageObjects.Sender.SendMessage("Ошибка: \n" + ex.Message); }
                return null;
            }
        }
    }
}