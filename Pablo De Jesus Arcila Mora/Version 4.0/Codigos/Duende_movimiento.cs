using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duende_movimiento : MonoBehaviour
{
    private Animator Anim;
    private int Direccion;
    private Rigidbody2D rgbd2D;
    public GameObject Punto;
    public GameObject Vida_Jugador;
    public float velocidad;
    public bool Atacando = false;
    public int Vida_Enemigo;

    void Start()
    {
        Anim = GetComponent<Animator>();
        rgbd2D = GetComponent<Rigidbody2D>();
    }

   
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        Debug.DrawRay(transform.position, Vector3.left * 2f, Color.black);
        Debug.DrawRay(transform.position, Vector3.right * 2f, Color.black);
        RaycastHit2D Conflictoizquierdo = Physics2D.Raycast(transform.position, Vector3.left, 2f, 1 << 7);
        RaycastHit2D Conflictoderecho = Physics2D.Raycast(transform.position, Vector3.right, 2f, 1 << 7);

        if (Atacando == false)
        {
            if (Conflictoizquierdo.collider != null)
            {
                if (Conflictoizquierdo.transform.tag == "Player")
                {
                    Anim.SetBool("Corriendo", true);
                    Direccion = -1;
                    transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                    rgbd2D.velocity = new Vector3(velocidad * Direccion, 0, 0);
                }
            }
            else if (Conflictoderecho.collider != null)
            {
                if (Conflictoderecho.transform.tag == "Player")
                {
                    Anim.SetBool("Corriendo", true);
                    Direccion = 1;
                    transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    rgbd2D.velocity = new Vector3(velocidad * Direccion, 0, 0);
                }
            }
            else
            {
                Anim.SetBool("Corriendo", false);
                Direccion = 1;
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                rgbd2D.velocity = new Vector3(0, 0, 0);
            }
        }
        Morir();
}
    public void Morir()
    {
        if (Vida_Enemigo ==0)
        {
            Destroy(gameObject);
        }
    }
    public void Parar()
    {
        Atacando = true;
        rgbd2D.velocity = new Vector3(0, 0, 0);
        Anim.SetBool("Atacar", true);
    }
    public void Seguir()
    {
        Atacando = false;
        Anim.SetBool("Atacar",false);
    }
    public void BajarVida()
    {
        if (Atacando == true)
        {
            Vida_Jugador.SendMessage("dañobtenido", 10);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Espada")
        {
            Vida_Enemigo = Vida_Enemigo - 1;
        }
        
    }
}
