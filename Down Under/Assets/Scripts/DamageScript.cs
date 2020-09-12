using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour{

    public Canvas hitText;

    // Start is called before the first frame update
    void Start()
    {
        hitText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(HitScript.hit == true)
        {
            hitText.gameObject.SetActive(true);
            Debug.Log("Hit");
        }
        else if(HitScript.hit == false)
        {
            hitText.gameObject.SetActive(false);
        }
    }
}
