using CMapp_Backend.Models;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace AfriLearnBackend.Services
{
    public class BlobStorageService
    {
        private IConfiguration Configuration { get;  }
        public string GetCredentials()
        {
            var reaiotConfiguration =  Configuration.GetSection("BlobStorageDetails");
            var reaiotCredentials = reaiotConfiguration.Get<BlobStorageCredentials>();
            var connectionString = Encoding.ASCII.GetBytes(reaiotCredentials.ConnectionString1).ToString();
            var key1 = Encoding.ASCII.GetBytes(reaiotCredentials.Key1).ToString();
            var credentials = new BlobStorageCredentials()
            {
                ConnectionString1 = connectionString, 
                Key1 = key1 
            };
            return credentials.ConnectionString1;
        }
    }
}
