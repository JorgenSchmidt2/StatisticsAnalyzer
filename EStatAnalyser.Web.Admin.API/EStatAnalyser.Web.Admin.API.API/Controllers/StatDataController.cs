using EStatAnalyser.Web.Admin.API.Core.Entities;
using EStatAnalyser.Web.Admin.API.Core.Interfaces.ServiceInterfaces;
using EStatAnalyser.Web.Admin.API.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace EStatAnalyser.Web.Admin.API.API.Controllers
{

    // Route определяет по какому адресу приложение будет принимать запросы
    // Produces определяет в каком формате сообщения будут приниматься

    [ApiController] 
    [Route("/api/[controller]")]
    [Produces("application/json")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class StatDataController
    {
        private IDataService _service;

        public StatDataController(IDataService service)
        {
            _service = service;
        }

        /// <summary>
        /// Получает список имеющихся в базе данных записей из таблицы DATAS без включения ассоциированных с ними записей из VALUES
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     https://localhost:44340/api/StatData/get-all
        ///
        /// </remarks>
        /// <response code="200"> Получен и отправлен список объектов из таблицы DATAS </response>
        /// <response code="400"> Получен некорректный запрос (JSON) </response>
        /// <response code="500"> Внутренняя ошибка сервера </response>
        [HttpGet("get-all")]
        public async Task<BaseResponse> GetAll()
        {
            BaseResponse response = await _service.GetAll();
            return response;
        }

        /// <summary>
        /// Получает конкретную запись из таблицы DATAS с включением ассоциированных с ней записей из VALUES
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     https://localhost:44340/api/StatData/get-by-id/1
        ///     где 1 - id объекта, информацию по которому необходимо получить
        ///     
        /// </remarks>
        /// <response code="200"> Получены и отправлены данные по конкретному объекту </response>
        /// <response code="400"> Получен некорректный запрос (JSON) </response>
        /// <response code="500"> Внутренняя ошибка сервера </response>
        [HttpGet("get-by-id/{Id}")]
        public async Task<BaseResponse> GetById(int Id)
        {
            BaseResponse response = await _service.GetByID(Id);
            return response;
        }

        /// <summary>
        /// Загружает полученные данные в БД
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     https://localhost:44340/api/StatData/add-data
        ///     
        /// </remarks>
        /// <response code="200"> Данные загружены в базу данных, ошибок не обнаружено </response>
        /// <response code="400"> Получен некорректный запрос (JSON) </response>
        /// <response code="500"> Внутренняя ошибка сервера </response>
        [HttpPost("add-data")]
        public async Task<BaseResponse> AddData([FromBody] Data data)
        {
            BaseResponse response = await _service.AddEntity(data);
            return response;
        }

        /// <summary>
        /// Удаляет данные из БД по ID
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     https://localhost:44340/api/StatData/delete-data/1
        ///     где 1 - id объекта, информацию по которому необходимо получить
        ///     
        /// </remarks>
        /// <response code="200"> Данные по конкретному объекту удалены из БД </response>
        /// <response code="400"> Получен некорректный запрос (JSON) </response>
        /// <response code="500"> Внутренняя ошибка сервера </response>
        [HttpDelete("delete-data/{Id}")]
        public async Task<BaseResponse> DeleteData(int Id)
        {
            BaseResponse response = await _service.DeleteEntity(Id);
            return response;
        }

        /// <summary>
        /// Обновляет данные исходя из полученного запроса (наличие ID обязательно)
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     https://localhost:44340/api/StatData/update-data
        ///     
        /// </remarks>
        /// <response code="200"> Данные по конкретному объекту обновлены </response>
        /// <response code="400"> Получен некорректный запрос (JSON) </response>
        /// <response code="500"> Внутренняя ошибка сервера </response>
        [HttpPut("update-data")]
        public async Task<BaseResponse> UpdateData([FromBody] Data data)
        {
            BaseResponse response = await _service.UpdateEntity(data);
            return response;
        }
    }
}