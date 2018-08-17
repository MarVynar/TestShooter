using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Projectile : NetworkBehaviour {

    // private Collider collider;

    private float speed = 1000f;

    /*
    public Projectile (Direction dir)
    {
        direction = dir;
    }*/

    //   private Direction direction;
    public Direction direction;
    public Direction pDirection
    {
        set { direction = value; }
        get { return direction; }
    }

    public void setDirection (Direction dir)
    {
        direction = dir;
    }

    //private Rigidbody rigidbody;

	// Use this for initialization
	void Start () {

       // rigidbody = GetComponent<Rigidbody>();
          Debug.Log("Spawned");

          if (direction == Direction.left)
          {
              Debug.Log("Left");
              transform.Rotate(new Vector3( 0, -90, 0));   
          }

          else if (direction == Direction.right)
          {
              Debug.Log("Right");
              transform.Rotate(new Vector3(0, 90, 0));

          }


     Debug.Log("Shoot");
          StartCoroutine(Move());

    }

    // Update is called once per frame
    void Update () {
		
	}

    void OnCollisionEnter(Collision collision)
    {
       // Debug.Log("proj Collision");
        // if (collision.gameObject.tag != "Untagged")
        //   if (collision.gameObject.tag == (( tag == "Player1")? "Player2" : "Player1"))
        /*if (collision.gameObject.tag == /*"Player1" || collision.gameObject.tag == / "Player")
           {
            Debug.Log("player Collision");

        }
        */
           Destroy(gameObject);

       // Debug.Log("proj Collision");



    }

    IEnumerator Move()
    {

      while (true)
        {
            Debug.Log("Moving");
            /* if (direction == Direction.left)
             {
                 transform.Translate(Vector3.forward * Time.deltaTime);
             }

             else if (direction == Direction.right)
             {
                 transform.Translate(Vector3.forward * Time.deltaTime);
             }
             */
              transform.Translate(Vector3.forward *  (Time.deltaTime *100f) ); //1000f

           // transform.position += Vector3.forward * Time.deltaTime* speed;
          
                
        yield return null;
        }
      //  yield return null;
    }


}
