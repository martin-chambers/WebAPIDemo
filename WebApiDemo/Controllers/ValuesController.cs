using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using WebApiDemo.Models;
using System.Threading.Tasks;

namespace WebApiDemo.Controllers
{
    public class ValuesController : ApiController
    {
        private readonly IValuesRepository repository;
        public ValuesController(ValuesRepository _repository)
        {
            repository = _repository;
        }

        public ValuesController() : this(new ValuesRepository()){}

        // GET api/values
        public async Task<IEnumerable<Value>> Get()
        {
            IEnumerable<Value> values = null; ;
            try
            {
                values = (IEnumerable<Value>)await repository.GetAllValuesAsync();
                return values;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return values;
            }
        }

        // GET api/values/568b9cbefbfd383c642a6dde/
        public async Task<Value> Get(string id)
        {
            Value rv = null;
            try
            {
                rv = await repository.GetValueAsync(id);
                return rv;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return rv;
            }            
        }

        // POST api/values/
        [System.Web.Http.HttpPost]
        public async Task<HttpResponseMessage> Insert([FromBody]Value value)
        {
            Task task = repository.AddValueAsync(value);
            try
            {
                await task;
                return Request.CreateResponse(HttpStatusCode.Created, value);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            
        }

        // PUT api/values/568b9cbefbfd383c642a6dde/
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
