using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaActivable : MonoBehaviour
{
    public float Velocidad = 1;
    public Transform Objetivo;
    private Vector3 Inicio, Fin;
    public GameObject Player;
    private float Tiempo;
    private bool Encendedor;
    private float x, y;
    void Start()
    {
        x = transform.position.x;
        y = transform.position.y;
        //Evita que el objetivo se mueva con la plataforma quitandole la jerarquia de hijo
        if (Objetivo != null)
        {
            Objetivo.parent = null;
            Inicio = transform.position;
            Fin = Objetivo.position;

        }
    }
    private void FixedUpdate()
    {
        if (Encendedor == true && Time.time > Tiempo + 3f)
        {
            transform.position = new Vector3(x,y,transform.position.z);
            Encendedor = false;
        }
    }
    private void Moverse()
    {
        //Movemos la plataforma hasta el objetivo
        if (Objetivo != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, Objetivo.position, Velocidad * Time.deltaTime);
        }
        //Al llegar al objetivo intercambiamos el inicio y el fin
        if (transform.position == Objetivo.position)
        {
            Objetivo.position = (Objetivo.position == Inicio) ? Fin : Inicio;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
 

        if (collision.gameObject.CompareTag("Player"))
         {
             Tiempo = Time.time;
             Moverse();
         }

        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Tiempo = Time.time;
            Encendedor = true;
        }
        
    }
}
