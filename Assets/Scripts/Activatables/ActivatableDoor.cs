using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatableDoor : MonoBehaviour, Activatable {
    public GameObject doorColider;
    public Animator anim;
    public bool state = true;

    public void Start() {
        doorColider.SetActive(state);
    }

    public void OnActivate() {
        ChangeState();
    }

    void ChangeState() {
        state = !state;
        doorColider.SetActive(state);
    }
}
