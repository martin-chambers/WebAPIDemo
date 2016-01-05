using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;


namespace WebApiDemo.Models
{
    public interface IValuesRepository
    {
        Task<IEnumerable<Value>> GetAllValuesAsync();
        Task<Value> GetValueAsync(string Id);
        Task AddValueAsync(Value item);
        bool RemoveValue(string Id);
        bool UpdateValue(string Id, Value item);
    }
}
