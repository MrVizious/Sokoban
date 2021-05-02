using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Box : MonoBehaviour
{
    public float speed = 5f;
    [SerializeField] private bool debug;
    private IEnumerator movingCoroutine = null;
    private void Start() {
        GetComponent<Rigidbody2D>().isKinematic = true;
        movingCoroutine = null;
    }
    public bool CanMove(int x, int y) {

        if (movingCoroutine != null) return false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * x + Vector2.up * y, 0.8f);

        if (hit.collider != null)
        {
            if (debug) Debug.Log("Hit a collider with tag: " + hit.collider.tag);
            if (hit.collider.tag.Equals("Wall") || hit.collider.tag.Equals("Box"))
            {
                return false;
            }
            if (hit.collider.tag.Equals("Player"))
            {
                return hit.collider.gameObject.GetComponent<Player>().CanMove(x, y);
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
