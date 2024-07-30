using Newtonsoft.Json;

namespace EStatAnalyser.Web.User.Desktop.Core.Entities.DataEntities
{
    public class UploadData
    {
        public int? Id { get; set; }
        public string XFieldName { get; set; }
        public string YFieldName { get; set; }
        public string DataType { get; set; }
        public string Description { get; set; }
        public bool WasAnalysed { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public List<SimpleData>? Values { get; set; }
    }
}