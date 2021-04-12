using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Direction { UP, DOWN, RIGHT, LEFT };

public class PlayerController : MonoBehaviour
{
    public float Speed;
    
    private Vector3 targetPosition;
    public Direction direction;


    // Start is called before the first frame update
    void Start()
    {
        targetPosition = transform.position;
        direction = Direction.UP;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 axisDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (axisDirection != Vector2.zero && targetPosition == transform.position)
        {
            if (Mathf.Abs(axisDirection.x) > Mathf.Abs(axisDirection.y))
            {
                if (axisDirection.x > 0)
                {
                    direction = Direction.RIGHT;
                    targetPosition += Vector3.right;
                }
                else
                {
                    direction = Direction.LEFT;
                    targetPosition -= Vector3.right;
                }
            }else{
                if (axisDirection.y > 0)
                {
                    direction = Direction.UP;
                    targetPosition += Vector3.up;
                }
                else
                {
                    direction = Direction.DOWN;
                    targetPosition -= Vector3.up;
                }
            }
            
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Speed * Time.deltaTime);
    }
}
