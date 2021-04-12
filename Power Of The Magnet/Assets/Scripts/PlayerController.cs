using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Direction { UP, DOWN, RIGHT, LEFT };
public enum Hability { NONE, ATTRACT, REPEL, CHARGE };

public class PlayerController : MonoBehaviour
{
    public float Speed;
    public LayerMask obstacles;

    private Vector3 lastposition;

    private float currentSpeed;
    private GameObject metalSurface;
    private Vector3 targetPosition;
    [SerializeField] private Direction direction;
    [SerializeField] private Hability hability;
    private bool moveBox = false;
    private Vector3 BoxTarget;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        targetPosition = transform.position;
        direction = Direction.UP;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 axisDirection = new Vector2(PlayerInput.Horizontal, PlayerInput.Vertical);

        if (axisDirection != Vector2.zero && targetPosition == transform.position && hability == Hability.NONE)
        {
            if (Mathf.Abs(axisDirection.x) > Mathf.Abs(axisDirection.y))
            {
                if (axisDirection.x > 0)
                {
                    if (!CheckColision && direction == Direction.RIGHT)
                    {
                        targetPosition += new Vector3(0.93f, 0);
                        if (BoxTarget == targetPosition)
                        {
                            targetPosition -= new Vector3(0.93f, 0);
                        }
                    }
                    direction = Direction.RIGHT;

                }
                else
                {
                    if (!CheckColision && direction == Direction.LEFT)
                    {
                        targetPosition -= new Vector3(0.93f, 0);
                        if (BoxTarget == targetPosition)
                        {
                            targetPosition += new Vector3(0.93f, 0);
                        }
                    }
                    direction = Direction.LEFT;
                }
            }
            else
            {
                if (axisDirection.y > 0)
                {
                    if (!CheckColision && direction == Direction.UP)
                    {
                        targetPosition += new Vector3(0, 0.93f);
                        if (BoxTarget == targetPosition)
                        {
                            targetPosition -= new Vector3(0, 0.93f);
                        }
                    }
                    direction = Direction.UP;
                }
                else
                {
                    if (!CheckColision && direction == Direction.DOWN)
                    {
                        targetPosition -= new Vector3(0, 0.93f);
                        if (BoxTarget == targetPosition)
                        {
                            targetPosition += new Vector3(0, 0.93f);
                        }
                    }
                    direction = Direction.DOWN;
                }
            }

        }

        if (direction == Direction.RIGHT)
        {
            //cambiar rotacion
        }

        if (transform.position != targetPosition && !animator.GetBool("Walk"))
        {
            animator.SetBool("Walk", true);
        }
        else if (transform.position == targetPosition && animator.GetBool("Walk"))
        {
            animator.SetBool("Walk", false);
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, currentSpeed * Time.deltaTime);

        if (PlayerInput.attractKey)
        {
            hability = Hability.ATTRACT;
            ActivateHablility();
            animator.SetBool("Atract", true);
        }
        else if (PlayerInput.repelKey)
        {
            hability = Hability.REPEL;
            ActivateHablility();
            animator.SetBool("Repel", true);
        }
        else
        {
            hability = Hability.NONE;
            currentSpeed = Speed;
            animator.SetBool("Atract", false);
            animator.SetBool("Repel", false);
        }

