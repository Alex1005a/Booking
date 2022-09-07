using Microsoft.eShopOnContainers.Services.Ordering.Domain.Seedwork;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HotelSevice.Infrastructure.MongoRepository
{
    public class MongoContext : IUnitOfWork
    {
        private IMongoDatabase Database { get; set; }
        private MongoClient MongoClient { get; set; }
        private readonly List<Func<Task>> _commands;
        private IClientSessionHandle Session { get; set; }
        public MongoContext()
        {
            _commands = new List<Func<Task>>();

            MongoClient = new MongoClient("mongodb://localhost:27017");

            Database = MongoClient.GetDatabase("Hotels");
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            if (MongoClient.Cluster.Description.Type == MongoDB.Driver.Core.Clusters.ClusterType.Standalone)
            {
                await Task.WhenAll(_commands.Select(c => c()));
            }

            else
            {
                using (Session = await MongoClient.StartSessionAsync())
                {
                    Session.StartTransaction();

                    await Task.WhenAll(_commands.Select(c => c()));

                    await Session.CommitTransactionAsync();
                }

            }

            int count = _commands.Count();
            _commands.Clear();

            return count;
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            if(!Database.ListCollectionNames().ToList().Contains(name))
            {
                Database.CreateCollection(name);
            }

            return Database.GetCollection<T>(name);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public void AddCommand(Func<Task> func)
        {
            _commands.Add(func);
        }
    }
}
