using AutoMapper;
using WineReviewsApplication.Dto;
using WineReviewsApplication.Models;

namespace WineReviewsApplication.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            //Mapped for get requests
            CreateMap<Wine, WineDto>();
            CreateMap<WineType, WineTypeDto>();
            CreateMap<WineTypeDto, WineType>(); //mapping the other way, post req
            CreateMap<CountryDto, Country>(); //mapping for post req
            CreateMap<VineyardDto, Vineyard>(); 
            CreateMap<WineDto, Wine>();
            CreateMap<ReviewDto, Review>();
            CreateMap<ReviewerDto, Reviewer>();
            CreateMap<Country, CountryDto>();
            CreateMap<Vineyard, VineyardDto>();
            CreateMap<Review, ReviewDto>();
            CreateMap<Reviewer, ReviewerDto>();
        }
    }
}
