using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInventory : MonoBehaviour {
    public int keys = 0; //change to private once you add the key item
    public int coins = 0;
    public TextMeshProUGUI keysUI;
    public TextMeshProUGUI coinsUI;

    public void AddKey() {
        keys++;
        keysUI.text = keys.ToString();
    }



    public void AddCoin() {
        coins++;
        coinsUI.text = coins.ToString();
    }

    public bool UseKey() {
        if(keys > 0) {
            keys--;
            return true;
        }
        else return false;
    }
}
