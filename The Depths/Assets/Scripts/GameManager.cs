using Gamekit2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public LevelXP xp;

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
        }
    }


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
