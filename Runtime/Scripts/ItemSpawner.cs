using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventoryPackage;

namespace InventoryPackage
{
    public class ItemSpawner : MonoBehaviour
    {
        public GameObject SpawnItem(ItemType itemType, Transform transform)
        {
            GameObject spawn = Instantiate(new GameObject(itemType.TypeName),transform.position, transform.rotation);
            ItemInstance itemInstance = spawn.AddComponent<ItemInstance>();
            itemInstance.SetItemType(itemType);
            return spawn;
        }

    }
}
