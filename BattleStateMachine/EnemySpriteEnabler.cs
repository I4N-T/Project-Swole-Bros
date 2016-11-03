using UnityEngine;
using System.Collections;

public class EnemySpriteEnabler : MonoBehaviour {

    public string enemyName;
   
    public SpriteRenderer sr;


    // Use this for initialization
    void Start () {
        enemyName = EnemyObjectDisable.instance2.enemyStatsScript.eName;
        sr = GetComponent<SpriteRenderer>();
        //spriteEnable();
    }

    void spriteEnable()
    {
        if (enemyName == "Alien Goo")
        {
            sr.enabled = true;
        }
        else if (enemyName != "Alien Goo")
        {
            sr.enabled = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
        spriteEnable();

	}
}
