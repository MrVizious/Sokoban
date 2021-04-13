using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 2f;
    public float deadZone = 0.05f;
    private bool moving = false;
    private float lastHorizontal = 0f;
    private float lastVertical = 0f;
    [SerializeField] private Vector2 target;

    private void Start() {
        target = transform.position;
    }

    private void Update() {
        if (!moving) Input();
        if (Vector2.Distance(transform.position, target) > Mathf.Epsilon)
        {
            moving = true;
            Move();
        }
        else
        {
            moving = false;
        }
    }

    private void Input() {

        float newHorizontal = UnityEngine.Input.GetAxisRaw("Horizontal");
        float newVertical = UnityEngine.Input.GetAxisRaw("Vertical");

        if (newHorizontal > deadZone && lastHorizontal != newHorizontal)
        {
            target = new Vector2(Mathf.Floor(transform.position.x) + 0.5f + 1f,
                                 Mathf.Floor(transform.position.y) + 0.5f);
        }
        else if (newHorizontal < -deadZone && lastHorizontal != newHorizontal)
        {
            target = new Vector2(Mathf.Floor(transform.position.x) + 0.5f - 1f,
                                 Mathf.Floor(transform.position.y) + 0.5f);
        }
        else if (newVertical > deadZone && lastVertical != newVertical)
        {
            target = new Vector2(Mathf.Floor(transform.position.x) + 0.5f,
                                 Mathf.Floor(transform.position.y) + 0.5f + 1f);
        }
        else if (newVertical < -deadZone && lastVertical != newVertical)
        {
            target = new Vector2(Mathf.Floor(transform.position.x) + 0.5f,
                                 Mathf.Floor(transform.position.y) + 0.5f - 1f);
        }

        lastHorizontal = newHorizontal;
        lastVertical = newVertical;

    }

    private void Move() {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }
}
