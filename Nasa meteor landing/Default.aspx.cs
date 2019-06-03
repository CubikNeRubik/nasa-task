using NasaMeteorLanding.BLL.DTO;
using NasaMeteorLanding.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NasaMeteorLanding
{
    public partial class Default : System.Web.UI.Page
    {
        static private MeteorLandingService service = new MeteorLandingService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Add items like below example
                for( int i = DateTime.Now.Year; i > 861; i--)
                {
                    yearInput.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
            }
        }

        [WebMethod]
        public static IEnumerable<MeteorLandingDTO> Search(int? year, double? mass)
        {
            var data = service.GetMeteorLandings(year, mass).ConfigureAwait(false).GetAwaiter().GetResult();
            return data.OrderBy(item => item.Id).Take(100).ToList();
        }
    }
}