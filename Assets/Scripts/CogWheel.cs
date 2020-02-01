using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogWheel : MonoBehaviour
{
    //the cog needs to be throwable
    //lock into place when hitting the machine
    //physics on throw
    public Rigidbody myRB;
    public ForceMode forceMode;

    //public Player currPlayer
    //currPlayer is the player that is currently holding the cogWheel, can be null

    //public Machine myMach;
    //myMach is in reference to the machine this cog is currently installed i

    public LayerMask team;

    public float throwSpeed;
    public float throwHeight;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickUp(GameObject parent)//a player has approached the cog and pressed a button to grab it
    {
        //first, turn off the collider
        GetComponent<Collider>().enabled = false;

        //then, set the cog wheel to a child of the parent
        //transform.position = parent.cogSnap;

        //set the object's rotation
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    public void ThrowCog()//(Player currplayer)
    {
        gameObject.GetComponent<Collider>().enabled = true;
        //impart physics onto the CogWheel 
        //make it fly at throwSpeed
        //should be a matter of myRB.Addforce
        myRB.AddForce(throwSpeed, throwHeight, 0, forceMode);

        //recognize this player and that player's teammate
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        //check first if we hit a player
        //if(collision.gameObject.GetComponent<Player>()
        {
            
        }

        //check if we hit the target
        //if(Collision.gameObject.GetComponent<Target>()
        {

        }
    }

    public void NewTeam()
    {
        //the cog wheel has been grabbed. Let's associate it with a team
        //gameObject.layer = currPlayer.gameObject.layer;
    }


}
