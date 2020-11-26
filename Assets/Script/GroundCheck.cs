using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{

    public Megaman player;
    public MovingPlat mov;

    public Vector3 movep;

    // Use this for initialization
    void Start()
    {
        mov = GameObject.FindGameObjectWithTag("MovingPlat").GetComponent<MovingPlat>();
        player = gameObject.GetComponentInParent<Megaman>();
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger == false)
            player.Ground = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        /*if (collision.isTrigger == false || collision.CompareTag("water"))
            player.Ground = true;*/
        if (collision.isTrigger == false && collision.CompareTag("MovingPlat"))
        {
            movep = player.transform.position;
            movep.x += mov.speed;
            player.transform.position = movep;
        }

    }

    /*private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.isTrigger == false || collision.CompareTag("water"))
            player.Ground = false;
    }*/
}