using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenExitGate : MonoBehaviour, Interactable{
    public GameObject stageMenu;

    public void OnInteract() {
        stageMenu.SetActive(true);
    }

}
