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
    class MeteorLandingService
    {
        private MeteorLandingRepository repo = new MeteorLandingRepository();

        public async Task<IEnumerable<MeteorLandingDTO>> GetMeteorLandings(int? year, double? mass)
        {
            IEnumerable<MeteorLanding> meteors;

            if (year == null && mass == null)
            {
                meteors = await repo.GetAll();
            }
            else if (year != null)
            {
                meteors = await repo.GetAll(year.Value);
            }
            else if (mass != null)
            {
                meteors = await repo.GetAll(mass.Value);
            }
            else
            {
                meteors = await repo.GetAll(year.Value, mass.Value);

                if (meteors == null)
                {
                    var meteorsByMass = await repo.GetAll(mass.Value);

                    //should attach this message too response or do this logic at UI
                    //throw new ValidationException("Meteors with that mass not found, jumping to first-year where there is a mass that fits the criteria", "");

                    var meteor = meteorsByMass.OrderBy(item => item.Date.Year).FirstOrDefault();
                    meteors = new List<MeteorLanding>() { meteor };
                }
            }


            if (meteors == null)
                throw new ValidationException("Meteors not found", "");

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<MeteorLanding, MeteorLandingDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<MeteorLanding>, List<MeteorLandingDTO>>(meteors.ToList());
        }
    }
}
