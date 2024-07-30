using DataGenerator.Admin.Desktop.Core.Entities.DataEntities;

namespace DataGenerator.Admin.Desktop.Model.FileServices
{
    public class FileRecorder
    {
        public static void Record(Data data, string fileName)
        {
            string Content = data.FieldName_X + "\t" + data.FieldName_Y + "\t" + data.Type + "\t" + data.Description + "\t" + "false" + "\n";

            foreach (var item in data.Values)
            {
                Content += item.X.ToString() + "\t" + item.Y.ToString() + "\n";
            }

            string ActuallyPath = Environment.CurrentDirectory + @"\" + fileName + ".txt";
            using (StreamWriter stream = new StreamWriter(ActuallyPath))
            {
                stream.Write(Content);
                stream.Close();
            }
        }
    }
}