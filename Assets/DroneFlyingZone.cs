using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneFlyingZone : MonoBehaviour {
    public DroneMovement drone;

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Drone" && drone.mode != 0) {
            drone.ChangeModeTo(3);
        }
    }
}
