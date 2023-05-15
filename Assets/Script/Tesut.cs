using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tesut : MonoBehaviour
{
    GameManager gm;

    public void Start()
    {
        gm = GetComponent<GameManager>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            gm.DetBoosEnemy();
        }

        if(Input.GetKeyDown(KeyCode.N))
        {
            gm._count++;
            gm.GoPlay(gm._count);
        }
    }
}
