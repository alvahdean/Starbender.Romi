namespace Starbender.Romi.Web.Service.Controllers
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore.Extensions.Internal;

    using Starbender.Romi.Data;
    using Starbender.Romi.Data.Models;
    using Starbender.Romi.Web.Service.Models;


    public class RegistryController : ApiController
    {
        private RomiDbContext _context;

        public RegistryController(RomiDbContext context)
        {
            _context = context;
        }

        public async Task<RomiResponse<IEnumerable<RomiUser>>> Users()
        {
            var data = await _context.Users.ToAsyncEnumerable().ToList();
            return new RomiResponse<IEnumerable<RomiUser>>() { Data = data };
        }

        public async Task<RomiResponse<IEnumerable<RomiRole>>> Roles()
        {
            var data = await _context.Roles.ToAsyncEnumerable().ToList();
            return new RomiResponse<IEnumerable<RomiRole>>() { Data = data };
        }

        public async Task<RomiResponse<IEnumerable<RomiSettings>>> Settings()
        {
            var data = await _context.Settings.ToAsyncEnumerable().ToList();
            return new RomiResponse<IEnumerable<RomiSettings>>() { Data = data };
        }

        public async Task<RomiResponse<RomiSettings>> LocalSettings()
        {
            var settings = await _context.Settings.ToAsyncEnumerable().ToList();
            var data = settings.FirstOrDefault();
            return new RomiResponse<RomiSettings>() { Data = data };
        }

        public async Task<RomiResponse<IEnumerable<RegisteredDevice>>> Devices()
        {
            var data = await _context.Devices.ToAsyncEnumerable().ToList();
            return new RomiResponse<IEnumerable<RegisteredDevice>>() { Data = data };
        }

        public async Task<RomiResponse<IEnumerable<RegisteredInterface>>> Interfaces()
        {
            var data = await _context.Interfaces.ToAsyncEnumerable().ToList();
            return new RomiResponse<IEnumerable<RegisteredInterface>>() { Data = data };
        }
    }
}
