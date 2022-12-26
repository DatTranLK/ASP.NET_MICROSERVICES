using Entities;
using MongoDB.Driver;
using System;

namespace DataAccess
{
    public interface ICatalogContext
    {
        IMongoCollection<Product> Products { get; }
    }
}
