using EStatAnalyser.Web.User.Desktop.Core.Entities.DataEntities;
using EStatAnalyser.Web.User.Desktop.Model.MessageService.MessageClasses;
using EStatAnalyser.Web.User.Desktop.Model.ValidateService;

namespace EStatAnalyser.Web.User.Desktop.Model.FileService
{
    public class GetDirectoryInformation
    {
        public static List<FileData> GetFileNamesFromFolder(string PathName, bool IsUploadFile)
        {
            try
            {
                var path = Environment.CurrentDirectory + "\\" + PathName;

                if (!Directory.Exists(path))
                {
                    throw new Exception("Папки " + path + " не существует.");
                }
                
                var fileList = Directory.GetFiles(path);
                if (fileList.Length == 0)
                {
                    var split = path.Split('\\');
                    throw new Exception("Папка " + split[split.Length - 1] + " пуста.");
                }

                var Result = new List<FileData>();
                int Counter = 1;
                foreach (var item in fileList)
                {
                    var content = ContentGetters.GetContentFromFile(item, false);
                    if (content != null && DataValidators.CheckFileContent(content, IsUploadFile))
                    {
                        var fileName = item.Split('\\');
                        Result.Add(new FileData { Id = Counter, FullPath = item, FileName = fileName[fileName.Length - 1] });
                        Counter++;
                    }
                }

                if (Result.Count == 0)
                {
                    MessageObjects.Sender.SendMessage("Пригодных для загруки файлов не обнаружено.");
                }

                return Result;
            }
            catch (Exception ex) 
            {
                MessageObjects.Sender.SendMessage("Ошибка: \n" + ex.Message);
                return new List<FileData>();
            }
        }
    }
}