using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Web.UI.Models;

namespace Web.UI.Services
{
    public class TahtaService
    {
        private readonly IMongoCollection<Tahta> _tahtaCollection;

        public TahtaService(
            IOptions<SatrancDatabaseSettings> bookStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                bookStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                bookStoreDatabaseSettings.Value.DatabaseName);

            _tahtaCollection = mongoDatabase.GetCollection<Tahta>(
                bookStoreDatabaseSettings.Value.TahtaCollectionName);
        }

        public async Task<List<Tahta>> GetAsync() =>
            await _tahtaCollection.Find(_ => true).ToListAsync();

        public async Task<Tahta?> GetAsync(string id) =>
            await _tahtaCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Tahta newBook) =>
            await _tahtaCollection.InsertOneAsync(newBook);

        public async Task UpdateAsync(string id, Tahta updatedBook) =>
            await _tahtaCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);

        public async Task RemoveAsync(string id) =>
            await _tahtaCollection.DeleteOneAsync(x => x.Id == id);
    }
}
