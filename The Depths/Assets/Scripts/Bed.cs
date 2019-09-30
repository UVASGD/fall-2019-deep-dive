using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : Interactable
{
    Transform bed_point;

    private void Start()
    {
        bed_point = transform.Find("bed_point");
        if (!bed_point) bed_point = Instantiate(new GameObject(), transform).transform;
        bed_point.name = "bed_point";
    }

    public override void Interact()
    {
        //GameManager.instance.player.Sleep(bed_point);
        print("I'm a bed, haha!");
    }
}
