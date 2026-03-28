using AutoMapper;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RestaurantManagement.CatalogMicroservice.Dtos.OutcomeDtos;
using RestaurantManagement.CatalogMicroservice.Entities.InComes;
using RestaurantManagement.CatalogMicroservice.Entities.Outcomes;
using RestaurantManagement.CatalogMicroservice.Settings;

namespace RestaurantManagement.CatalogMicroservice.Services.OutComeService
{
    public class OutcomeService : IOutcomeService
    {

        private readonly IMongoCollection<Outcome> _collection;
        private readonly IMapper _mapper;

        public OutcomeService(
        IMongoClient mongoClient,
        IOptions<DatabaseSettings> databaseSettings,
        IMapper mapper)
        {
            var database = mongoClient.GetDatabase(
                databaseSettings.Value.DatabaseName);

            _collection = database.GetCollection<Outcome>(
                databaseSettings.Value.OutcomeCollectionName);

            _mapper = mapper;
        }

        public async Task CreateOutcomeDto(CreateOutcomeDto createOutcomeDto)
        {
           await _collection.InsertOneAsync(_mapper.Map<Outcome>(createOutcomeDto));
        }

        public async Task DeleteOutcomeDto(string id)
        {
            await _collection.DeleteOneAsync(outcome => outcome.OutcomeId == id);
        }

        public async Task<List<ResultOutcomeDto>> GetAllOutcomes()
        {
           var results = await _collection.Find(outcome => true).ToListAsync();
            return _mapper.Map<List<ResultOutcomeDto>>(results);
        }

        public async Task<GetByIdOutcomeDto> GetByIdAbotDto(string id)
        {
            var result = await _collection.Find(outcome => outcome.OutcomeId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdOutcomeDto>(result);
        }

        public async Task<List<ResultOutcomeDto>> GetOutcomesByShiftAsync(string shift)
        {
            // Veritabanı seviyesinde sadece ilgili vardiyaya ait giderleri getirir
            var values = await _collection.Find(x => x.ShiftType == shift).ToListAsync();
            return _mapper.Map<List<ResultOutcomeDto>>(values);
        }

        public async Task UpdateOutcomeDto(UpdateOutComeDto updateOutcomeDto)
        {
            await _collection.ReplaceOneAsync(outcome => outcome.OutcomeId == updateOutcomeDto.OutcomeId, _mapper.Map<Outcome>(updateOutcomeDto));
        }
    }
}
