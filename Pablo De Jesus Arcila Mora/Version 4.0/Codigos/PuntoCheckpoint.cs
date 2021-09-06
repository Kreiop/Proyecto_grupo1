using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuntoCheckpoint : MonoBehaviour
{
    public int Contador = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Contador ==1)
        {
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<Respawn>().PuntoAparacion(transform.position.x, transform.position.y);
                GetComponent<Animator>().enabled = true;

            }
            Contador = 0;
        }
            
    }
}
