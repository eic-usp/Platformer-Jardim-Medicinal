using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : MonoBehaviour, Interactable {
    public void OnInteract() {
        GameObject.Find("PlayerSprite").GetComponent<PlayerInventory>().AddKey();
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") { 
            GameObject.Find("PlayerSprite").GetComponent<PlayerInventory>().AddKey();
            gameObject.SetActive(false);
        }
    }
}
