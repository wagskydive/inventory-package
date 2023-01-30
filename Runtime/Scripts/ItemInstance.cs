using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventoryPackage
{
    public class ItemInstance : MonoBehaviour
    {
        private ItemType typeOfItem;

        public ItemType TypeOfItem {get => typeOfItem;}

        public void SetItemType(ItemType itemType)
        {
            this.typeOfItem = itemType;
        }

        internal void Use()
        {

        }
    }
}
