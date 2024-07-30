using EStatAnalyser.Web.Admin.API.Core.Entities;
using EStatAnalyser.Web.Admin.API.Core.Responses;

namespace EStatAnalyser.Web.Admin.API.Core.Interfaces.ServiceInterfaces
{
    public interface IDataService
    {
        /// <summary>
        /// Возвращает ответ сервера, содержащий список всех элементов и статус выполнения
        /// </summary>
        public Task<BaseResponse> GetAll();
        /// <summary>
        /// Возвращает ответ сервера, содержащий данные целевого объекта + его данные и статус выполнения
        /// </summary>
        public Task<BaseResponse> GetByID(int Id);
        /// <summary>
        /// Возвращает ответ сервера об успешности добавления данных в БД
        /// </summary>
        public Task<BaseResponse> AddEntity(Data data);
        /// <summary>
        /// Возвращает ответ сервера об успешности обновления данных в БД
        /// </summary>
        public Task<BaseResponse> UpdateEntity(Data data);
        /// <summary>
        /// Возвращает ответ сервера об успешности удаления данных из БД
        /// </summary>
        public Task<BaseResponse> DeleteEntity(int Id);
    }
}