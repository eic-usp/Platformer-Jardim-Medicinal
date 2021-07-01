using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {
    public int keys = 0; //change to private once you add the key item

    public void AddKey() {
        keys++;
    }

    public bool UseKey() {
        if(keys > 0) {
            keys--;
            return true;
        }
        else return false;
    }
}
