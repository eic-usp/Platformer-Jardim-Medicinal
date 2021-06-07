using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundButton : MonoBehaviour {
    public GameObject door;

    void OnTriggerStay2D(Collider2D collider) {
        door.SetActive(false);
    }

    void OnTriggerExit2D(Collider2D collider) {
        door.SetActive(true);
    }
}
