using NasaMeteorLanding.BLL.DTO;
using NasaMeteorLanding.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NasaMeteorTest
{
    class Program
    {
        static private MeteorLandingService service = new MeteorLandingService();

        static async Task Main(string[] args)
        {
            var data = await service.GetMeteorLandings(null, null);
            string html = "";

            List<MeteorLandingDTO> list = data.OrderBy(item => item.Id).Take(100).ToList();

            foreach (var meteor in list)
            {
                try
                {
                    string htmlString = string.Format($@"<tr>
                        <th> {meteor.Id} </th>
                        <th> {meteor.Name} </th>
                        <th> {meteor.Nametype} </th>
                        <th> {meteor.Recclass} </th>
                        <th> {meteor.Mass} </th>
                        <th> {meteor.Fall} </th>
                        <th> {meteor.Year} </th>
                        <th> {meteor.Latitude} </th>
                        <th> {meteor.Longitude} </th>
                    </tr>");

                    html += htmlString;
                }
                catch (Exception ex)
                {

                }
            }

            Console.WriteLine(html);
            Console.ReadLine();
        }
    }
}
