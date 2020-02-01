using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Stompable : MonoBehaviour {

    private PlayerInput input;
    private AudioSource audioSource;
    private Animator anim;

	// Use this for initialization
	void Start () {
        input = transform.parent.GetComponent<PlayerInput>();
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    /*
	// Update is called once per frame
	void Update () {
		if (bounceColl && bounceColl.IsTouching(playerColl) && !player.IsGrounded()) {
            player.Bounce();
            audioSource.Play();
            anim.SetTrigger("Die");
            GameObject colls = transform.parent.Find("Colliders").gameObject;
            GetComponentInParent<Controller2D>().enabled = false;
            if (GetComponentInParent<Slime>()) {
                GetComponentInParent<Slime>().enabled = false;
            } else if (GetComponentInParent<Snail>()) {
                GetComponentInParent<Snail>().enabled = false;
            }
            Destroy(colls);
        }
	}
    */

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement enemyPlayer = collision.gameObject.GetComponent<PlayerMovement>();
        if (enemyPlayer && enemyPlayer.IsFalling())
        {
            enemyPlayer.Bounce();
        }
    }

    public void Kill() {
        Destroy(transform.parent.gameObject);
    }
}
