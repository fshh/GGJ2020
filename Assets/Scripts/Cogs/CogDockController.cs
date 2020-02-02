using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogDockController : MonoBehaviour
{
    public Transform cogPosit;
    public int team;

    public SpriteRenderer leftLight;
    public SpriteRenderer rightLight;
    public List<GameObject> Lights = new List<GameObject>();

    public GameController gameController;
    private bool occupied;
    private int ballLayer;

    public AudioSource machineSound;
    public AudioClip runningSound;
    public AudioClip stoppedSound;
    // Start is called before the first frame update
    void Start()
    {
        leftLight.color = Color.grey;
        rightLight.color = Color.grey;
        foreach(GameObject light in Lights)
        {
            light.SetActive(false);
        }
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
            AkSoundEngine.PostEvent("SFX_ENV_GearPlacedinTerminal", gameObject);
            collider.gameObject.GetComponent<CogWheel>().DockToggle();
            collider.gameObject.transform.parent = cogPosit;
            collider.gameObject.transform.position = cogPosit.position;
            collider.gameObject.layer = LayerMask.NameToLayer("Neutral");
            collider.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            gameController.AddCog(team);

            leftLight.color = Color.yellow;
            rightLight.color = Color.yellow;
            foreach (GameObject light in Lights)
            {
                light.SetActive(true);
            }
        }
    }

    public void RemoveCog() {
        leftLight.color = Color.grey;
        rightLight.color = Color.grey;
        foreach (GameObject light in Lights)
        {
            light.SetActive(false);
        }
        occupied = false;
        AkSoundEngine.PostEvent("SFX_ENV_GearTakenFromTerminal", gameObject);
        gameController.SubtractCog(team);
    }
}
