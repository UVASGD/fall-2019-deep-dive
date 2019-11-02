using Gamekit2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public LevelXP xp;

    // Start is called before the first frame update
    void Start()
    {
        xp = GetComponentInChildren<LevelXP>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
