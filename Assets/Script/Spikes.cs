using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public Megaman player;
    public Enemy bot;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Megaman>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            player.Damage(2);
            player.Knockback(350f, player.transform.position);
        }
        else if (col.CompareTag("Enemy"))
        {
            bot.Damage(2);
            bot.Knockback(350f, player.transform.position);
        }
    }
 }
