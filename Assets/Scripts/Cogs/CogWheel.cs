﻿using System.Collections;
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
        if(collision.gameObject.layer == 9)
        {
            //it has hit a wall or something else
            gameObject.layer = 10;
        }
    }


    public void Stun()
    {
        //check the object's velocity
    }

    public void IgnorePlayers(GrabCog thrower)
    {
        gameObject.layer = thrower.gameObject.layer;
        
        
    }

    public void ResetIgnore()
    {
        gameObject.layer = 0;
    }

    public void DockToggle() {
        docked = !docked;
    }

    public bool isDocked() {
        return docked;
    }

}