using MongoDB.Driver;
using MongoDbGenericRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportWebApp.Repositories
{
    public class RepositoryBase<T> : BaseMongoRepository, IRepositoryBase<T> where T : class
    {
        private readonly IMongoClient _client;

        public RepositoryBase(IMongoClient client) : base(client.GetDatabase("SpacePlanets"))
        {
            _client = client;
        }

    }
}
