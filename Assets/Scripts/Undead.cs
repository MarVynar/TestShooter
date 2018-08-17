using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;




//public class Undead : MonoBehaviour {
public class Undead : NetworkBehaviour
{

    public enum State { stay, walk, jump, shoot, hit, die }

    [SerializeField] AudioClip sfxWalk;
    [SerializeField] AudioClip sfxJump;
    [SerializeField] AudioClip sfxAttack;
    [SerializeField] AudioClip sfxDie;

  //  [SerializeField] private Controller controller;
    [SerializeField] private GameObject spawnPoint;
   // [SerializeField] private GameObject playerLogo;

    [SerializeField] private GameObject proj;
    [SerializeField] private GameManager gameManager;
   // [SerializeField] private GameObject cam;

    private AudioSource audiosource;
    private Animator animator;
    private Direction direction;
    private bool isJump = false;
    private Rigidbody rigidbody;

    private bool activePlayer = false;

    private float attackCounter = 0;
    private float jumpspeed = 6000f;
    private float speed = 200f; //100f
    public float Speed
    {
        get { return speed; }
        set { speed = value; }

    }
    /*
    private int healthPoints = 10;
    public int HealthPoints
    {
        get { return healthPoints; }
        set { healthPoints = value; }

    }
    */


    State state;
   
    public Vector3 spawnPosition;
 





  


    public override void OnStartLocalPlayer()
    {
        //  base.OnStartLocalPlayer();
       // playerLogo.SetActive(true);
        tag = "Player";
    }



    // Use this for initialization
    void Start () {
        //  spawnPosition = spawnPoint.transform.position;
        animator = GetComponent<Animator>();
        audiosource = GetComponent<AudioSource>();
        state = State.stay;
        rigidbody = GetComponent<Rigidbody>();
        if (transform.position.x > 0)
        {
         //   direction = Direction.left;
            transform.eulerAngles = new Vector3(0, 270, 0);
        }
        else
        if (transform.position.x < 0)
        {
         //   direction = Direction.right;
            transform.eulerAngles = new Vector3(0, 90, 0);
        }

    }
	
	// Update is called once per frame
	void FixedUpdate () {

        if (attackCounter > 0) attackCounter -= Time.deltaTime;

        if (tag != "Bot")
        {
            gameManager.GetPlayerPosition = transform.position;
        }

        //if (gameManager.GameState == GameState.multiplay || gameManager.GameState == GameState.singleplay)
        {

            if (gameManager.GameState == GameState.multiplay) {
                Debug.Log("Multiplay");
                if ( !isLocalPlayer)
                              {
                    Debug.Log("Not local");
                                return;
                             }

                else if (isLocalPlayer)
                {
                    Debug.Log("Local");
                    /*/////
                            if (Input.GetKey(KeyCode.UpArrow))
                            {
                                if (transform.position.x > 0)
                                    moveRight();
                                else
                                    moveLeft();
                            }

                            if (Input.GetKey(KeyCode.DownArrow))
                            {
                                if (transform.position.x > 0)
                                    moveLeft();
                                else
                                    moveRight();
                            }

                            if (Input.GetKey(KeyCode.LeftArrow))
                            {
                                //  player.moveLeft(); // mirror
                                // moveLeft();
                            }

                            if (Input.GetKey(KeyCode.RightArrow))
                            {
                                // moveRight();
                            }

                            if (Input.GetKeyDown(KeyCode.Space))
                            {
                                if (gameManager.GameState == GameState.multiplay)
                                    CmdAttack();
                                else
                                    Attack();
                            }
                            ////*/

                }

                        } // multi

             else {
                              Debug.Log("Singleplay");
                             if (tag == "Bot")
                             {
                                 runAi();
                             } 

                            else
                            // if (isLocalPlayer || (gameManager.GameState == GameState.singleplay))
                       //     if (activePlayer)
                                { 
                                     if (Input.GetKey(KeyCode.UpArrow))
                                     {
                                         if (transform.position.x > 0)
                                             moveRight();
                                         else
                                             moveLeft();
                                      }

                                    if (Input.GetKey(KeyCode.DownArrow))
                                    {
                                        if (transform.position.x > 0)
                                            moveLeft();
                                        else
                                            moveRight();
                                    }

                                    if (Input.GetKey(KeyCode.LeftArrow))
                                    {
                                        //  player.moveLeft(); // mirror
                                       // moveLeft();
                                    }

                                    if (Input.GetKey(KeyCode.RightArrow))
                                    {
                                       // moveRight();
                                    }

                                    if (Input.GetKeyDown(KeyCode.Space))
                                    {
                                     if (gameManager.GameState == GameState.multiplay)
                                             CmdAttack();
                                     else
                                              Attack();
                                    }

                            /*
                                    if (direction == Direction.left)
                                    {
                                        // transform.Rotate(0, 180, 0);
                                        transform.eulerAngles = new Vector3(0, 270, 0);
                                    }

                                    else if (direction == Direction.right)
                                    {
                                        //  transform.Rotate(0, 0, 0);
                                        transform.eulerAngles = new Vector3(0, 90, 0);
                                    }
                                    */
                        } // not bot
                    }   // not multi non local
                } // if game
                  // else 
      //  if (gameManager.GameState != GameState.multiplay && gameManager.GameState != GameState.singleplay) Destroy(gameObject);

    }
    /*
    public void jump()
    {
        if (state != State.jump && !isJump)
        {
            animator.Play("Jump");
            audiosource.PlayOneShot(sfxJump);
            state = State.jump;
            isJump = true;
            rigidbody.AddForce(new Vector3(0, jumpspeed, 0), ForceMode.Impulse); //200f
            //
           // rigidbody.detectCollisions = false;
            //

         //   state = State.stay;
        }
    }
    */
    public void moveLeft()
    {
        animator.Play("Walk");
        //audiosource.PlayOneShot(sfxWalk);
        state = State.walk;
        //  gameObject.transform.Rotate(new Vector3(0, -90, 0));
        //gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        direction = Direction.left;
       // transform.Translate(Vector3.forward * Time.deltaTime * speed);
         transform.Translate(Vector3.left * Time.deltaTime * speed);
      //  transform.eulerAngles = new Vector3(0, 270, 0);
        //  Debug.Log("PlLeft "+ direction);
        // state = State.stay;
    }


