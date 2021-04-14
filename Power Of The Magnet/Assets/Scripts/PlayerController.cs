using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { UP, DOWN, RIGHT, LEFT };
public enum Hability { NONE, ATTRACT, REPEL, CHARGE };

public class PlayerController : MonoBehaviour
{
    public float Speed;
    public LayerMask obstacles;
    [SerializeField] private Direction direction;
    [SerializeField] private Hability hability;
    public GameObject OptionMenu;

    [Header("Particulas Atraer")]
    public GameObject PSAttractU;
    public GameObject PSAttractD;
    public GameObject PSAttractR;
    public GameObject PSAttractL;
    [Header("Particulas Repeler")]
    public GameObject PSRepelU;
    public GameObject PSRepelD;
    public GameObject PSRepelR;
    public GameObject PSRepelL;
    [Header("Rango de Atraer")]
    public GameObject AttractAreaU;
    public GameObject AttractAreaD;
    public GameObject AttractAreaR;
    public GameObject AttractAreaL;
    [Header("Rango de Repeler")]
    public GameObject RepelAreaU;
    public GameObject RepelAreaD;
    public GameObject RepelAreaR;
    public GameObject RepelAreaL;

    private GameObject metalSurface;
    private Animator animator;
    private SpriteRenderer sprite;
    private SpriteRenderer boxSprite;

    private float currentSpeed;
    private Vector3 targetPosition;
    private bool moveBox = false;
    private Vector3 BoxTarget;
    private float ChangeDirTime = 135;
    private float WaitedTime;
    private Vector3 lastposition;
    private bool menu;
    private float timer = 0.0f;


    // Start is called before the first frame update.
    void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        targetPosition = transform.position;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        menu = PlayerInput.PauseMenu;
        if (menu)
        {
            if (Time.timeScale == 1)
            {
                PauseGame();
            }
            else if (Time.timeScale == 0)
            {
                
                ResumeGame();
            }
        }

        if (metalSurface != null)
        {
            if (metalSurface.tag == "CajaMetal")
            {
                if (BoxTarget == metalSurface.transform.position)
                {
                    metalSurface.transform.rotation = Quaternion.Euler(0, 0, 0);
                    boxSprite.color = Color.white;
                    metalSurface = null;
                }

            }else{
                if (hability == Hability.NONE)
                {
                    boxSprite.color = Color.white;
                    metalSurface = null;
                }
            }
            
        }

        float delta = Time.deltaTime * 1000;
        Vector2 axisDirection = new Vector2(PlayerInput.Horizontal, PlayerInput.Vertical);

        if (metalSurface != null && hability == Hability.REPEL)
        {
            sacudirCaja();
            boxSprite.color = Color.red;
                //new Color(243, 70, 70);
        }
        else if (metalSurface != null && hability == Hability.ATTRACT)
        {
            sacudirCaja();
            boxSprite.color = Color.blue;
            //new Color(135, 144, 253)
        }

        if (axisDirection != Vector2.zero && targetPosition == transform.position && hability == Hability.NONE)
        {
            if (Mathf.Abs(axisDirection.x) > Mathf.Abs(axisDirection.y))
            {
                if (axisDirection.x > 0)
                {
                    if (direction != Direction.RIGHT)
                    {
                        WaitedTime = 0;
                    }
                    WaitedTime += delta;
                    direction = Direction.RIGHT;
                    if (!CheckColision && WaitedTime > ChangeDirTime)
                    {
                        targetPosition += new Vector3(0.93f, 0);
                        if (BoxTarget == targetPosition)
                        {
                            targetPosition -= new Vector3(0.93f, 0);
                        }
                    }
                    

                }
                else
                {
                    if (direction != Direction.LEFT)
                    {
                        WaitedTime = 0;
                    }
                    direction = Direction.LEFT;
                    WaitedTime += delta;
                    if (!CheckColision && WaitedTime > ChangeDirTime)
                    {
                        targetPosition -= new Vector3(0.93f, 0);
                        if (BoxTarget == targetPosition)
                        {
                            targetPosition += new Vector3(0.93f, 0);
                        }
                    }
                    
                }
            }
            else
            {
                if (axisDirection.y > 0)
                {
                    if (direction != Direction.UP)
                    {
                        WaitedTime = 0;
                    }
                    WaitedTime += delta;
                    direction = Direction.UP;
                    if (!CheckColision && WaitedTime > ChangeDirTime)
                    {
                        targetPosition += new Vector3(0, 0.93f);
                        if (BoxTarget == targetPosition)
                        {
                            targetPosition -= new Vector3(0, 0.93f);
                        }
                    }
                }
                else
                {
                    if (direction != Direction.DOWN)
                    {
                        WaitedTime = 0;
                    }
                    WaitedTime += delta;
                    direction = Direction.DOWN;

                    if (!CheckColision && WaitedTime > ChangeDirTime)
                    {
                        targetPosition -= new Vector3(0, 0.93f);
                        if (BoxTarget == targetPosition)
                        {
                            targetPosition += new Vector3(0, 0.93f);
                        }
                    }
                }
            }

        }

