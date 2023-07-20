using LoginPage.Models;
using MongoDB.Driver;

namespace LoginPage.Service
{
    public class DbMethods
    {
        /*mongodb://hash:123@ac-rwkaegx-shard-00-00.gfyyaga.mongodb.net:27017,ac-rwkaegx-shard-00-01.gfyyaga.mongodb.net:27017,ac-rwkaegx-shard-00-02.gfyyaga.mongodb.net:27017/?ssl=true&replicaSet=atlas-hsyte0-shard-0&authSource=admin&retryWrites=true&w=majority*/
        /*  mongodb+srv://hash:<password>@cluster0.gfyyaga.mongodb.net/?retryWrites=true&w=majority*/
        private const string connectionString = "mongodb://hash:123@ac-rwkaegx-shard-00-00.gfyyaga.mongodb.net:27017,ac-rwkaegx-shard-00-01.gfyyaga.mongodb.net:27017,ac-rwkaegx-shard-00-02.gfyyaga.mongodb.net:27017/?ssl=true&replicaSet=atlas-hsyte0-shard-0&authSource=admin&retryWrites=true&w=majority";
        private const string dbName = "UserData";
        private const string userCollection = "Users";


        public IMongoCollection<T> ConnectToMongo<T>(in string collection)
        {
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(dbName);
            return db.GetCollection<T>(collection);
        }

        public async Task<List<People>> GetUserByID(int id)
        {
            var user = ConnectToMongo<People>(userCollection);
            var result = await user.FindAsync(u => u.Id.Equals(id));
            return await result.ToListAsync();
        }

        public async Task<List<People>> GetUsers()
        {
            var user = ConnectToMongo<People>(userCollection);
            var result = await user.FindAsync(_ => true);
            return await result.ToListAsync();
        }

        public async Task<bool> FindUserByEmail(string username, string email, string password)
        {
            var collection = ConnectToMongo<People>(userCollection);

            var result = await collection.FindAsync(u => (u.Username.Equals(username) || u.Email.Equals(email)) && u.Password.Equals(password));
            return result.Any() ? true : false;
        }

        public async Task<bool> FindUser(People? user)
        {
            var collection = ConnectToMongo<People>(userCollection);
            if (user != null)
            {
                var result = await collection.FindAsync(us => us.Email.Equals(user.Email) && us.Password.Equals(user.Password));
                /* Console.WriteLine("hellop " + result.ToString());*/
                return result.Any() ? true : false;
            }
            return false;

        }
        public Task SaveUser(People user)
        {
            var collection = ConnectToMongo<People>(userCollection);
            return collection.InsertOneAsync(user);
        }

    }
}
