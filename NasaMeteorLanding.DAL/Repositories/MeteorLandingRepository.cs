using NasaMeteorLanding.DAL.DataSources;
using NasaMeteorLanding.DAL.Interfaces;
using NasaMeteorLanding.DAL.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NasaMeteorLanding.DAL.Repositories
{
    public class MeteorLandingRepository: IRepository<MeteorLanding>
    {
        private readonly MeteorLandingDataSource dataSource = MeteorLandingDataSource.getInstance();

        public async Task<IEnumerable<MeteorLanding>> GetAll()
        {
            return await dataSource.Data;
        }

        public async Task<IEnumerable<MeteorLanding>> GetAll(int year)
        {
            var data = await dataSource.Data;
            return data.Where(item => item.Date.Year == year);
        }

        public async Task<IEnumerable<MeteorLanding>> GetAll(double mass)
        {
            var data = await dataSource.Data;
            return data.Where(item => item.Mass == mass);
        }

        public async Task<IEnumerable<MeteorLanding>> GetAll(int year, double mass)
        {
            var data = await dataSource.Data;
            return data.Where(item => item.Date.Year == year && item.Mass == mass);
        }
    }
}
