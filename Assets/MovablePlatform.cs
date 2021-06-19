using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablePlatform : MonoBehaviour, Activatable {
    public GameObject platform;
    public Transform[] positionsArray;
    public Animator anim;
    public int state = 0;

    public void Start() {
        
    }

    public void OnActivate() {
        ChangeState();
    }

    void ChangeState() {
        if(state < 1) {
            state++;
        }else state = 0;
        platform.GetComponent<Transform>().position = positionsArray[state].position;
    }
}