        if (direction == Direction.RIGHT)
        {
            //cambiar rotacion
            animator.SetBool("Turn", true);
            sprite.flipY = false;
            sprite.flipX = true;
            if (hability == Hability.ATTRACT)
            {
                PSAttractU.SetActive(false);
                PSAttractD.SetActive(false);
                PSAttractR.SetActive(true);
                PSAttractL.SetActive(false);

                PSRepelU.SetActive(false);
                PSRepelD.SetActive(false);
                PSRepelR.SetActive(false);
                PSRepelL.SetActive(false);

                AttractAreaU.SetActive(false);
                AttractAreaD.SetActive(false);
                AttractAreaR.SetActive(true);
                AttractAreaL.SetActive(false);

                RepelAreaU.SetActive(false);
                RepelAreaD.SetActive(false);
                RepelAreaR.SetActive(false);
                RepelAreaL.SetActive(false);

            }
            else if (hability == Hability.REPEL)
            {
                PSAttractU.SetActive(false);
                PSAttractD.SetActive(false);
                PSAttractR.SetActive(false);
                PSAttractL.SetActive(false);

                PSRepelU.SetActive(false);
                PSRepelD.SetActive(false);
                PSRepelR.SetActive(true);
                PSRepelL.SetActive(false);

                AttractAreaU.SetActive(false);
                AttractAreaD.SetActive(false);
                AttractAreaR.SetActive(false);
                AttractAreaL.SetActive(false);

                RepelAreaU.SetActive(false);
                RepelAreaD.SetActive(false);
                RepelAreaR.SetActive(true);
                RepelAreaL.SetActive(false);

            }
            else
            {
                PSAttractU.SetActive(false);
                PSAttractD.SetActive(false);
                PSAttractR.SetActive(false);
                PSAttractL.SetActive(false);

                PSRepelU.SetActive(false);
                PSRepelD.SetActive(false);
                PSRepelR.SetActive(false);
                PSRepelL.SetActive(false);

                AttractAreaU.SetActive(false);
                AttractAreaD.SetActive(false);
                AttractAreaR.SetActive(false);
                AttractAreaL.SetActive(false);

                RepelAreaU.SetActive(false);
                RepelAreaD.SetActive(false);
                RepelAreaR.SetActive(false);
                RepelAreaL.SetActive(false);
            }
        }
        else if (direction == Direction.LEFT)
        {
            animator.SetBool("Turn", true);
            sprite.flipY = false;
            sprite.flipX = false;
            if (hability == Hability.ATTRACT)
            {
                PSAttractU.SetActive(false);
                PSAttractD.SetActive(false);
                PSAttractR.SetActive(false);
                PSAttractL.SetActive(true);

                PSRepelU.SetActive(false);
                PSRepelD.SetActive(false);
                PSRepelR.SetActive(false);
                PSRepelL.SetActive(false);

                AttractAreaU.SetActive(false);
                AttractAreaD.SetActive(false);
                AttractAreaR.SetActive(false);
                AttractAreaL.SetActive(true);

                RepelAreaU.SetActive(false);
                RepelAreaD.SetActive(false);
                RepelAreaR.SetActive(false);
                RepelAreaL.SetActive(false);
            }
            else if (hability == Hability.REPEL)
            {
                PSAttractU.SetActive(false);
                PSAttractD.SetActive(false);
                PSAttractR.SetActive(false);
                PSAttractL.SetActive(false);

                PSRepelU.SetActive(false);
                PSRepelD.SetActive(false);
                PSRepelR.SetActive(false);
                PSRepelL.SetActive(true);

                AttractAreaU.SetActive(false);
                AttractAreaD.SetActive(false);
                AttractAreaR.SetActive(false);
                AttractAreaL.SetActive(false);

                RepelAreaU.SetActive(false);
                RepelAreaD.SetActive(false);
                RepelAreaR.SetActive(false);
                RepelAreaL.SetActive(true);
            }
            else
            {
                PSAttractU.SetActive(false);
                PSAttractD.SetActive(false);
                PSAttractR.SetActive(false);
                PSAttractL.SetActive(false);

                PSRepelU.SetActive(false);
                PSRepelD.SetActive(false);
                PSRepelR.SetActive(false);
                PSRepelL.SetActive(false);

                AttractAreaU.SetActive(false);
                AttractAreaD.SetActive(false);
                AttractAreaR.SetActive(false);
                AttractAreaL.SetActive(false);

                RepelAreaU.SetActive(false);
                RepelAreaD.SetActive(false);
                RepelAreaR.SetActive(false);
                RepelAreaL.SetActive(false);
            }
        }
        else if (direction == Direction.UP)
        {

            animator.SetBool("Turn", false);
            sprite.flipY = true;
            sprite.flipX = false;
            if (hability == Hability.ATTRACT)
            {
                PSAttractU.SetActive(true);
                PSAttractD.SetActive(false);
                PSAttractR.SetActive(false);
                PSAttractL.SetActive(false);

                PSRepelU.SetActive(false);
                PSRepelD.SetActive(false);
                PSRepelR.SetActive(false);
                PSRepelL.SetActive(false);

                AttractAreaU.SetActive(true);
                AttractAreaD.SetActive(false);
                AttractAreaR.SetActive(false);
                AttractAreaL.SetActive(false);

                RepelAreaU.SetActive(false);
                RepelAreaD.SetActive(false);
                RepelAreaR.SetActive(false);
                RepelAreaL.SetActive(false);
            }
            else if (hability == Hability.REPEL)
            {
                PSAttractU.SetActive(false);
                PSAttractD.SetActive(false);
                PSAttractR.SetActive(false);
                PSAttractL.SetActive(false);

                PSRepelU.SetActive(true);
                PSRepelD.SetActive(false);
                PSRepelR.SetActive(false);
                PSRepelL.SetActive(false);

                AttractAreaU.SetActive(false);
                AttractAreaD.SetActive(false);
                AttractAreaR.SetActive(false);
                AttractAreaL.SetActive(false);

                RepelAreaU.SetActive(true);
                RepelAreaD.SetActive(false);
                RepelAreaR.SetActive(false);
                RepelAreaL.SetActive(false);
            }
            else
            {
                PSAttractU.SetActive(false);
                PSAttractD.SetActive(false);
                PSAttractR.SetActive(false);
                PSAttractL.SetActive(false);

                PSRepelU.SetActive(false);
                PSRepelD.SetActive(false);
                PSRepelR.SetActive(false);
                PSRepelL.SetActive(false);

                AttractAreaU.SetActive(false);
                AttractAreaD.SetActive(false);
                AttractAreaR.SetActive(false);
                AttractAreaL.SetActive(false);

                RepelAreaU.SetActive(false);
                RepelAreaD.SetActive(false);
                RepelAreaR.SetActive(false);
                RepelAreaL.SetActive(false);
            }
        }
        else if (direction == Direction.DOWN)
        {
            animator.SetBool("Turn", false);
            sprite.flipY = false;
            sprite.flipX = false;

            if (hability == Hability.ATTRACT)
            {
                PSAttractU.SetActive(false);
                PSAttractD.SetActive(true);
                PSAttractR.SetActive(false);
                PSAttractL.SetActive(false);

                PSRepelU.SetActive(false);
                PSRepelD.SetActive(false);
                PSRepelR.SetActive(false);
                PSRepelL.SetActive(false);

                AttractAreaU.SetActive(false);
                AttractAreaD.SetActive(true);
                AttractAreaR.SetActive(false);
                AttractAreaL.SetActive(false);

                RepelAreaU.SetActive(false);
                RepelAreaD.SetActive(false);
                RepelAreaR.SetActive(false);
                RepelAreaL.SetActive(false);

            }
            else if (hability == Hability.REPEL)
            {
                PSAttractU.SetActive(false);
                PSAttractD.SetActive(false);
                PSAttractR.SetActive(false);
                PSAttractL.SetActive(false);

                PSRepelU.SetActive(false);
                PSRepelD.SetActive(true);
                PSRepelR.SetActive(false);
                PSRepelL.SetActive(false);

                AttractAreaU.SetActive(false);
                AttractAreaD.SetActive(false);
                AttractAreaR.SetActive(false);
                AttractAreaL.SetActive(false);

                RepelAreaU.SetActive(false);
                RepelAreaD.SetActive(true);
                RepelAreaR.SetActive(false);
                RepelAreaL.SetActive(false);
            }
            else
            {
                PSAttractU.SetActive(false);
                PSAttractD.SetActive(false);
                PSAttractR.SetActive(false);
                PSAttractL.SetActive(false);

                PSRepelU.SetActive(false);
                PSRepelD.SetActive(false);
                PSRepelR.SetActive(false);
                PSRepelL.SetActive(false);

                AttractAreaU.SetActive(false);
                AttractAreaD.SetActive(false);
                AttractAreaR.SetActive(false);
                AttractAreaL.SetActive(false);

                RepelAreaU.SetActive(false);
                RepelAreaD.SetActive(false);
                RepelAreaR.SetActive(false);
                RepelAreaL.SetActive(false);
            }
        }


