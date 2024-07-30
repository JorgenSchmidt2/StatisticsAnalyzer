using EStatAnalyser.Web.Admin.API.Core.Entities;
using EStatAnalyser.Web.Admin.API.Core.Enums;
using EStatAnalyser.Web.Admin.API.Core.Interfaces.RepositoryInterfaces;
using EStatAnalyser.Web.Admin.API.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace EStatAnalyser.Web.Admin.API.DAL.Repositories
{
    public class DataRepository : IDataRepository
    {
        private readonly EfContext _dbcontext;

        public DataRepository(EfContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        // В запросах будет использован lazy loading
        // Его суть заключается в том, чтобы если в какой либо таблице
        // есть связанные сущности но в другой таблице ассоциированных 
        // с целевым объектом записей нет, то прогрузка данных не совершается
        // Для GetAll запроса он будет отключён, для остальных включён
        // Управление данной опцией (lazy loading) следует совершать с осторожностью

        public async Task<List<Data>> GetAll()
        {
            // Так как нет необходимости включать lazy loading для всего списка элементов
            // для данного запроса он будет отключён
            _dbcontext.ChangeTracker.LazyLoadingEnabled = false;
            var Entity = await _dbcontext.Set<Data>().ToListAsync();

            return Entity;
        }

        public async Task<Data> GetByID(int Id)
        {
            // Так как требуется прогрузка всех ассоциированных элементов, LL будет включён
            _dbcontext.ChangeTracker.LazyLoadingEnabled = true;
            var Entity = await _dbcontext.Set<Data>().FirstOrDefaultAsync(x => x.Id == Id);

            return Entity;
        }

        public async Task<RespositoryResponse> AddEntity(Data data)
        {
            _dbcontext.ChangeTracker.LazyLoadingEnabled = true;
            
            try
            {
                // Ре-формирование связанных данных
                var Related = new List<Value>();
                if (data.Values != null || data.Values.Count != 0)
                {
                    foreach (var item in data.Values)
                    {
                        var obj = new Value()
                        {
                            X = item.X, 
                            Y = item.Y
                        };
                        Related.Add(obj);
                    }
                }
                // Ре-формирование основных данных
                var Data = new Data()
                {
                    XFieldName = data.XFieldName,
                    YFieldName = data.YFieldName,
                    DataType = data.DataType,
                    Description = data.Description,
                    Values = Related,
                };
                // Запрос к БД и сохранение изменений
                // При любых подобных запросах прописывать SaveChangesAsync обязательно
                // в противном случае данные сохранены не будут
                await _dbcontext.DATAS.AddAsync(Data);
                await _dbcontext.SaveChangesAsync();
                return new RespositoryResponse
                {
                    Status = StatusCodes.OK
                };
            }
            catch (Exception ex)
            {
                return new RespositoryResponse
                {
                    Message = ex.Message,
                    Status = StatusCodes.InternalServerError
                };
            }
        }

        public async Task<RespositoryResponse> DeleteEntity(int Id)
        {
            _dbcontext.ChangeTracker.LazyLoadingEnabled = true;
            
            try
            {
                var data = await _dbcontext.DATAS.FindAsync(Id);
                if (data == null)
                {
                    throw new Exception("Input data not found.");
                }

                _dbcontext.DATAS.Remove(data);
                await _dbcontext.SaveChangesAsync();

                return new RespositoryResponse
                {
                    Status = StatusCodes.OK
                };
            }
            catch (Exception ex)
            {
                return new RespositoryResponse
                {
                    Message = ex.Message,
                    Status = StatusCodes.InternalServerError
                };
            }
        }

        public async Task<RespositoryResponse> UpdateEntity(Data data)
        {
            _dbcontext.ChangeTracker.LazyLoadingEnabled = true;

            try
            {
                var requestData = await _dbcontext.DATAS.FindAsync(data.Id);
                if (requestData == null)
                {
                    throw new Exception("Data for updating not found.");
                }

                requestData.XFieldName = data.XFieldName;
                requestData.YFieldName = data.YFieldName;
                requestData.DataType = data.DataType;
                requestData.Description = data.Description;
                requestData.WasAnalysed = data.WasAnalysed;
                requestData.Values = data.Values;

                _dbcontext.DATAS.Update(requestData);
                await _dbcontext.SaveChangesAsync();

                return new RespositoryResponse
                {
                    Status = StatusCodes.OK
                };
            }
            catch (Exception ex)
            {
                return new RespositoryResponse
                {
                    Message = ex.Message,
                    Status = StatusCodes.InternalServerError
                };
            }
        }
    }
}