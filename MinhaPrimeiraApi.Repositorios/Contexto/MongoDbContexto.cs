using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhaPrimeiraApi.Repositorios.Contexto
{
    public class MongoDbContexto : IMongoDbContexto
    {
        private IMongoDatabase Database { get; set; }
        public IClientSessionHandle Session { get; set; }
        public MongoClient MongoClient { get; set; }

        private IConfiguration _configuration;
        private readonly List<Func<Task>> _commands;

        public MongoDbContexto(IConfiguration configuration)
        {
            _configuration = configuration;
            _commands = new List<Func<Task>>();
        }

        public async Task<int> SalvarAlteracoes()
        {
            ConfigureMongo();

            using (Session = await MongoClient.StartSessionAsync())
            {
                Session.StartTransaction();

                var commandTasks = _commands.Select(func => func());

                await Task.WhenAll(commandTasks);

                await Session.CommitTransactionAsync();
            }

            return _commands.Count;
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            ConfigureMongo();

            return Database.GetCollection<T>(name);
        }

        public void AddCommand(Func<Task> func) => _commands.Add(func);

        public void Dispose()
        {
            Session?.Dispose();
            GC.SuppressFinalize(this);
        }

        private void ConfigureMongo()
        {
            if (MongoClient != default)
                return;

            MongoClient = new MongoClient(_configuration["MongoSettings:Connection"]);
            Database = MongoClient.GetDatabase(_configuration["MongoSettings:DatabaseName"]);
        }
    }
}