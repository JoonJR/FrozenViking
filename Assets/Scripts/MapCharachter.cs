using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapCharachter : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.manager.currentLevel != "")
        {   
            // Some level has been passed
            GameObject.Find(GameManager.manager.currentLevel).GetComponent<LoadLevel>().Cleared(true);
            // currentLevel is something else than empty, so youve exited level scene
            // set new position to player
            transform.position = GameObject.Find(GameManager.manager.currentLevel).transform.GetChild(0).transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMove = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float verticalMove = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.Translate(horizontalMove, verticalMove, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("LevelTrigger"))
        {
            GameManager.manager.currentLevel = collision.gameObject.name;
            SceneManager.LoadScene(collision.gameObject.GetComponent<LoadLevel>().levelToLoad);
        }
    }
}
