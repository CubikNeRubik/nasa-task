using NasaMeteorLanding.DAL.Repositories;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace NasaMeteorTest
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            var repo = new MeteorLandingRepository();
            var res = await repo.GetAll();
            Console.ReadLine();
        }
    }
}
