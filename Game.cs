using System.Data;
using System.Data.SqlClient;

namespace Core
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public interface IPlayerRepository
    {
        Task<IEnumerable<Player>> GetAllItemsAsync();
        Task<Player> GetItemByIdAsync(int id);  // Использование id вместо Player
        Task AddItemAsync(Player player);       // Использование player вместо Player
        Task UpdateItemAsync(Player player);    // Использование player вместо Player
        Task DeleteItemAsync(int id);           // Использование id вместо Player
    }
}
