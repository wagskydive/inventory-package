using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventoryPackage
{

    public class ItemInstance : MonoBehaviour, IItemInstance
    {
        private ItemType typeOfItem;

        public ItemType TypeOfItem { get => typeOfItem; }

        public void SetItemType(ItemType itemType)
        {
            this.typeOfItem = itemType;
        }

        public void Use()
        {

        }
    }
}
