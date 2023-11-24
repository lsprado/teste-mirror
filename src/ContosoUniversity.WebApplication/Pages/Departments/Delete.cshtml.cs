using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ContosoUniversity.WebApplication.Pages.Departments
{
    public class DeleteModel : PageModel
    {
        private readonly IHttpClientFactory client;
        private readonly TelemetryClient _telemetryClient;

        public DeleteModel(IHttpClientFactory client, TelemetryClient telemetryClient)
        {
            this.client = client;
            _telemetryClient = telemetryClient;
        }

        [BindProperty]
        public Models.APIViewModels.Department Department { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            //teste 02
            if (id == null)
            {
                return NotFound();
            }

            var response = await client.CreateClient("client").GetStringAsync("api/Departments/" + id);
            Department = JsonConvert.DeserializeObject<Models.APIViewModels.Department>(response);

            if (Department == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                // ERRO para mostrar no AppInsights
                //string teste = id.ToString() + "teste";
                //var response = await client.CreateClient("client").DeleteAsync("api/Departments/" + teste);

                var response = await client.CreateClient("client").DeleteAsync("api/Departments/" + id);

                if (response.IsSuccessStatusCode)
                    return RedirectToPage("./Index");
                else
                    return RedirectToPage("/Error");
            }
            catch (System.Exception ex)
            {
                //var properties = new Dictionary<string, string>
                //{
                //    ["Pagina"] = "Departaments"
                //};

                //var measurements = new Dictionary<string, string>
                //{
                //    ["Acao"] = "Delete"
                //};

                // Send the exception telemetry:
                _telemetryClient.TrackException(ex);
                return RedirectToPage("/Error");
            }
        }
    }
}