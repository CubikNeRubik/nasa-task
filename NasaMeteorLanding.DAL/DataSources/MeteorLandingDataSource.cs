using NasaMeteorLanding.DAL.Interfaces;
using NasaMeteorLanding.DAL.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace NasaMeteorLanding.DAL.DataSources
{
    public class MeteorLandingDataSource : IDataSource<MeteorLanding>
    {
        private static MeteorLandingDataSource instance;

        private readonly HttpClient client = new HttpClient();

        public Task<IEnumerable<MeteorLanding>> Data { get; private set; }

        private MeteorLandingDataSource()
        {
            Data = Task.Run(GetFreshData);
            // Fetch data each 5 minutes
            TimerCallback callback = new TimerCallback(UpdateData);
            Timer timer = new Timer(callback, null, 0, 5 * 60 * 1000);
        }

        public static MeteorLandingDataSource getInstance()
        {
            if (instance == null)
                instance = new MeteorLandingDataSource();
            return instance;
        }

        private void UpdateData(object obj)
        {
            Data = Task.Run(GetFreshData);
        }

        // return base lins of sources (for now only one source is available)
        private string GetBaseLink() => "https://data.nasa.gov/resource/gh4g-9sfh.json";

        private async Task<string> GetAndRetryOnError(string query)
        {
            var attemps = 0;
            var maxAttemps = 3;

            while (attemps < maxAttemps)
            {
                try
                {
                    var rawData = await client.GetStringAsync($"{GetBaseLink()}?{query}");
                    return rawData;
                }
                catch (Exception e)
                {
                    attemps++;
                }
            }

            return null;
        }

        private async Task<int> GetRecordCount()
        {
            var rawRecordCount = await GetAndRetryOnError("$select=count(*)");
            var count = JArray.Parse(rawRecordCount)[0]["count"];
            return Convert.ToInt32(count);
        }

        private async Task GetRecords(int offset, ConcurrentBag<MeteorLanding> bag)
        {
            var rawData = await GetAndRetryOnError($"$offset={offset}");
            var data = JsonConvert.DeserializeObject<List<MeteorLanding>>(rawData);
            data.ForEach(item => bag.Add(item));
        }

        private async Task<IEnumerable<MeteorLanding>> GetFreshData()
        {
            try {
                var recordRequests = new List<Task>();
                var pageSize = 1000;
                var newData = new ConcurrentBag<MeteorLanding>();

                var recordCount = await GetRecordCount();

                // make few request because API return just 1000 records per page
                for (int i = 0; i <= recordCount / pageSize; i++)
                {
                    recordRequests.Add(GetRecords(i * pageSize, newData));
                }

                await Task.WhenAll(recordRequests);
                return newData;
            }
            catch (Exception e)
            {
                // use old data if source unavailable
                return await Data;
            }
        }
    }
}
