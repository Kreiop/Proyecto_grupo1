using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento_jugador : MonoBehaviour
{
    private Rigidbody2D rgb2d;
    public Animator animator;
    public float Velocidad;
    public float Fuerzadesalto;
    public bool Suelo;
    private float Horizontal;
    public float Empujex;
    public float direccionx;
    private float tiempoenemigo;

    public GameObject Barra_de_vida;

    RaycastHit2D Cañonizquierdo;
    RaycastHit2D Cañonderecho;
    RaycastHit2D Piso;
    void Start()
    {
        rgb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        Atacar();
        Debug.DrawRay(transform.position, Vector3.down * 0.21f, Color.black);

        Debug.DrawRay(transform.position, Vector3.right * 10f, Color.blue);
        Debug.DrawRay(transform.position, Vector3.left * 10f, Color.blue);

        Cañonizquierdo = Physics2D.Raycast(transform.position, Vector3.left, 4f, 1 << 9);
        Cañonderecho = Physics2D.Raycast(transform.position, Vector3.right, 4f, 1 << 9);
        Piso = Physics2D.Raycast(transform.position, Vector3.down, 0.21f, 1<<6 | 1<<11);
        if (Piso)   
        {
            Suelo = true;
           
        }
        else Suelo = false;

        if (Input.GetKeyDown(KeyCode.Space) && Suelo && Time.time>tiempoenemigo+0.5f)
        {
            Salto();
        }
        animator.SetBool("Saltando", Suelo == false);
    }
    private void FixedUpdate()
    {
        if (Horizontal == 0 && Suelo == true)
        {
            animator.SetBool("Quieto", true);
        }
        else animator.SetBool("Quieto", false);


        
        Horizontal = Input.GetAxisRaw("Horizontal");
        animator.SetBool("Corriendo",Horizontal!=0);

        Vector3 velocidadfriccion = rgb2d.velocity;
        velocidadfriccion *= 0.75f;

        if (Suelo)
        {
            rgb2d.velocity = velocidadfriccion;
        }
        if (Horizontal !=0 && Time.time > tiempoenemigo + 0.5f)
        {
            Corriendo(Horizontal);  
        }
        
    }
    private void Corriendo(float Direccion)
    {
        rgb2d.velocity = new Vector2(Direccion* Velocidad, rgb2d.velocity.y);

        
        if (Direccion == -1)
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
        else if (Direccion == 1)
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
    }
    private void Salto()
    {
        rgb2d.AddForce(Vector2.up*Fuerzadesalto, ForceMode2D.Impulse);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Plataforma")
        {
            transform.parent = collision.transform;
        }
    }
    private void Atacar()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetBool("Atacando", true);
        }
        else animator.SetBool("Atacando", false);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Plataforma" )
        {
            transform.parent = null;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Daño" || collision.gameObject.tag == "BolaFuego")
        {
            if (Cañonderecho.collider != null)
            {
                if (Cañonderecho.transform.tag == "Encapuchado")
                {
                    Empujex = -1;
                }
            }
            else if (Cañonizquierdo.collider != null)
            {
                if (Cañonizquierdo.transform.tag == "Encapuchado")
                {
                    Empujex = 1;
                }
            }
            Horizontal = 0;
            rgb2d.velocity = new Vector2(0, rgb2d.velocity.y);
            tiempoenemigo = Time.time;
        }
        if (collision.gameObject.tag == "BolaFuego" || collision.gameObject.tag == "Lanza")
        {
            Barra_de_vida.SendMessage("dañobtenido", 10);
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        
            if (collision.gameObject.tag == "BolaFuego")
            {
                Horizontal = 0;
                Suelo = false;
                rgb2d.AddForce(new Vector2(Empujex * 20f, 30f), ForceMode2D.Force);
            }
            
            
        
    }

}
