using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GroundItem : MonoBehaviour/*, ISerializationCallbackReceiver*/
{
    public ItemObject item;

    //public void OnAfterDeserialize()
    //{
    //}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") 
        {
            if (item)
            {
                Item _item = new Item(item);
                other.gameObject.GetComponent<PlayerController>().getItem(_item);
                Destroy(this.gameObject);
            }
        }
    }
    //public void OnBeforeSerialize()
    //{
    //    GetComponentInChildren<SpriteRenderer>().sprite = item.uiDisplay;
    //    EditorUtility.SetDirty(GetComponentInChildren<SpriteRenderer>());
    //}
}
