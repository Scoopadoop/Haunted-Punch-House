using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LifeCounter : MonoBehaviour {

    // Use this for initialization
    public static int lives;
    public static List<GameObject> SaveState = new List<GameObject>();
    
    void OnLevelWasLoaded(int level)
    {
        if(level == 0)
        {
            if (lives <= 2)
            {
                Destroy(GameObject.Find("Life 3"));
            }
            if (lives <= 1)
            {
                Destroy(GameObject.Find("Life 2"));
            }
            if (lives <= 0)
            {
                Destroy(GameObject.Find("Life 1"));
            }
        }
        foreach(GameObject enemy in SaveState)
        {
            Destroy(enemy);
        }
        
    }
	void Start () {
        lives = 3;
        DontDestroyOnLoad(gameObject);

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
