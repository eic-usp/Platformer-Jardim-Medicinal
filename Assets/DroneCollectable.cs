using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneCollectable : MonoBehaviour, Interactable{
    public GameObject drone;

    public void OnInteract() {
        drone.SetActive(true);
        gameObject.SetActive(false);
    }
}
