using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lanza : MonoBehaviour
{
    private Animator animator;
    private float TiempoEspera = 0.0f;
    private int contador;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    
    void Update()
    {
       
    }
    private void FixedUpdate()
    {
        if (Time.time> TiempoEspera)
        {
            TiempoEspera = Time.time + 3f;
        }
         animator.SetBool("Tiempo_espera", Time.time == TiempoEspera);
    }
    public void encender()
    {
        GetComponent<BoxCollider2D>().enabled = true;
    }
    public void Apagar()
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
