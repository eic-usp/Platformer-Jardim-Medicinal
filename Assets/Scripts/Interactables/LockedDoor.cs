using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour, Interactable {
    private GameObject player;
    public GameObject doorColider;

    public void Start() {
        player = GameObject.Find("PlayerSprite");
    }

    public void OnInteract() {
        if(UseKey()) {
            doorColider.SetActive(false);
        }
    }

    private bool UseKey() {
        return player.GetComponent<PlayerInventory>().UseKey();
    }
}