        if (moveBox)
        {
            metalSurface.transform.position = Vector3.MoveTowards(metalSurface.transform.position, BoxTarget, Speed * Time.deltaTime);
            if (metalSurface.transform.position == BoxTarget)
            {
                moveBox = false;
            }
        }
    }
    private bool checkRaycastWithScenario(RaycastHit2D _hits) {
        if ( _hits.collider != null ) {
            if( _hits.collider.gameObject.tag == "CajaMetal" || _hits.collider.gameObject.tag == "ParedMetal") {
                metalSurface = _hits.collider.gameObject;

                return true;            
            }
        }

        return false;
    }
    private bool StartRayCast() {

        bool _collision = false;

        

        if (direction == Direction.UP) {
            RaycastHit2D hits = Physics2D.Raycast(transform.position, Vector2.up, 8.77f, obstacles);
            if (checkRaycastWithScenario(hits)) { _collision = true; }
        }
        else if (direction == Direction.DOWN) {
            RaycastHit2D hits = Physics2D.Raycast(transform.position, Vector2.down, 8.77f, obstacles);
            if (checkRaycastWithScenario(hits)) { _collision = true; }
        }
        else if (direction == Direction.RIGHT) {
            RaycastHit2D hits = Physics2D.Raycast(transform.position, Vector2.right, 8.77f, obstacles);
            if (checkRaycastWithScenario(hits)) { _collision = true; }
        }
        else if (direction == Direction.LEFT) {
            RaycastHit2D hits = Physics2D.Raycast(transform.position, Vector2.left, 8.77f, obstacles);
            if (checkRaycastWithScenario(hits)) { _collision = true; }
        }

        return _collision;
    }

    private void ActivateHablility()
    {
        if (hability == Hability.ATTRACT && StartRayCast() && metalSurface.tag == "CajaMetal")
        {
            currentSpeed = Speed;
            moveBox = true;
            if (direction == Direction.UP)
            {
                BoxTarget = new Vector3(transform.position.x, transform.position.y + 0.93f);
            }
            else if (direction == Direction.DOWN)
            {
                BoxTarget = new Vector3(transform.position.x, transform.position.y - 0.93f);
            }
            else if (direction == Direction.RIGHT)
            {
                BoxTarget = new Vector3(transform.position.x + 0.93f, transform.position.y);
            }
            else if (direction == Direction.LEFT)
            {
                BoxTarget = new Vector3(transform.position.x - 0.93f, transform.position.y);
            }

        }
        else if (hability == Hability.REPEL && StartRayCast() && metalSurface.tag == "CajaMetal"){
 
            float distanceX = 0;
            float distanceY = 0;

            if (direction == Direction.UP)
            {
                distanceX = transform.position.x;
                distanceY = metalSurface.transform.position.y - transform.position.y;
                metalSurface.transform.position = Vector3.MoveTowards(metalSurface.transform.position, new Vector3(distanceX, distanceY + 0.93f), Speed * Time.deltaTime);
            }
            else if (direction == Direction.DOWN)
            {
                distanceX = transform.position.x;
                distanceY = metalSurface.transform.position.y - transform.position.y;
                metalSurface.transform.position = Vector3.MoveTowards(metalSurface.transform.position, new Vector3(distanceX, distanceY - 0.93f), Speed * Time.deltaTime);
            }
            else if (direction == Direction.RIGHT)
            {
                distanceX = metalSurface.transform.position.x - transform.position.x;
                distanceY = transform.position.y;
                metalSurface.transform.position = Vector3.MoveTowards(metalSurface.transform.position, new Vector3(distanceX + 0.93f, distanceY), Speed * Time.deltaTime);
            }
            else if (direction == Direction.LEFT)
            {
                distanceX = metalSurface.transform.position.x - transform.position.x;
                distanceY = transform.position.y;
                metalSurface.transform.position = Vector3.MoveTowards(metalSurface.transform.position, new Vector3(distanceX - 0.93f, distanceY), Speed * Time.deltaTime);
            }

        }
        else if (hability == Hability.ATTRACT && StartRayCast() && metalSurface.tag == "ParedMetal")
        {
            currentSpeed += 0.5f;
            if (direction == Direction.UP)
            {
                targetPosition = new Vector2(metalSurface.transform.position.x, metalSurface.transform.position.y - 0.93f);
            }
            else if (direction == Direction.DOWN)
            {
                targetPosition = new Vector2(metalSurface.transform.position.x, metalSurface.transform.position.y + 0.93f);
            }
            else if (direction == Direction.RIGHT)
            {
                targetPosition = new Vector2(metalSurface.transform.position.x - 0.93f, metalSurface.transform.position.y );
            }
            else if (direction == Direction.LEFT)
            {
                targetPosition = new Vector2(metalSurface.transform.position.x + 0.93f, metalSurface.transform.position.y);
            }
        }else if (hability == Hability.REPEL && StartRayCast() && metalSurface.tag == "ParedMetal")
        {
            currentSpeed += 0.5f;

            float distanceX;
            float distanceY;
            GameObject Collision;
            if (direction == Direction.UP){
                RaycastHit2D hits = Physics2D.Raycast(transform.position, Vector2.down, 100, obstacles);
                Collision = checkRaycastWithScenarioRepel(hits);
                if (Collision != null)
                {
                    distanceX = transform.position.x;
                    distanceY = Collision.transform.position.y + 0.93f;
                    targetPosition = new Vector2(distanceX, distanceY);
                }
                
                
            }
            else if (direction == Direction.DOWN)
            {
                RaycastHit2D hits = Physics2D.Raycast(transform.position, Vector2.up, 100, obstacles);
                Collision = checkRaycastWithScenarioRepel(hits);
                if (Collision != null)
                {
                    distanceX = transform.position.x;
                    distanceY = Collision.transform.position.y - 0.93f;
                    targetPosition = new Vector2(distanceX, distanceY);
                }



            }
            else if (direction == Direction.RIGHT)
            {
                RaycastHit2D hits = Physics2D.Raycast(transform.position, Vector2.left, 100, obstacles);
                Collision = checkRaycastWithScenarioRepel(hits);
                if (Collision != null)
                {
                    distanceX = Collision.transform.position.x + 0.93f;
                    distanceY = transform.position.y;
                    targetPosition = new Vector2(distanceX, distanceY);
                }
            }
            else if (direction == Direction.LEFT)
            {
                RaycastHit2D hits = Physics2D.Raycast(transform.position, Vector2.right, 100, obstacles);
                Collision = checkRaycastWithScenarioRepel(hits);
                if (Collision != null)
                {
                    distanceX = Collision.transform.position.x - 0.93f;
                    distanceY = transform.position.y;
                    targetPosition = new Vector2(distanceX, distanceY);
                }
            }
        }
        else
        {
            if (transform.position == targetPosition)
            {
                currentSpeed = Speed;
            }
            
        }

    }

    private GameObject checkRaycastWithScenarioRepel(RaycastHit2D _hits)
    {
        GameObject Back;
        if (_hits.collider != null)
        {
            
                Back = _hits.collider.gameObject;

                return Back;
            
        }

        return null;
    }

    bool CheckColision
    {
        get
        {
            RaycastHit2D rh;

            Vector2 dir = Vector2.zero;
            if (direction == Direction.DOWN)
            {
                dir = Vector2.down;
            }

            if (direction == Direction.UP)
            {
                dir = Vector2.up;
            }
            if (direction == Direction.RIGHT)
            {
                dir = Vector2.right;
            }
            if (direction == Direction.LEFT)
            {
                dir = Vector2.left;
            }

            rh = Physics2D.Raycast(transform.position, dir, 1, obstacles);

            return rh.collider != null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Pared" || collision.gameObject.tag == "ParedMetal" || collision.gameObject.tag == "CajaMetal")
        {
            targetPosition = transform.position;
        }
    }
}
