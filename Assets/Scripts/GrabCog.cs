using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabCog : MonoBehaviour
{
    //public bool canGrab;
    //this is the nonPhysics cog that the player displays when carrying one
    //float grabThrowCD = 0.1f;
    public CogWheel myCog;
    public Transform heldCogPosit;
    public CogWheel cogNearMe;
    public GrabCog myTeamMate;
    //this is the cog on the ground that gets destoryed when a player picks one up

    // Start is called before the first frame update
    void Start()
    {
        myCog = null;
        cogNearMe = null;
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<CogWheel>())
        {
            Debug.Log("touched");
            cogNearMe = collision.transform.parent.GetComponent<CogWheel>();
        }
    }


    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponentInParent<CogWheel>())
        {
            cogNearMe = null;
        }
    }
  

    public void PickUp()
    {
        
        myCog = cogNearMe;
        cogNearMe = null;
        myCog.transform.parent = transform;
        myCog.transform.position = heldCogPosit.position;
        myCog.myRB.bodyType = RigidbodyType2D.Kinematic;
        myCog.GetComponent<Collider2D>().enabled = false;
    }

    public void ThrowCog()
    {
        //LayerMask teamToIgnore = LayerMask.NameToLayer()
        myCog.transform.parent = null;
        myCog.myRB.bodyType = RigidbodyType2D.Dynamic;
        myCog.GetComponent<Collider2D>().enabled = true;
        //set the physics to ignore this player's teammate
        myCog.IgnorePlayers(this);
        //throw the sucker
        myCog.myRB.AddForce(new Vector2(myCog.throwSpeed, myCog.throwHeight));
        myCog = null;
    }
}
