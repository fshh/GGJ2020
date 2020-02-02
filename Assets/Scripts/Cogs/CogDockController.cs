using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogDockController : MonoBehaviour
{
    public Transform cogPosit;
    public int team;

    public GameController gameController;
    private bool occupied;
    private int ballLayer;
    // Start is called before the first frame update
    void Start()
    {
        occupied = false;
        ballLayer = team == 1 ? LayerMask.NameToLayer("Ball1") : LayerMask.NameToLayer("Ball2");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider) {
        Debug.Log("Collision with Dock detected");
        if(!occupied && collider.gameObject.CompareTag("Cog") && collider.gameObject.layer == ballLayer) {
            Debug.Log("Gear in Dock");
            occupied = true;
            collider.gameObject.GetComponent<CogWheel>().DockToggle();
            collider.gameObject.transform.parent = cogPosit;
            collider.gameObject.transform.position = cogPosit.position;
            collider.gameObject.layer = LayerMask.NameToLayer("Default");
            collider.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            gameController.AddCog(team);
        }
    }

    public void RemoveCog() {
        occupied = false;
        gameController.SubtractCog(team);
    }
}
