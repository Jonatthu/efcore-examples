using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

namespace RedisTestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {

		private readonly IMemoryCache memoryCache;
		private readonly IDistributedCache distributedCache;
		private readonly IEnumerable<string> savedUsers = new string[] { "Hello", "Hello1", "Hello2" };

		public ValuesController(IDistributedCache distributedCache, IMemoryCache memoryCache)
		{
			this.distributedCache = distributedCache;
			this.memoryCache = memoryCache;
		}

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {

			Stopwatch stopwatch = new Stopwatch();

			stopwatch.Start();

			var cacheKey = id.GetHashCode();

			//string existingValue = this.distributedCache.GetString(cacheKey.ToString());

			//if (existingValue != null)
			//{

			//	stopwatch.Stop();
			//	return $@"({stopwatch.ElapsedTicks}) Fetched from cache: {existingValue}";
			//}
			//else
			//{
			//	stopwatch.Stop();
			//	existingValue = string.Join(",", savedUsers);
			//	byte[] val = Encoding.UTF8.GetBytes(existingValue);
			//	this.distributedCache.Set(cacheKey.ToString(), val, new DistributedCacheEntryOptions {  });
			//	return $@"({stopwatch.ElapsedTicks}) Added to cache: {existingValue}";
			//}

			//stopwatch.Reset();

			if (this.memoryCache.TryGetValue(cacheKey, out string existingValue))
			{
				stopwatch.Stop();
				return $@"({stopwatch.ElapsedTicks}) Fetched from cache: {existingValue}";
			}
			else
			{
				existingValue = string.Join(",", savedUsers);
				this.memoryCache.Set(cacheKey, existingValue, new TimeSpan(0, 0, 5));
				stopwatch.Stop();
				return $@"({stopwatch.ElapsedTicks}) Added to cache: {existingValue}";
			}
		}

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
