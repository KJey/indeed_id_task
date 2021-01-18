using System.Collections;
using System.Threading.Tasks;
using indeed_id.Models;

namespace indeed_id.Engine
{
    public interface IUserEngine
    {
        public Task<User> Get(int id);
    }
}