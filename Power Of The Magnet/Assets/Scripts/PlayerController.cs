using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Direction { UP, DOWN, RIGHT, LEFT };
public enum Hability { ATTRACT, REPEL, CHARGE };

public class PlayerController : MonoBehaviour
{
    public float Speed;

    private Transform metalBox;
    
    private Vector3 targetPosition;
    public Direction direction;
    public Hability hability;

    KeyCode attractKey = KeyCode.Mouse0;
    KeyCode repelKey = KeyCode.Mouse1;


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
                    targetPosition += new Vector3(0.93f, 0);
                }
                else
                {
                    direction = Direction.LEFT;
                    targetPosition -= new Vector3(0.93f, 0);
                }
            }
            else
            {
                if (axisDirection.y > 0)
                {
                    direction = Direction.UP;
                    targetPosition += new Vector3(0, 0.93f);
                }
                else
                {
                    direction = Direction.DOWN;
                    targetPosition -= new Vector3(0, 0.93f);
                }
            }

        }
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Speed * Time.deltaTime);

        if (Input.GetKey(attractKey)) {
            hability = Hability.ATTRACT;
        }else if (Input.GetKey(repelKey)) {
            hability = Hability.REPEL;
        }
    }
    private bool checkRaycastWithScenario(RaycastHit2D _hits) {
        if ( _hits.collider != null ) {
            if( _hits.collider.gameObject.tag == "Metal") {
                metalBox = GameObject.Find("MetalBox").GetComponent<Transform>();

                return true;            
            }
        }

        return false;
    }
    private void StartRayCast(Hability _hability) {

        bool _collision = false;

        LayerMask metalBoxMask = LayerMask.GetMask("MetalBox");

        if (direction == Direction.UP) {
            RaycastHit2D hits = Physics2D.Raycast(transform.position, Vector2.up, 240, metalBoxMask);
            if (checkRaycastWithScenario(hits)) { _collision = true; }
        }
        else if (direction == Direction.DOWN) {
            RaycastHit2D hits = Physics2D.Raycast(transform.position, Vector2.down, 240, metalBoxMask);
            if (checkRaycastWithScenario(hits)) { _collision = true; }
        }
        else if (direction == Direction.RIGHT) {
            RaycastHit2D hits = Physics2D.Raycast(transform.position, Vector2.right, 240, metalBoxMask);
            if (checkRaycastWithScenario(hits)) { _collision = true; }
        }
        else if (direction == Direction.LEFT) {
            RaycastHit2D hits = Physics2D.Raycast(transform.position, Vector2.left, 240, metalBoxMask);
            if (checkRaycastWithScenario(hits)) { _collision = true; }
        }

        if (_hability == Hability.ATTRACT && _collision) {

            if (direction == Direction.UP)
            {
                
            }
            else if (direction == Direction.DOWN)
            {
                
            }
            else if (direction == Direction.RIGHT)
            {
               
            }
            else if (direction == Direction.LEFT)
            {
                
            }

        }
        else if (_hability == Hability.REPEL && _collision) {

        }
    }
}
