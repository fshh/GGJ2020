using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CogWheel : MonoBehaviour
{
    //aggresor is the one who threw the cog

    public float throwSpeed;
    public float throwHeight;
    public GrabCog carrier;
    public Rigidbody2D myRB;


    public void Start()
    {
       
        //Physics.IgnoreCollision(GetComponent<Collider>(), agressor.myTeammate.GetComponent<Collider>());
        myRB = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        if(myRB.velocity.x <= 1)
        {
            ResetIgnore();
        }
    }


    public void Stun()
    {
        //check the object's velocity
    }

    public void IgnorePlayers(GrabCog thrower)
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), thrower.GetComponent<Collider2D>());
        //Physics2D.IgnoreCollision(GetComponent<Collider2D>(), thrower.myTeamMate.GetComponent<Collider2D>());
    }

    public void ResetIgnore()
    {
      
    }

}
