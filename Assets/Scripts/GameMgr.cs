using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    // Start is called before the first frame update
    Character player;
    void Start()
    {
       player = GameObject.Find("Player").GetComponent<Character>();
       player.OnStart();
    }

    // Update is called once per frame
    void Update()
    {
        player.OnUpdate();
    }
}
