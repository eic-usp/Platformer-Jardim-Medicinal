using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {

    private float len, startPos;
    public GameObject cam;
    public float parallaxFx;
    public bool followCam = true;

    void Start() {
        startPos = this.transform.position.x;
        len = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate() {
        float temp = (cam.transform.position.x * (1 - parallaxFx));
        float dist = (cam.transform.position.x * parallaxFx);

        this.transform.position = new Vector3(startPos + dist,transform.position.y,transform.position.z);

        if(followCam) {
            if (temp > startPos + len) startPos += len;
            else if (temp < startPos - len) startPos -= len;
        }
    }

}
