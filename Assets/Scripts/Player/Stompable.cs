using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Stompable : MonoBehaviour {

    private Stunnable stunControl;

	// Use this for initialization
	void Start () {
        Transform parent = transform.parent;
        stunControl = parent.GetComponent<Stunnable>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject enemyPlayer = collision.gameObject;
        PlayerMovement enemyPlayerMovement = enemyPlayer.GetComponent<PlayerMovement>();
        if (enemyPlayerMovement && enemyPlayerMovement.IsFalling() && enemyPlayer.layer != transform.parent.gameObject.layer)
        {
            if (stunControl.Stun())
            {
                enemyPlayerMovement.Bounce();
            }
        }
    }

    public void Kill() {
        Destroy(transform.parent.gameObject);
    }
}
