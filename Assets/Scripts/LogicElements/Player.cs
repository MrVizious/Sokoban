using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    public float deadZone = 0.05f;
    [SerializeField] private bool debug = false;
    private Vector2 target;
    private IEnumerator movingCoroutine = null;

    private void Start() {
        target = transform.position;
        movingCoroutine = null;
    }

    private void Update() {
        if (movingCoroutine == null && Time.timeScale == 1f) Input();
    }

    private void Input() {

        float horizontalInput = UnityEngine.Input.GetAxisRaw("Horizontal");
        float verticalInput = UnityEngine.Input.GetAxisRaw("Vertical");

        if (horizontalInput > deadZone)
        {
            if (debug) Debug.Log("Trying to move right");
            CanMove(1, 0);
        }
        else if (horizontalInput < -deadZone)
        {
            if (debug) Debug.Log("Trying to move left");
            CanMove(-1, 0);
        }
        else if (verticalInput > deadZone)
        {
            if (debug) Debug.Log("Trying to move up");
            CanMove(0, 1);
        }
        else if (verticalInput < -deadZone)
        {
            if (debug) Debug.Log("Trying to move down");
            CanMove(0, -1);
        }
    }

    private bool CanMove(int x, int y) {

        if (movingCoroutine != null) return false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * x + Vector2.up * y, 0.8f);

        if (hit.collider != null)
        {
            if (hit.collider.tag.Equals("Wall"))
            {
                if (debug) Debug.Log("Hit wall");
                return false;
            }
            else if (hit.collider.tag.Equals("Box"))
            {
                bool canMoveBox = hit.collider.gameObject.GetComponent<Box>().CanMove(x, y);
                if (debug)
                {
                    if (canMoveBox)
                    {
                        Debug.Log("Can move the box!");
                    }
                    else
                    {
                        Debug.Log("Can move the box!");
                    }
                }
                if (!canMoveBox) return false;
            }
        }

        StartMoving(x, y);
        return true;
    }

    private void StartMoving(int x, int y) {
        if (movingCoroutine == null)
        {
            Vector2 newTarget = new Vector2(Mathf.Floor(transform.position.x) + x + 0.5f,
                                 Mathf.Floor(transform.position.y) + y + 0.5f);

            movingCoroutine = MovingCoroutine(newTarget);
            if (debug) Debug.Log("Will try to move box");
            StartCoroutine(movingCoroutine);
        }
    }

    private IEnumerator MovingCoroutine(Vector2 target) {
        if (debug) Debug.Log("Starting moving coroutine");
        while (Vector2.Distance(transform.position, target) > Mathf.Epsilon)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
            yield return null;
        }

        transform.position = target;
        movingCoroutine = null;
    }
}