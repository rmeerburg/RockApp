using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using rock_app.Models;

namespace rock_app.Services
{
    public class ExternalDataImportService : IDataImportService
    {
        private readonly IDataImportService _importService;
        private readonly IConfiguration _config;
        private string externalHost => _config["ImportOptions:ImportHost"];
        
        public ExternalDataImportService(DataImportService importService, IConfiguration config)
        {
            _importService = importService;
            _config = config;
        }

        public async Task<ImportResult> ImportArtists(IEnumerable<Artist> artists) => await _importService.ImportArtists(await ImportData<IEnumerable<Artist>>(_config["ImportOptions:ArtistImportPath"]));

        public async Task<ImportResult> ImportSongs(IEnumerable<ImportSong> songs) => await _importService.ImportSongs(await ImportData<IEnumerable<ImportSong>>(_config["ImportOptions:SongsImportPath"]));

        private async Task<TResponse> ImportData<TResponse>(string path)
        {
            if (Uri.TryCreate(new Uri(externalHost), path, out var uri))
                return Newtonsoft.Json.JsonConvert.DeserializeObject<TResponse>(await GetResponse(uri));
            else
                throw new Exception($"unable to create uri from parts: [{externalHost}, {path}] ");
        }

        private async Task<string> GetResponse(Uri uri)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}