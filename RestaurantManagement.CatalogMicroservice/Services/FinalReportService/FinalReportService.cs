using AutoMapper;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RestaurantManagement.CatalogMicroservice.Dtos.FinalReportDtos;
using RestaurantManagement.CatalogMicroservice.Dtos.FixedExpenseDto;
using RestaurantManagement.CatalogMicroservice.Dtos.IncomeDtos;
using RestaurantManagement.CatalogMicroservice.Entities.DailyReport;
using RestaurantManagement.CatalogMicroservice.Entities.FinalReport;
using RestaurantManagement.CatalogMicroservice.Entities.FixedExpense;
using RestaurantManagement.CatalogMicroservice.Settings;

namespace RestaurantManagement.CatalogMicroservice.Services.FinalReportService
{
    public class FinalReportService : IFinalReportService
    {

        private readonly IMongoCollection<FinalReport> _collection;
        private readonly IMapper _mapper;

        public FinalReportService(
        IMongoClient mongoClient,
        IOptions<DatabaseSettings> databaseSettings,
        IMapper mapper)
        {
            var database = mongoClient.GetDatabase(
                databaseSettings.Value.DatabaseName);

            _collection = database.GetCollection<FinalReport>(
                databaseSettings.Value.FinalReportCollectionName);

            _mapper = mapper;
        }

        public async Task CreateFinalReport(CreateFinalReportDto dto)
        {
            await _collection.InsertOneAsync(_mapper.Map<FinalReport>(dto));

        }

        public async Task DeleteFinalReport(string id)
        {
            await _collection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<List<ResultFinalReportDto>> GetAllFinalReports()
        {
            var result = await _collection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultFinalReportDto>>(result);
        }

        public async Task<List<ResultFinalReportDto>> GetFinalReportsByShiftAsync(string shift)
        {
            var values = await _collection.Find(x => x.ShiftType == shift).ToListAsync();
            return _mapper.Map<List<ResultFinalReportDto>>(values);
        }

        public async Task<GetByIdFinalReportDto> GetByIdFinalReport(string id)
        {
            var result = _collection.Find(x => x.Id == id).FirstOrDefault();
            return (_mapper.Map<GetByIdFinalReportDto>(result));
        }
    }
}
