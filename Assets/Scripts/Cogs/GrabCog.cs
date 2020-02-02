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
    public int throwDir;
    //this is either 1 or -1. 1 for right, -1 for left
    private ContactFilter2D contactFilter;


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
        // change to get component for the radius if this works
        if (cogNearMe != null && Vector2.Distance(transform.position, cogNearMe.transform.position) > 1) {
            cogNearMe = null;
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<CogWheel>())
        {
            Debug.Log("touched");
            cogNearMe = collision.GetComponentInParent<CogWheel>();
        }
    }


    /*public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<CogWheel>())
        {
            //Debug.Log("Stepped away");
            cogNearMe = null;
        }
    }*/
  

    public void PickUp()
    {
        myCog = cogNearMe;
        if (myCog.isDocked()) {
            myCog.DockToggle();
            myCog.transform.parent.gameObject.transform.parent.GetComponent<CogDockController>().RemoveCog();
        }
        myCog.transform.GetChild(0).gameObject.GetComponent<Collider2D>().enabled = false;
        cogNearMe = null;
        myCog.transform.parent = heldCogPosit;
        myCog.transform.position = heldCogPosit.position;
        myCog.myRB.bodyType = RigidbodyType2D.Kinematic;
        myCog.GetComponent<Collider2D>().enabled = false;
    }

    public void ThrowCog()
    {
        //LayerMask teamToIgnore = LayerMask.NameToLayer()
        myCog.IgnorePlayers(this);
        myCog.transform.parent = null;
        myCog.myRB.bodyType = RigidbodyType2D.Dynamic;
        myCog.GetComponent<Collider2D>().enabled = true;
        myCog.transform.GetChild(0).gameObject.GetComponent<Collider2D>().enabled = true;
        //set the physics to ignore this player's teammate
        //throw the sucker
        myCog.myRB.velocity = new Vector2(myCog.throwSpeed * throwDir, myCog.throwHeight);
        myCog = null;
    }

    public void Drop()
    {
        if (myCog != null)
        {
            myCog.gameObject.layer = LayerMask.NameToLayer("Neutral");
            myCog.transform.parent = null;
            myCog.myRB.bodyType = RigidbodyType2D.Dynamic;
            myCog.GetComponent<Collider2D>().enabled = true;
            //set the physics to ignore this player's teammate
            //throw the sucker
            //myCog.myRB.velocity = new Vector2(myCog.throwSpeed, myCog.throwHeight);
            myCog = null;
        }
    }
}
