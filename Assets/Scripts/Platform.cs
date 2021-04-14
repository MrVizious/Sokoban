using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Platform : MonoBehaviour
{
    public bool debug = false;
    void Start() {
        GetComponent<Collider2D>().isTrigger = true;
        Deactivate();
    }

    private bool Activate() {
        return LevelController.Instance.UntrackPlatform(this);
    }

    private bool Deactivate() {
        LevelController newLevel = LevelController.Instance;
        Debug.Log(newLevel);
        return LevelController.Instance.TrackPlatform(this);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (debug) Debug.Log("Trigger Enter detected on platform ", this);
        if (other.tag.Equals("Box"))
        {
            if (debug) Debug.Log("Box detected, trying to activate");
            Activate();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (debug) Debug.Log("Trigger Exit detected on platform ", this);
        if (other.tag.Equals("Box"))
        {
            if (debug) Debug.Log("Box detected, trying to deactivate");
            Deactivate();
        }
    }

}