    public void moveRight()
    {
        animator.Play("Walk");
       // audiosource.PlayOneShot(sfxWalk);
        state = State.walk;
        //   gameObject.transform.Rotate(new Vector3(0, 90, 0));
       // gameObject.transform.rotation = new Quaternion(0, 0, 0, 1);
        direction = Direction.right;
       // transform.Translate(Vector3.forward * Time.deltaTime * speed);
         transform.Translate(Vector3.right * Time.deltaTime * speed);
       // transform.eulerAngles = new Vector3(0, 90, 0);
        //  Debug.Log("PlRight " + direction);
        // state = State.stay;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor") ;//&& collision.gameObject.transform.position.y < transform.position.y)
        {
            //Debug.Log("Floor");
            isJump = false;
            //
          //  rigidbody.detectCollisions =true;

            //
            state = State.stay;
        }

        if (collision.gameObject.tag == "Lava")
        {
            Debug.Log("player Lava");
            if (gameManager.GameState == GameState.multiplay)
                CmdPlayerDie();
            else
                PlayerDie();
        }

        if (collision.gameObject.tag == "Projectile")
        {
            Debug.Log("player Collision");
            if (gameManager.GameState == GameState.multiplay)
                CmdPlayerDie();
            else
                PlayerDie();
        }

    }

    [Command]
    public void CmdPlayerDie()
    {
        PlayerDie();
    }

    public void PlayerDie()
    {
        audiosource.PlayOneShot(sfxDie);
        animator.Play("Die");
        //  Destroy(gameObject);
        gameManager.endGame();

    }


    [Command]
    public void CmdAttack()
    {
        // proj = new Projectile(direction);
        /* audiosource.PlayOneShot(sfxAttack);
         animator.Play("Attack");

         GameObject newProj = Instantiate(proj) as GameObject; //new Projectile() ;
         newProj.tag = tag;
         newProj.transform.position = transform.position;

         newProj.transform.eulerAngles = new Vector3 (transform.eulerAngles.x , transform.eulerAngles.y+90, transform.eulerAngles.z);
        // newProj.GetComponent<Rigidbody>().velocity = newProj.transform.forward * 100; */
        GameObject newProj = Attack();
        NetworkServer.Spawn(newProj);
        //newProj.pDirection = direction;
    }


    public GameObject Attack()
    {

        if (attackCounter <= 0)
        {
            attackCounter = 0.5f;
            audiosource.PlayOneShot(sfxAttack);
            animator.Play("Attack");

            GameObject newProj = Instantiate(proj) as GameObject; //new Projectile() ;
            newProj.tag = tag;
            newProj.transform.position = transform.position;

            newProj.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 90, transform.eulerAngles.z);
            return newProj;
            // newProj.GetComponent<Rigidbody>().velocity = newProj.transform.forward * 100;

        }

        else
            return null;
    }

    IEnumerator Ai(float target)
    {
        //float target = gameManager.GetPlayerPosition.z;
       // while (transform.position.z != gameManager.GetPlayerPosition.z)
        {
          
            if (transform.position.z > target)
            {

                if (transform.position.x < 0)
                    moveRight();
                else
                    moveLeft();
            }

            else if (transform.position.z < target)
            {

                if (transform.position.x < 0)
                    moveLeft();
                else
                    moveRight();
            }

            if (Mathf.Abs(transform.position.z - target) < 5)
            {
                Attack();
              //  StartCoroutine(Ai());
            }

           //  yield return null;
            // StartCoroutine(Ai(gameManager.GetPlayerPosition.z));
        }
        //*/


        yield return new WaitForSeconds(0.5f);
        float newtarget = gameManager.GetPlayerPosition.z;
         StartCoroutine(Ai(newtarget));

    }

    public void runAi()
    {
        Debug.Log("Ai");

        StartCoroutine(Ai(gameManager.GetPlayerPosition.z));
      //  float target = gameManager.GetPlayerPosition.z;
      /*/  while (transform.position.z != gameManager.GetPlayerPosition.z)
        {

           if (transform.position.z > target){

                if (transform.position.x < 0)
                    moveRight();
                else
                    moveLeft();
            }

           else if (transform.position.z < target){

                if (transform.position.x < 0)
                    moveLeft();
                else
                    moveRight();
            }

           if ( Mathf.Abs (transform.position.z - target) < 5)
            {
                Attack();
            }


        }*/


    }

}
