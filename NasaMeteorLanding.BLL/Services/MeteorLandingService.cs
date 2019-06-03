using AutoMapper;
using NasaMeteorLanding.BLL.DTO;
using NasaMeteorLanding.BLL.Infrastructure;
using NasaMeteorLanding.DAL.Models;
using NasaMeteorLanding.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasaMeteorLanding.BLL.Services
{
    public class MeteorLandingService
    {
        private MeteorLandingRepository repo = new MeteorLandingRepository();

        public async Task<IEnumerable<MeteorLandingDTO>> GetMeteorLandings(int? year, double? mass)
        {
            IEnumerable<MeteorLanding> meteors;

            if (year == null && mass == null)
            {
                meteors = await repo.GetAll();
            }
            else if (year != null && mass == null)
            {
                meteors = await repo.GetAll(year.Value);
            }
            else if (year == null && mass != null)
            {
                meteors = await repo.GetAll(mass.Value);
            }
            else
            {
                meteors = await repo.GetAll(year.Value, mass.Value);
            }


            if (meteors == null)
                throw new ValidationException("Meteors not found", "");

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<MeteorLanding, MeteorLandingDTO>()
                .ForMember("Year", opt => opt.MapFrom(src => src.Date.Year))
                .ForMember("Longitude", opt => opt.MapFrom(src => src.Reclong))
                .ForMember("Latitude", opt => opt.MapFrom(src => src.Reclat))
                ).CreateMapper();
            return mapper.Map<IEnumerable<MeteorLanding>, List<MeteorLandingDTO>>(meteors.ToList());
        }
    }
}
