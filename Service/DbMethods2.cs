using LoginPage.Models;
using MongoDB.Driver;

namespace LoginPage.Service
{
    public class DbMethods2
    {
        private const string connectionString = "mongodb+srv://harsh:123@cluster0.jgaahuz.mongodb.net/?retryWrites=true&w=majority";
        private const string dbName = "UserData";
        private const string userCollection = "DataCollection";


        public IMongoCollection<T> ConnectToMongo<T>(in string collection)
        {
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(dbName);
            return db.GetCollection<T>(collection);
        }
        public async Task<List<Details>> GetUserByID(string id)
        {
            var user = ConnectToMongo<Details>(userCollection);
            var result = await user.FindAsync(u => u.MyId.Equals(id));
            return await result.ToListAsync();
        }

        public Task SaveUser(Details data)
        {
            var collection = ConnectToMongo<Details>(userCollection);
            return collection.InsertOneAsync(data);
        }
        public async Task<List<Details>> ShowAll()
        {
            var collection = ConnectToMongo<Details>(userCollection);
            var filter = Builders<Details>.Filter.Empty; // Retrieve all documents

            var result = await collection.Find(filter).ToListAsync();
            return result;
        }
        public async Task DeleteUser(string userid)
        {
            var collection = ConnectToMongo<Details>(userCollection);
            var filter = Builders<Details>.Filter.Eq(d => d.MyId, userid);
            await collection.DeleteOneAsync(filter);
        }

        public async Task<bool> UpdateUser(string userId, Details updatedUserData)
        {
            try
            {
                var collection = ConnectToMongo<Details>(userCollection);

                // Create a filter to find the document to update by MyId
                var filter = Builders<Details>.Filter.Eq(d => d.MyId, userId);

                // Create an update definition to set the new values
                var update = Builders<Details>.Update
                    .Set(d => d.Username, updatedUserData.Username)
                    .Set(d => d.Description, updatedUserData.Description);

                // Perform the update operation
                var updateResult = await collection.ReplaceOneAsync(filter, updatedUserData);

                // Check if the update was successful
                return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                // Handle any exceptions that may occur during the update
                // You can log the exception or handle it as needed
                return false; // Update failed
            }
        }


    }
}
