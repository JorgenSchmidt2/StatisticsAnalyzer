using EStatAnalyser.Web.Admin.API.Core.Entities;
using EStatAnalyser.Web.Admin.API.Core.Responses;

namespace EStatAnalyser.Web.Admin.API.Core.Interfaces.RepositoryInterfaces
{
    public interface IDataRepository
    {
        /// <summary>
        /// Возвращает список всех объектов типа Data без включения приуроченных к объекту данных
        /// </summary>
        public Task<List<Data>> GetAll();
        /// <summary>
        /// Возвращает данные по конкретному объекту с включением приуроченных к этому объекту данных
        /// </summary>
        public Task<Data> GetByID(int Id);
        /// <summary>
        /// Содержит логику добавления данных на сервер
        /// </summary>
        public Task<RespositoryResponse> AddEntity(Data data);
        /// <summary>
        /// Обновляет данные по Id в соответствии с тем каково соответствующее значение id у объекта
        /// </summary>
        public Task<RespositoryResponse> UpdateEntity(Data data);
        /// <summary>
        /// Удаляет объект по Id
        /// </summary>
        public Task<RespositoryResponse> DeleteEntity(int Id);
    }
}