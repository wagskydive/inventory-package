namespace InventoryPackage
{
    public class Character
    {
        public string CharacterName { get; private set; }

        public Inventory PersonalInventory { get; private set; }

        public Character(string name)
        {
            CharacterName = name;
            PersonalInventory = InventoryBuilder.CreateInventory(GlobalSettings.DefaultCharacterInventorySize);
        }

        public Character(string name, int inventorySize)
        {
            CharacterName = name;
            PersonalInventory = InventoryBuilder.CreateInventory(inventorySize);
        }
    }
}