namespace Core
{
    public class PlayerService
    {
        private readonly IPlayerRepository _repository;

        public PlayerService(IPlayerRepository repository)
        {
            _repository = repository;
        }

        public async Task<Player> AddItemAsync(Player item)
        {
            if (string.IsNullOrEmpty(item.Name))
                throw new ArgumentException("Name cannot be empty.");

            await _repository.AddItemAsync(item);
            return item;
        }

        public async Task<Player> UpdateItemAsync(int id, Player item)
        {
            var existingItem = await _repository.GetItemByIdAsync(id);
            if (existingItem == null)
                throw new KeyNotFoundException("Item not found.");

            existingItem.Name = item.Name;
            existingItem.Description = item.Description;

            await _repository.UpdateItemAsync(existingItem);
            return existingItem;
        }
        public async Task DeleteItemAsync(int id)
        {
            var item = await _repository.GetItemByIdAsync(id);
            if (item == null)
                throw new KeyNotFoundException("Item not found.");

            await _repository.DeleteItemAsync(id);
        }

        public async Task<IEnumerable<Player>> GetAllItemsAsync()
        {
            return await _repository.GetAllItemsAsync();
        }

        public async Task<Player> GetItemByIdAsync(int id)
        {
            return await _repository.GetItemByIdAsync(id);
        }
    }
}
