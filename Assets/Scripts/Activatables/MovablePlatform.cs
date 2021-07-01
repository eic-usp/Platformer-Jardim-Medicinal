using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablePlatform : MonoBehaviour, Activatable {
    public GameObject platform;
    public Transform[] positionsArray;
    public int position = 0;
    public float speed = 1;
    private bool isMoving = false;
    private Vector3 curr;
    private float i = 0;

    public void Start() {
        platform.GetComponent<Transform>().position = positionsArray[position].position;
    }

    public void OnActivate() {
        ChangePosition();
    }

    private void FixedUpdate() {
        if(isMoving) {
            platform.GetComponent<Transform>().position = Vector3.Lerp(curr, positionsArray[position].position, i);
            if(i > 1) {
                isMoving = false;
                i = 0;
            }
            i += 0.01f * speed;
        }
    }


    void ChangePosition() {
        if(position < positionsArray.Length - 1) {
            position++;
        }else position = 0;
        curr = platform.GetComponent<Transform>().position;
        i = 0;
        isMoving = true;
    }
}
