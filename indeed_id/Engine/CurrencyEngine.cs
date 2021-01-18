using SimpleSOAPClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;
using indeed_id.Engine.Integration;
using RestSharp;

namespace indeed_id.Engine
{
    public class CurrencyEngine : ICurrencyEngine
    {
        public async Task<decimal?> GetRate(string currency)
        {
            var client = new RestClient("https://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml");
            var request = new RestRequest(Method.GET);
            var response = await client.ExecuteAsync(request);
            Envelope result = new Envelope();
            XmlSerializer serializer = new XmlSerializer(typeof(Envelope));
            if (response.IsSuccessful)
            {
                using (TextReader reader = new StringReader(response.Content))
                {
                    result = (Envelope)serializer.Deserialize(reader);
                }

                return result.Cube.Cube1.Cube.FirstOrDefault(x=>x.currency==currency).rate;
            }

            return null;
        }
    }
}
