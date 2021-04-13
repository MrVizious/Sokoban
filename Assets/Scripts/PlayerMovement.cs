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
            CanMove(1, 0);
        }
        else if (newHorizontal < -deadZone && lastHorizontal != newHorizontal)
        {
            CanMove(-1, 0);
        }
        else if (newVertical > deadZone && lastVertical != newVertical)
        {
            CanMove(0, 1);
        }
        else if (newVertical < -deadZone && lastVertical != newVertical)
        {
            CanMove(0, -1);
        }

        lastHorizontal = newHorizontal;
        lastVertical = newVertical;

    }

    private void Move() {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    private bool CanMove(int x, int y) {

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * x + Vector2.up * y, 0.8f);

        if (hit.collider != null)
        {
            if (hit.collider.tag.Equals("Wall"))
            {
                Debug.Log("Hit wall");
                return false;
            }
        }

        target = new Vector2(Mathf.Floor(transform.position.x) + x + 0.5f,
                             Mathf.Floor(transform.position.y) + y + 0.5f);
        return true;

    }
}
