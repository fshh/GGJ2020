using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class CogWheel : MonoBehaviour
{
    //aggresor is the one who threw the cog

    public float throwSpeed;
    public float throwHeight;
    public GrabCog carrier;
    public Rigidbody2D myRB;
    public AudioSource cogSound;
    public AudioClip cogHit;
    public GameObject cogchild;
    public SpriteRenderer cogsprite;
    public TrailRenderer myTrail;

    private bool docked;
    //public Team team


    public void Start()
    {

        //Physics.IgnoreCollision(GetComponent<Collider>(), agressor.myTeammate.GetComponent<Collider>());
        myRB = GetComponent<Rigidbody2D>();
        docked = false;
    }

    public void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Machine>())
        {
            //check if this machine belongs to the thrower
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Platform"))
        {
            cogSound.PlayOneShot(cogHit);
            myTrail.startColor = Color.grey;
            myTrail.endColor = Color.grey;
            ResetIgnore();
        }

        //if the layer is not neutral
        if(gameObject.layer != LayerMask.NameToLayer("Neutral"))
        {
            if (collision.gameObject.GetComponent<Stunnable>())
            {
                collision.gameObject.GetComponent<Stunnable>().Stun();
            }
        }
    }


    public void Stun()
    {
        //check the object's velocity
    }

    public void IgnorePlayers(GrabCog thrower)
    {
        if(thrower.gameObject.layer == LayerMask.NameToLayer("Team1"))
        {
            gameObject.layer = LayerMask.NameToLayer("Ball1");
        }
        else if(thrower.gameObject.layer == LayerMask.NameToLayer("Team2"))
        {
            gameObject.layer = LayerMask.NameToLayer("Ball2");
        }
        
        
    }

    public void ResetIgnore()
    {
        gameObject.layer = LayerMask.NameToLayer("Neutral");
    }

    public void DockToggle() {
        docked = !docked;
       GetComponent<CircleCollider2D>().enabled = !GetComponent<CircleCollider2D>().enabled;

        Debug.Log(docked);
    }

    public bool isDocked() {
        return docked;
    }

}
