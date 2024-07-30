using EStatAnalyser.Web.Admin.API.Core.Enums;

namespace EStatAnalyser.Web.Admin.API.Core.Responses
{
    public class RespositoryResponse
    {
        /// <summary>
        /// Сообщение о возможной ошибке
        /// </summary>
        public string? Message { get; set; }
        /// <summary>
        /// Статус выполнения
        /// </summary>
        public StatusCodes Status { get; set; }
    }
}