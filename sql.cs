using Microsoft.Data.SqlClient;
using System.Data;

namespace Core
{
    public class ItemRepository : IPlayerRepository
    {
        private readonly string _connectionString;

        public ItemRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private IDbConnection Connection => new Microsoft.Data.SqlClient.SqlConnection(_connectionString);

        private async Task<IDbConnection> OpenConnectionAsync()
        {
            var connection = Connection;
            await connection.OpenAsync();
            return connection;
        }

        public async Task<IEnumerable<Player>> GetAllItemsAsync()
        {
            using (var dbConnection = await OpenConnectionAsync())
            {
                return await dbConnection.QueryAsync<Player>("SELECT * FROM Items");
            }
        }

        public async Task<Player> GetItemByIdAsync(int id)
        {
            using (var dbConnection = await OpenConnectionAsync())
            {
                return await dbConnection.QueryFirstOrDefaultAsync<Player>(
                    "SELECT * FROM Items WHERE Id = @Id", new { Id = id });
            }
        }

        public async Task AddItemAsync(Player player)
        {
            using (var dbConnection = await OpenConnectionAsync())
            {
                var query = "INSERT INTO Items (Name, Description) VALUES (@Name, @Description)";
                await dbConnection.ExecuteAsync(query, player);
            }
        }

        public async Task UpdateItemAsync(Player player)
        {
            using (var dbConnection = await OpenConnectionAsync())
            {
                var query = "UPDATE Items SET Name = @Name, Description = @Description WHERE Id = @Id";
                await dbConnection.ExecuteAsync(query, player);
            }
        }

        public async Task DeleteItemAsync(int id)
        {
            using (var dbConnection = await OpenConnectionAsync())
            {
                var query = "DELETE FROM Items WHERE Id = @Id";
                await dbConnection.ExecuteAsync(query, new { Id = id });
            }
        }
    }
}