        if (transform.position != targetPosition && !animator.GetBool("Walk"))
        {
            animator.SetBool("Walk", true);
        }
        else if (transform.position == targetPosition && animator.GetBool("Walk"))
        {
            animator.SetBool("Walk", false);
        }
        if (transform.position != targetPosition)
        {
            timer += Time.deltaTime;
            if (timer > 0.35f)
            {
                timer = 0;
                SoundManager.PlaySound("Step");
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, currentSpeed * Time.deltaTime);

        if (PlayerInput.attractKey)
        {
            hability = Hability.ATTRACT;
            ActivateHablility();
            SoundManager.PlaySound("Iman");
        }
        else if (PlayerInput.repelKey)
        {
            SoundManager.PlaySound("Iman");
            hability = Hability.REPEL;
            ActivateHablility();
        }
        else
        {
            hability = Hability.NONE;
            currentSpeed = Speed;
            if (targetPosition == transform.position)
            {
                animator.SetBool("Atract", false);
                animator.SetBool("Repel", false);
            }
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
            if( _hits.collider.gameObject.tag == "CajaMetal" || _hits.collider.gameObject.tag == "ParedMetal" || _hits.collider.gameObject.tag == "CajaCandado" || _hits.collider.gameObject.tag == "CajaCandadoRoja" || _hits.collider.gameObject.tag == "CajaCandadoVerde")  {
                metalSurface = _hits.collider.gameObject;
                boxSprite = metalSurface.GetComponent<SpriteRenderer>();

                return true;            
            }
        }

        return false;
    }
    private bool StartRayCast() {

        bool _collision = false;

        

        if (direction == Direction.UP ) {
            RaycastHit2D hits = Physics2D.Raycast(transform.position, Vector2.up, 7.44f, obstacles);
            if (checkRaycastWithScenario(hits)) { _collision = true; }
        }
        else if (direction == Direction.DOWN) {
            RaycastHit2D hits = Physics2D.Raycast(transform.position, Vector2.down, 7.44f, obstacles);
            if (checkRaycastWithScenario(hits)) { _collision = true; }
        }
        else if (direction == Direction.RIGHT) {
            RaycastHit2D hits = Physics2D.Raycast(transform.position, Vector2.right, 7.44f, obstacles);
            if (checkRaycastWithScenario(hits)) { _collision = true; }
        }
        else if (direction == Direction.LEFT) {
            RaycastHit2D hits = Physics2D.Raycast(transform.position, Vector2.left, 7.44f, obstacles);
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
        else if (hability == Hability.REPEL && StartRayCast() && metalSurface.tag == "CajaMetal") {

            float distanceX = -100;
            float distanceY = -100;
            GameObject Collision;
            RaycastHit2D rh;

            if (direction == Direction.UP && !moveBox)
            {
                rh = Physics2D.Raycast(transform.position, Vector2.up, 0.93f, obstacles);
                if (rh.collider != null && rh.collider.gameObject.tag == "CajaMetal") {
                    distanceX = metalSurface.transform.position.x;
                    distanceY = metalSurface.transform.position.y + 3.72f;
                    RaycastHit2D hits = Physics2D.Raycast(new Vector2(metalSurface.transform.position.x, metalSurface.transform.position.y + 0.93f), Vector2.up, 2.79f, obstacles);
                    Collision = checkRaycastWithScenarioRepel(hits);
                    if (Collision != null)
                    {
                        distanceY = Collision.transform.position.y - 0.93f;
                    }
                    BoxTarget = new Vector3(distanceX, distanceY);
                }


            }
            else if (direction == Direction.DOWN && !moveBox)
            {
                rh = Physics2D.Raycast(transform.position, Vector2.down, 0.93f, obstacles);
                if (rh.collider != null && rh.collider.gameObject.tag == "CajaMetal")
                {
                    distanceX = metalSurface.transform.position.x;
                    distanceY = metalSurface.transform.position.y - 3.72f;
                    RaycastHit2D hits = Physics2D.Raycast(new Vector2(metalSurface.transform.position.x, metalSurface.transform.position.y - 0.93f), Vector2.down, 2.79f, obstacles);
                    Collision = checkRaycastWithScenarioRepel(hits);
                    if (Collision != null)
                    {
                        distanceY = Collision.transform.position.y + 0.93f;
                    }
                    BoxTarget = new Vector3(distanceX, distanceY);
                }
            }
            else if (direction == Direction.RIGHT && !moveBox)
            {
                rh = Physics2D.Raycast(transform.position, Vector2.right, 0.93f, obstacles);
                if (rh.collider != null && rh.collider.gameObject.tag == "CajaMetal")
                {
                    distanceX = metalSurface.transform.position.x + 3.72f;
                    distanceY = metalSurface.transform.position.y;
                    RaycastHit2D hits = Physics2D.Raycast(new Vector2(metalSurface.transform.position.x + 0.93f, metalSurface.transform.position.y), Vector2.right, 2.79f, obstacles);
                    Collision = checkRaycastWithScenarioRepel(hits);
                    if (Collision != null)
                    {
                        distanceX = Collision.transform.position.x - 0.93f;
                    }
                    BoxTarget = new Vector3(distanceX, distanceY);
                }
            }
            else if (direction == Direction.LEFT && !moveBox)
            {
                rh = Physics2D.Raycast(transform.position, Vector2.left, 0.93f, obstacles);
                if (rh.collider != null && rh.collider.gameObject.tag == "CajaMetal")
                {
                    distanceX = metalSurface.transform.position.x - 3.72f;
                    distanceY = metalSurface.transform.position.y;
                    RaycastHit2D hits = Physics2D.Raycast(new Vector2(metalSurface.transform.position.x - 0.93f, metalSurface.transform.position.y), Vector2.left, 2.79f, obstacles);
                    Collision = checkRaycastWithScenarioRepel(hits);
                    if (Collision != null)
                    {
                        distanceX = Collision.transform.position.x + 0.93f;
                    }
                    
                    BoxTarget = new Vector3(distanceX, distanceY);
                }
            }
            if (distanceX == -100 && distanceY == -100)
            {
                return;
            }
            moveBox = true;
        }
        else if (hability == Hability.ATTRACT && StartRayCast() && metalSurface.tag == "ParedMetal" || hability == Hability.ATTRACT && StartRayCast() && metalSurface.tag == "CajaCandado" || hability == Hability.REPEL && StartRayCast() && metalSurface.tag == "CajaCandadoRoja" || hability == Hability.REPEL && StartRayCast() && metalSurface.tag == "CajaCandadoVerde")
        {
            animator.SetBool("Atract", true);
            currentSpeed += 0.5f;
            if (direction == Direction.UP)
            {
                targetPosition = new Vector2(transform.position.x, metalSurface.transform.position.y - 0.93f);
            }
            else if (direction == Direction.DOWN)
            {
                targetPosition = new Vector2(transform.position.x, metalSurface.transform.position.y + 0.93f);
            }
            else if (direction == Direction.RIGHT)
            {
                targetPosition = new Vector2(metalSurface.transform.position.x - 0.93f, transform.position.y);
            }
            else if (direction == Direction.LEFT)
            {
                targetPosition = new Vector2(metalSurface.transform.position.x + 0.93f, transform.position.y);
            }
        }
        else if (hability == Hability.REPEL && StartRayCast() && metalSurface.tag == "ParedMetal" || hability == Hability.REPEL && StartRayCast() && metalSurface.tag == "CajaCandado" || hability == Hability.REPEL && StartRayCast() && metalSurface.tag == "CajaCandadoRoja" || hability == Hability.REPEL && StartRayCast() && metalSurface.tag == "CajaCandadoVerde")
        {
            currentSpeed += 0.5f;
            RaycastHit2D rh = Physics2D.Raycast(transform.position, Vector2.zero, 0);
            switch (direction)
            {
                case Direction.UP:
                    rh = Physics2D.Raycast(transform.position, Vector2.up, 0.93f, obstacles);
                    break;
                case Direction.DOWN:
                    rh = Physics2D.Raycast(transform.position, Vector2.down, 0.93f, obstacles);
                    break;
                case Direction.RIGHT:
                    rh = Physics2D.Raycast(transform.position, Vector2.right, 0.93f, obstacles);
                    break;
                case Direction.LEFT:
                    rh = Physics2D.Raycast(transform.position, Vector2.left, 0.93f, obstacles);
                    break;
                default:
                    break;
            }
        
            
            if (rh.collider != null )
            {
                if (rh.collider.gameObject.tag == "ParedMetal" || rh.collider.gameObject.tag == "CajaCandado")
                {

                    animator.SetBool("Repel", true);
                    currentSpeed += 0.5f;

                    float distanceX;
                    float distanceY;
                    GameObject Collision;
                    if (direction == Direction.UP)
                    {
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

    void sacudirCaja( )
    {
        float delta = Time.deltaTime * 1000;
        float TimeToWaitRot = 50;
        float waitedTimeRot = 0;

        waitedTimeRot += delta;
        if (metalSurface.tag == "CajaMetal" && TimeToWaitRot < WaitedTime)
        {

            metalSurface.transform.rotation = Quaternion.Euler(0, 0, Random.Range(-5, 5));
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;// que la velocidad del juego sea 0
        OptionMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;// que la velocidad del juego regrese a 1
        OptionMenu.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            if (direction == Direction.UP && hability == Hability.NONE)
            {
                targetPosition = new Vector2(transform.position.x, collision.gameObject.transform.position.y - 0.93f);
            }
            else if (direction == Direction.DOWN && hability == Hability.NONE)
            {
                targetPosition = new Vector2(transform.position.x, collision.gameObject.transform.position.y + 0.93f);
            }
            else if (direction == Direction.RIGHT && hability == Hability.NONE)
            {
                targetPosition = new Vector2(collision.gameObject.transform.position.x - 0.93f, transform.position.y);
            }
            else if (direction == Direction.LEFT && hability == Hability.NONE)
            {
                targetPosition = new Vector2(collision.gameObject.transform.position.x + 0.93f, transform.position.y);
            }
        }

        if (collision.gameObject.tag == "Placa")
        {
            SoundManager.PlaySound("Door");
            GameObject[] CajasCandado = GameObject.FindGameObjectsWithTag("CajaCandado");
            for (int i = 0; i < CajasCandado.Length; i++)
            {
                CajasCandado[i].SetActive(false);
            }

        }
        if (collision.gameObject.tag == "PlacaVerde")
        {
            SoundManager.PlaySound("Door");
            GameObject[] CajasCandado = GameObject.FindGameObjectsWithTag("CajaCandadoVerde");
            for (int i = 0; i < CajasCandado.Length; i++)
            {
                CajasCandado[i].SetActive(false);
            }
        }
        if (collision.gameObject.tag == "PlacaRoja")
        {
            SoundManager.PlaySound("Door");
            GameObject[] CajasCandado = GameObject.FindGameObjectsWithTag("CajaCandadoRoja");
            for (int i = 0; i < CajasCandado.Length; i++)
            {
                CajasCandado[i].SetActive(false);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            if (direction == Direction.UP && hability == Hability.NONE)
            {
                targetPosition = new Vector2(transform.position.x, collision.gameObject.transform.position.y - 0.93f);
            }
            else if (direction == Direction.DOWN && hability == Hability.NONE)
            {
                targetPosition = new Vector2(transform.position.x, collision.gameObject.transform.position.y + 0.93f);
            }
            else if (direction == Direction.RIGHT && hability == Hability.NONE)
            {
                targetPosition = new Vector2(collision.gameObject.transform.position.x - 0.93f, transform.position.y);
            }
            else if (direction == Direction.LEFT && hability == Hability.NONE)
            {
                targetPosition = new Vector2(collision.gameObject.transform.position.x + 0.93f, transform.position.y);
            }
        }
    }

}