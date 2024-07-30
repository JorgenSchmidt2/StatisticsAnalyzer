using EStatAnalyser.Web.User.Desktop.Core.Entities.DataEntities;
using System.Collections.Generic;

namespace EStatAnalyser.Web.User.Desktop.UI.AppService
{
    public class AppData
    {
        public static UploadData AnalysisData = new UploadData();
        public static UploadData RewriteAnalysisData = new UploadData();
        public static DescriptionStatistics DescriptionStatistics = new DescriptionStatistics();
        public static List<UploadData> DataList = new List<UploadData>();
        public static List<FileData> FileData = new List<FileData>();
    }
}