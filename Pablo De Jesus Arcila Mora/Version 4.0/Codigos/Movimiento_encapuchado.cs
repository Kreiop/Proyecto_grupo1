using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento_encapuchado : MonoBehaviour
{
    //copia
    public float Velocidad = 0.4f;
    public Transform Objetivo;
    private Vector3 Inicio, Fin;
    public float direccion = 1.0f;

    public GameObject personaje;
    private Animator Animator;
    public bool detectado;

    public GameObject BolaDeFuego;
    public Transform mano;
    public float tiempo;
    private int contador = 1;
    private bool PrimerAtaque;
    private int contadorAuxiliar = 0;

    void Start()
    {
        Animator = GetComponent<Animator>();
        

        //Evita que el objetivo se mueva con la plataforma quitandole la jerarquia de hijo
        if (Objetivo != null)
        {
            Objetivo.parent = null;
            transform.localScale = new Vector3(direccion, 1.0f, 1.0f);
            Inicio = transform.position;
            Fin = Objetivo.position;
        }
    }
    private void FixedUpdate()
    {
        // Las siguientes lineas determinan si el jugador esta delante de el brujo para pasar al ataque
        Vector3 lado = personaje.transform.position - transform.position;

        Debug.DrawRay(transform.position , Vector3.left * 4f, Color.black);

        Debug.DrawRay(transform.position, Vector3.right * 4f, Color.black);

        RaycastHit2D Conflictoizquierdo = Physics2D.Raycast(transform.position, Vector3.left, 4f, 1<<7);
        RaycastHit2D Conflictoderecho = Physics2D.Raycast(transform.position, Vector3.right, 4f, 1 << 7);

        if (Conflictoizquierdo.collider != null)
        {
            if (Conflictoizquierdo.transform.tag == "Player")
            {
                if (contadorAuxiliar == 0)
                {
                    PrimerAtaque = true;
                }
                else if (contadorAuxiliar == 1)
                {
                    PrimerAtaque = false;
                }
                detectado = true;
                if (contador == 1)
                {
                    tiempo = Time.time;
                    contador = 0;
                }
                Disparo();

            }
            else
            {
                detectado = false;
                
            }
        }

        else if (Conflictoderecho.collider != null)
        {
            if (Conflictoderecho.transform.tag == "Player")
            {

                detectado = true;
                if (contador == 1)
                {
                    tiempo = Time.time;
                    contador = 0;
                }
                Disparo();
            }
            else
            {
                detectado = false;
                
            }
        }

        else
        {
            detectado = false;
            
        }


        Animator.SetBool("Atacando", detectado == true);
        if (detectado == false)
        {
            contador = 1;

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
        else if (lado.x >= 0.0f && detectado == true)
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        }

        else if (lado.x <= 0.0f && detectado == true)
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

        }
    }
    private void Disparo()
    {
        Vector2 direccion = Vector2.right;
        if (transform.localScale.x == -1.0f)
        {
            direccion = Vector2.left;
        }
        else if(transform.localScale.x == 1.0f)
        {
            direccion = Vector2.right;
        }
        if (PrimerAtaque == true)
        {
            if (Time.time >= tiempo + 0.4f)
            {
                GameObject bola = Instantiate(BolaDeFuego, mano.position, Quaternion.identity);
                bola.GetComponent<BolaDeFuego>().SetDireccion(direccion);
                contador = 1;
                contadorAuxiliar = 1;
            }
        }
        else if (PrimerAtaque == false)
        {
            if (Time.time >= tiempo + 0.1f)
            {
                GameObject bola = Instantiate(BolaDeFuego, mano.position, Quaternion.identity);
                bola.GetComponent<BolaDeFuego>().SetDireccion(direccion);
                contador = 1;

            }
        }
        
    }
}
