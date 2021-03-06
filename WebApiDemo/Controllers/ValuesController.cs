﻿using System;
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
        /// <summary>
        /// Get the values
        /// </summary>
        /// <remarks>The endpoint returns a Json representation of the object collection</remarks>
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
        /// <summary>
        /// Get a value by id
        /// </summary>
        /// <remarks>The endpoint returns a Json representation of the object identified by the SHA-1 Id</remarks>
        /// <param name="id">
        /// <Description>A SHA-1 string id</Description>
        /// </param>
        public async Task<Value> Get(string id)
        {
            Value rv = null;
            try
            {
                rv = await repository.GetValueAsync(id);
                return rv;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return rv;
            }
        }

        // POST api/values/ (+payload)
        /// <summary>
        /// Create a value
        /// </summary>
        /// <remarks>The endpoint returns a Json representation of the object created in the data source</remarks>
        /// <param name="value">
        /// <Description>A Json representation of a Value object not including the Id which is assigned on creation in the data store</Description>
        /// </param>
        /// <response code="201">Value created</response>
        [System.Web.Http.HttpPost]
        public async Task<HttpResponseMessage> Insert([FromBody]Value value)
        {
            if (value == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            Task task = repository.AddValueAsync(value);
            try
            {
                await task;
                return Request.CreateResponse(HttpStatusCode.Created, value);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        // PUT api/values/ (+payload)
        /// <summary>
        /// Update a value
        /// </summary>
        /// <remarks>The endpoint returns a Json representation of the object as updated in the data source</remarks>
        /// <param name="value">
        /// <Description>A Json representation of a Value object including the Id</Description>
        /// </param>
        /// <response code="200">Value updated</response>
        [System.Web.Http.HttpPut]
        public async Task<HttpResponseMessage> Update([FromBody]Value value)
        {
            Task<long> updateResult = repository.UpdateValue(value);            
            try
            {
                await updateResult;
                if(updateResult.Result < 1)
                {
                    Console.WriteLine("No documents matched update query");
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
                else
                {
                    if(updateResult.Result > 1)
                    {
                        // should not happen: Ids are unique
                        Console.WriteLine("More than one document matched update query");
                        return Request.CreateResponse(HttpStatusCode.InternalServerError);
                    }
                }
                return Request.CreateResponse(HttpStatusCode.OK, value);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        // DELETE api/values/568b9cbefbfd383c642a6dde/
        /// <summary>
        /// Delete a value
        /// </summary>
        /// <param name="id">
        /// <Description>A SHA-1 string id</Description>
        /// </param>
        /// <response code="204">Value deleted</response>
        [System.Web.Http.HttpDelete]
        public async Task<HttpResponseMessage> Delete(string id)
        { 
            if(id == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }           
            try
            {
                Task<long> deleteResult = repository.RemoveValue(id);
                await deleteResult;
                if (deleteResult.Result < 1)
                {
                    Console.WriteLine("No documents matched delete query");
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
                else
                {
                    if (deleteResult.Result > 1)
                    {
                        // should not happen: Ids are unique
                        Console.WriteLine("More than one document matched delete query");
                        return Request.CreateResponse(HttpStatusCode.InternalServerError);
                    }
                }
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}
