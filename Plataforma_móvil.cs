using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma_móvil : MonoBehaviour
{
    public int Velocidad = 1;
    public Transform Objetivo;
    private Vector3 Fin, Inicio;
    void Start()
    {
        //Evita que el objetivo se mueva con la plataforma quitandole la jerarquia de hijo
        if (Objetivo != null)
        {
            Objetivo.parent = null;
            Fin = transform.position;
            Inicio = Objetivo.position;
        }
    }
    private void FixedUpdate()
    {
        //Movemos la plataforma hasta el objetivo
        if (Objetivo != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, Objetivo.position, Velocidad * Time.deltaTime);
        }
        //Al llegar al objetivo intercambiamos el inicio y el fin
        if (transform.position == Objetivo.position)
        {
            Objetivo.position = (Objetivo.position == Fin) ? Inicio : Fin;
        }
    }
}
