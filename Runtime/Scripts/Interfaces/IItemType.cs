using UnityEngine;
namespace InventoryPackage
{
    public interface IItemType
    {
        string TypeName { get; }
        string Description { get; }
        Texture2D Icon { get; }
        int StackSize { get; }
        string ResourceFolderPath { get; }
    }
}