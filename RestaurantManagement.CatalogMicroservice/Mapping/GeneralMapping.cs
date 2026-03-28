using AutoMapper;
using MongoDB.Driver.Search;
using RestaurantManagement.CatalogMicroservice.Dtos.DailyReport;
using RestaurantManagement.CatalogMicroservice.Dtos.FinalReportDtos;
using RestaurantManagement.CatalogMicroservice.Dtos.FixedExpenseDto;
using RestaurantManagement.CatalogMicroservice.Dtos.IncomeDtos;
using RestaurantManagement.CatalogMicroservice.Dtos.OutcomeDtos;
using RestaurantManagement.CatalogMicroservice.Entities.DailyReport;
using RestaurantManagement.CatalogMicroservice.Entities.FinalReport;
using RestaurantManagement.CatalogMicroservice.Entities.FixedExpense;
using RestaurantManagement.CatalogMicroservice.Entities.InComes;
using RestaurantManagement.CatalogMicroservice.Entities.Outcomes;

namespace RestaurantManagement.CatalogMicroservice.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Income, ResultIncomeDto>().ReverseMap();
            CreateMap<Income, CreateInComeDto>().ReverseMap();
            CreateMap<Income, UpdateInComeDtos>().ReverseMap();
            CreateMap<Income, GetByIdIncomeDto>().ReverseMap();

            CreateMap<Outcome, UpdateOutComeDto>().ReverseMap();
            CreateMap<Outcome, CreateOutcomeDto>().ReverseMap();
            CreateMap<Outcome, GetByIdOutcomeDto>().ReverseMap();
            CreateMap<Outcome, ResultOutcomeDto>().ReverseMap();

            CreateMap<FixedExpense, ResultFixedExpenseDto>().ReverseMap();
            CreateMap<FixedExpense, CreateFixedExpenseDto>().ReverseMap();
            CreateMap<FixedExpense, UpdateFixedExpensedto>().ReverseMap();

            CreateMap<DailyReport, ResultDailyReportDto>().ReverseMap();
            CreateMap<DailyReport, CreateDailyReportDto>().ReverseMap();

            CreateMap<FinalReport, ResultFinalReportDto>().ReverseMap();
            CreateMap<FinalReport, CreateFinalReportDto>().ReverseMap();
            CreateMap<FinalReport, GetByIdFinalReportDto>().ReverseMap();

        }
    }
}
