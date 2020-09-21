using Microsoft.eShopOnContainers.Services.Ordering.Domain.Seedwork;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var commandTasks = _commands.Select(c => c());            

            if (MongoClient.Cluster.Description.Type == MongoDB.Driver.Core.Clusters.ClusterType.Standalone)
            {
                await Task.WhenAll(commandTasks);
            }

            else
            {
                using (Session = await MongoClient.StartSessionAsync())
                {
                    Session.StartTransaction();

                    await Task.WhenAll(commandTasks);

                    await Session.CommitTransactionAsync();
                }

            }

            _commands.Clear();

            return commandTasks.Count();
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
