using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventoryPackage
{
    public class ItemInstance : MonoBehaviour
    {
        public ItemType itemType{get; private set; }

        public void SetItemType(ItemType itemType)
        {
            this.itemType = itemType;
        }
    }
}
