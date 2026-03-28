using AutoMapper;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RestaurantManagement.CatalogMicroservice.Dtos.IncomeDtos;
using RestaurantManagement.CatalogMicroservice.Entities.InComes;
using RestaurantManagement.CatalogMicroservice.Settings;

namespace RestaurantManagement.CatalogMicroservice.Services.IncomeService
{
    public class IncomeService : IIncomeService
    {
        private readonly IMongoCollection<Income> _collection;
        private readonly IMapper _mapper;

        public IncomeService(
        IMongoClient mongoClient,
        IOptions<DatabaseSettings> databaseSettings,
        IMapper mapper)
        {
            var database = mongoClient.GetDatabase(
                databaseSettings.Value.DatabaseName);

            _collection = database.GetCollection<Income>(
                databaseSettings.Value.InComeCollectionName);

            _mapper = mapper;
        }

        public async Task CreateIncomeDto(CreateInComeDto createIncomeDto)
        {
            await _collection.InsertOneAsync(_mapper.Map<Income>(createIncomeDto));
        }

        public async Task DeleteIncomeDto(string id)
        {
            await _collection.DeleteOneAsync(income => income.IncomeId == id);
        }

        public async Task<List<ResultIncomeDto>> GetAllIncomes()
        {
           var result = await _collection.Find(income => true).ToListAsync();
              return _mapper.Map<List<ResultIncomeDto>>(result);
        }

        public Task<GetByIdIncomeDto> GetByIdAbotDto(string id)
        {
           var result =  _collection.Find(income => income.IncomeId == id).FirstOrDefault();
            return Task.FromResult(_mapper.Map<GetByIdIncomeDto>(result));
        }

        public async Task<List<ResultIncomeDto>> GetIncomesByShiftAsync(string shift)
        {
            // Veritabanından (MongoDB/SQL) sadece o vardiyaya ait gelirleri filtreler
            var values = await _collection.Find(x => x.ShiftType == shift).ToListAsync();
            return _mapper.Map<List<ResultIncomeDto>>(values);
        }

        public async Task UpdateIncomeDto(UpdateInComeDtos updateIncomeDto)
        {
          await  _collection.ReplaceOneAsync(income => income.IncomeId == updateIncomeDto.IncomeId, _mapper.Map<Income>(updateIncomeDto));
        }
    }
}
