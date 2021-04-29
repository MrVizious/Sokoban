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
        LevelController level = LevelController.Instance;
        if (level != null)
        {
            LevelController.Instance.UntrackPlatform(this);
        }
        return false;
    }

    private bool Deactivate() {
        LevelController level = LevelController.Instance;
        if (level != null)
        {
            LevelController.Instance.TrackPlatform(this);
        }
        return false;
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

    private void OnDestroy() {
        Activate();
    }

}
