using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollectable : MonoBehaviour {
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") { 
            GameObject.Find("PlayerSprite").GetComponent<PlayerInventory>().AddCoin();
            gameObject.SetActive(false);
        }
    }
}
