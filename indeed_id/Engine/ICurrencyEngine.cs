using System.Threading.Tasks;

namespace indeed_id.Engine
{
    public interface ICurrencyEngine
    {
        public Task<decimal?> GetRate(string currency);
    }
}