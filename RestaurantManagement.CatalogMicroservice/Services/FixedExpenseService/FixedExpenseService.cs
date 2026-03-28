using AutoMapper;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RestaurantManagement.CatalogMicroservice.Dtos.FixedExpenseDto;
using RestaurantManagement.CatalogMicroservice.Entities.FixedExpense;
using RestaurantManagement.CatalogMicroservice.Entities.InComes;
using RestaurantManagement.CatalogMicroservice.Settings;

namespace RestaurantManagement.CatalogMicroservice.Services.FixedExpenseService
{
    public class FixedExpenseService : IFixedExpenseService
    {

        private readonly IMongoCollection<FixedExpense> _collection;
        private readonly IMapper _mapper;

        public FixedExpenseService(
        IMongoClient mongoClient,
        IOptions<DatabaseSettings> databaseSettings,
        IMapper mapper)
        {
            var database = mongoClient.GetDatabase(
                databaseSettings.Value.DatabaseName);

            _collection = database.GetCollection<FixedExpense>(
                databaseSettings.Value.FixedExpenseCollectionName);

            _mapper = mapper;
        }

        public async Task CreateFixedExpenseDto(CreateFixedExpenseDto dto)
        {
            await _collection.InsertOneAsync(_mapper.Map<FixedExpense>(dto));
        }

        public async Task DeleteFixedExpenseDto(string id)
        {
            await _collection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<List<ResultFixedExpenseDto>> GetAllFixedExpenseDto()
        {
            var result = await _collection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultFixedExpenseDto>>(result);
        }

        public async Task<List<ResultFixedExpenseDto>> GetFixedExpensesByShiftAsync(string shift)
        {
            // MongoDB veya SQL sorgusuyla sadece ilgili vardiyayı getirir
            var values = await _collection.Find(x => x.ShiftType == shift).ToListAsync();
            return _mapper.Map<List<ResultFixedExpenseDto>>(values);
        }

        public async Task<UpdateFixedExpensedto> GetByIdFixedExpenseDto(string id)
        {
            var result = await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<UpdateFixedExpensedto>(result);
        }

        public async Task UpdateFixedExpenseDto(UpdateFixedExpensedto dto)
        {
            await _collection.ReplaceOneAsync(x => x.Id == dto.Id, _mapper.Map<FixedExpense>(dto));
        }
    }
}
