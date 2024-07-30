using EStatAnalyser.Web.Admin.API.Core.Enums;

namespace EStatAnalyser.Web.Admin.API.Core.Responses
{
    public class BaseResponse
    {
        /// <summary>
        /// Тело JSON файла, помечен как object для возможности передачи как ошибки, так и готовой структуры
        /// </summary>
        public object Body { get; set; }
        /// <summary>
        /// Статус исполнения запроса
        /// </summary>
        public StatusCodes Status { get; set; }
    }
}