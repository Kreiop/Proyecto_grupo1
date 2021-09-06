using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ave_Volando : MonoBehaviour
{
    private bool Cambio;
    private Animator anim;

    public float Velocidad = 1;
    public Transform Objetivo;
    private Vector3 Inicio, Fin;
    public float direccion;
    void Start()
    {

        anim = GetComponent<Animator>();
        if (Objetivo != null)
        {
            if (transform.position.x > Objetivo.position.x)
            {
                direccion = -1;
            }
            Objetivo.parent = null;
            transform.localScale = new Vector3(direccion, 1.0f, 1.0f);
            Inicio = transform.position;
            Fin = Objetivo.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Cambis", Cambio);
    }
    public void Cambiar()
    {
        Cambio = true;
    }
    public void Regresar()
    {
        Cambio = false;
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
            transform.localScale = new Vector3(-1.0f * direccion, 1.0f, 1.0f);
            Objetivo.position = (Objetivo.position == Inicio) ? Fin : Inicio;
            direccion = direccion * -1.0f;
        }
    }
}
