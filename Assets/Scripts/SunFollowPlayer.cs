using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunFollowPlayer : MonoBehaviour {
    public GameObject player;

    // Update is called once per frame
    void Update() {
        Vector3 newPos = new Vector3(player.GetComponent<Transform>().position.x - 15, this.transform.position.y, this.transform.position.z);
        this.transform.position = newPos;
    }
}
