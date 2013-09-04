using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIMASH_Core.Services
{
    public interface ILocationService
    {

        bool HasUserOptedIn();

        void SaveLocationConsent(bool result);

        Task<string> GetCoordinatesAsync();

        Task<string> GetLocationAsync();
    }
}
