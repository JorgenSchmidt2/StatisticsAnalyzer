using EStatAnalyser.Web.User.Desktop.Core.Entities.DataEntities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EStatAnalyser.Web.User.Desktop.NetworkUtils.Converters
{
    public class UploadDataConverter {

        private static char quo = '"';

        public static string ConvertUploadToJSON(UploadData data, bool ignoreId)
        {
            string Result = "{";

            if (data == null ) 
            {
                return null;
            }

            if (!ignoreId)
            {
                Result += quo + "id" + quo + ":" + data.Id + ",";
            }
            Result += quo + "xFieldName" + quo + ":" + quo + data.XFieldName + quo + ",";
            Result += quo + "yFieldName" + quo + ":" + quo + data.YFieldName + quo +  ",";
            Result += quo + "dataType" + quo + ":" + quo  + data.DataType + quo + ",";
            Result += quo + "description" + quo + ":" + quo +  data.Description + quo + ",";
            Result += quo + "wasAnalysed" + quo + ":" + data.WasAnalysed + ",";
            
            if (data != null && data.Values.Count != 0)
            {
                Result += quo + "values" + quo + ":[";
                int Counter = 0;
                foreach ( var item in data.Values )
                {
                    Result += "{";
                    if (!ignoreId)
                    {
                        Result += quo + "id" + quo + ":" + item.Id + ",";
                    }
                    Result += quo + "x" + quo + ":" + item.X.ToString().Replace(',','.') + ",";
                    Result += quo + "y" + quo + ":" + item.Y.ToString().Replace(',','.');
                    Result += "}";
                    if (Counter != data.Values.Count - 1)
                    {
                        Result += ",";
                    }
                    Counter++;
                }
                Result += "]";
            }

            Result += "}";
            return Result.Replace("False", "false").Replace("True", "true");
        }

        public static List<UploadData> ConvertJSONToUploadList(string data)
        {
            List<UploadData> Result = new List<UploadData>();

            var jsonObject = JObject.Parse(data);
            var uploadDataArray = jsonObject["body"].ToString();
            Result = JsonConvert.DeserializeObject<List<UploadData>>(uploadDataArray);

            return Result;
        }

        public static UploadData ConvertJSONToUpload(string data)
        {
            UploadData Result = new UploadData();
            Result.Values = new List<SimpleData>();

            var jsonObject = JObject.Parse(data);
            var uploadDataArray = jsonObject["body"].ToString();
            Result = JsonConvert.DeserializeObject<UploadData>(uploadDataArray);

            return Result;
        }

    }
}