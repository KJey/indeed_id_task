using indeed_id.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using indeed_id.Data;
using Microsoft.EntityFrameworkCore;

namespace indeed_id.Engine
{
    public class UserEngine : IUserEngine
    {
        private readonly AppContext _appContext;

        public UserEngine(AppContext appContext)
        {
            _appContext = appContext;
        }

        public async Task<User> Get(int id)
        {
            return await _appContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
