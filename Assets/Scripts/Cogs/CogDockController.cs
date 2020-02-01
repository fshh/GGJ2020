using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogDockController : MonoBehaviour
{
    public Transform cogPosit;
    private bool occupied;
    // Start is called before the first frame update
    void Start()
    {
        occupied = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if(!occupied && collider.gameObject.CompareTag("Cog") && collider.gameObject.layer == gameObject.layer) {
            occupied = true;
            collider.gameObject.transform.parent = cogPosit;
            collider.gameObject.transform.position = cogPosit.position;
            collider.gameObject.layer = LayerMask.NameToLayer("Default");
            collider.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        }
    }

    public void removeCog() {
        occupied = false;
    }
}
