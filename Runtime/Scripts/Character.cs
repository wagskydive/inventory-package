namespace InventoryAndCharacterLogic
{
    public class Character
    {
        public string CharacterName {get; private set;}

        public Inventory PersonalInventory{get; private set;}

        public Character(string name)
        {
            CharacterName = name;

        }


    }
}