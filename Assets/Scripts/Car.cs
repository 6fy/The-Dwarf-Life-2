using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{

    public float moveSpeed = 5.0f;
    public Direction objectDirection = Direction.Right;

    public List<Transform> assignedTargets;
    public Transform startFrom = null;

    private List<Transform> targets = new List<Transform>();
    private bool movable = false;
    private bool repeat = false;

    void Start()
    {
        if (assignedTargets.Count > 0)
        {
            targets = new List<Transform>(assignedTargets);
            movable = true;
        }
        if (startFrom != null)
        {
            repeat = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        var step = moveSpeed * Time.deltaTime; // calculate distance to move
        // teleport to start position if there are no targets
        if (targets.Count == 0) {
            if (movable) {
                if (repeat) {
                    transform.position = startFrom.position;
                }
                targets = new List<Transform>(assignedTargets);
            }
            return;
        }

        for (int i = 0; i < targets.Count; i++)
        {
            Transform target = targets[i]; // get target from list
            // if target is within range of current position, remove it from the list
            if (Vector3.Distance(transform.position, target.position) < 1.5f)
            {
                targets.Remove(target);
                continue;
            }

            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            break;
        }
    }

    private Vector3 GetVectorDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return Vector3.up;
            case Direction.Down:
                return Vector3.down;
            case Direction.Left:
                return Vector3.left;
            default:
                return Vector3.right;
        }
    }
}

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}