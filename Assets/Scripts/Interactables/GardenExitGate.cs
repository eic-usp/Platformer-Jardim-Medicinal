using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenExitGate : MonoBehaviour, Interactable{
    public GameObject stageMenu;

    public void OnInteract() {
        GameObject.Find("PlayerSprite").GetComponent<PlayerMovement>().CanMove = false;
        stageMenu.SetActive(true);
    }

}
