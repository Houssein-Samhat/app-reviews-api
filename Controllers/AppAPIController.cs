using Microsoft.AspNetCore.Mvc;

using Apps_Review_Api.Models;
using Apps_Review_Api.Services;
using Microsoft.IdentityModel.Tokens;
using Apps_Review_Api.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace Apps_Review_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AppAPIController : ControllerBase
    {
        private readonly GenreRepository _genresServices;
        private readonly GetAggregatedRankService _getAggregatedRankService;
        private readonly SingleAppDetailsRepository _singleAppDetailsService;

        public AppAPIController(GenreRepository genresServices, GetAggregatedRankService getAppsService, SingleAppDetailsRepository singleAppDetailsService)
        {
            this._genresServices = genresServices;
            _getAggregatedRankService = getAppsService;
            _singleAppDetailsService = singleAppDetailsService;
        }
        
        [HttpGet("genres")]
        public async Task<IActionResult> getGenresAsync(string store,string country,string date)
        {
            Console.WriteLine("Genres Called------");
            GenreReqBinding details = new GenreReqBinding(store,country,date);

            List<Genre> genres = await _genresServices.GetAllGenresAsync(details);
            if (genres.IsNullOrEmpty())
            {
                return BadRequest("Error");
            }
            else
            {
                return Ok(genres);
            }
        }

        [HttpGet("AggregatedRanks")]

        public async Task<IActionResult> getAggregatedRanks(string country,string store,string date)
        {
            AppsReqBinding appsReqBinding = new AppsReqBinding(store,country,date);
            List<AggregatedRankModel> aggregatedRanks =await _getAggregatedRankService.getAggregatedRanks(appsReqBinding);
            if (aggregatedRanks.IsNullOrEmpty())
            {
                return BadRequest("Empty List");
            }
            else
            {
                return Ok(aggregatedRanks);
            }
        }

        [HttpGet("apps")]
        public async Task<IActionResult> getGenresApps(string genre_id,string store,string country,string date)
        {
            Console.WriteLine("APPS Called");
            AppsReqBinding appsReqBinding = new AppsReqBinding(store,country,date);
            List<AppDetails> result = await _singleAppDetailsService.getAllApps(genre_id,country,date,store);
            if (result.IsNullOrEmpty())
            {
                return BadRequest("Error cant get data");
            }

            return Ok(result);

        }

        [HttpGet("app")]
        public async Task<IActionResult> getSingleApp(string appId,string store,string country)
        {
            AppDetails app = await _singleAppDetailsService.getSingleApp(appId,store,country);
            if(app == null)
            {
                return BadRequest("Error Cant Get APPs");
            }
            return Ok(app);
        }

        }
}
