using System.Collections;
using TMPro;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlay : MonoBehaviour
{
    public List<GameObject> UIitems = new List<GameObject>();
    public TextMeshProUGUI buttonText;
    public GameObject TutScreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleTut()
    {
        //if the tutorial is not on screen
        if (!TutScreen.activeInHierarchy)
        {
            foreach(GameObject uiItem in UIitems)
            {
                //turn off the standard UI items
                uiItem.SetActive(false);

                //and turn on the tutorial item
                TutScreen.SetActive(true);
            }
        }
        else if (TutScreen.activeInHierarchy)
        {
            foreach(GameObject uiItem in UIitems)
            {
                uiItem.SetActive(true);
                TutScreen.SetActive(false);
            }
        }
    }
}
