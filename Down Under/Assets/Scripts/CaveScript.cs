using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CaveScript : MonoBehaviour{

    public bool isIn;
    public GameObject dialogueBox;

    // Start is called before the first frame update
    void Start()
    {
        isIn = false;
        dialogueBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(isIn == true)
        {
            dialogueBox.SetActive(true);
        }


    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            isIn = true;
        }
    }

    public void EnterCave()
    {
        SceneManager.LoadScene("Cave");
    }
}
