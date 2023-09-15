using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{
    public string levelToLoad;
    public bool cleared;
    // Start is called before the first frame update
    void Start()
    {
        // When you open Map Scene, check if GameManager has ticked Level Passed
        // if it is cleared, run Cleared functinon, which makes needed changes to this object
        if(GameManager.manager.GetType().GetField(levelToLoad).GetValue(GameManager.manager).ToString() == "True")
        {
            Cleared(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Cleared(bool isClear)
    {
        if(isClear == true)
        {
            cleared = true;
            // set in GameManager right boolean value -> true 
            GameManager.manager.GetType().GetField(levelToLoad).SetValue(GameManager.manager, true);
            // set Stage Clear image to show
            transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().enabled = true;
            // because level is cleared, delete Level trigger from object circle collider, so you cant enter level again
            GetComponent<CircleCollider2D> ().enabled = false;
        }
    }
}

