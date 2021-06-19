using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverInteractable : MonoBehaviour, Interactable{
    public GameObject[] doors;

    public void OnInteract() {
        foreach(GameObject door in doors) {
            door.GetComponent<Activatable>().OnActivate();
        }
    }
}
