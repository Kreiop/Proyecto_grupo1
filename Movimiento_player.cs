using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento_player : MonoBehaviour
{
    [SerializeField] private LayerMask capaSuelo;
    [SerializeField] private int vida = 100;
    [SerializeField] private BoxCollider2D colisionador;

    Transform tramoAgarrado;
    public GameObject Barra_de_vida;
    private Rigidbody2D rgb2d;
    public Animator animator;
    private CapsuleCollider2D ccPlayer;
    private BoxCollider2D bcPlayer;
    private Vector2 ccSize;
    private Vector3 posIni;
    private SpriteRenderer sPlayer;
    public float Velocidad;
    public float Fuerzadesalto;
    private float posPlayer, altoCam, altoPlayer;
    private bool Suelo;
    bool agarrado = false;
    private bool tocando = false;
    private bool muerto = false;
    private Color colorOriginal;
    private Camera cámara; 

    void Start()
    {
        rgb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sPlayer = GetComponent<SpriteRenderer>();
        colorOriginal = sPlayer.color;
        ccPlayer = GetComponent<CapsuleCollider2D>();
        ccSize = ccPlayer.size;
        posIni = transform.position;
        cámara = Camera.main;
        altoCam = cámara.orthographicSize * 2;
        altoPlayer = GetComponent<Renderer>().bounds.size.y;
        bcPlayer = GetComponent<BoxCollider2D>();
    }

    
    void Update()
    {
        Debug.DrawRay(transform.position, Vector3.down * 0.21f, Color.black);
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.21f))   
        {
            Suelo = true;
            Debug.Log(Suelo);
        }
        else Suelo = false;
       
        if (Input.GetKeyDown(KeyCode.Space) && Suelo)
        {
            Salto();
        }
        animator.SetBool("Saltando", Suelo == false);
        if (muerto)
        {
            posPlayer = cámara.transform.InverseTransformDirection(transform.position - cámara.transform.position).y;
            if (posPlayer < ((altoCam/2)* -1) - (altoPlayer / 2))
            {
                GameController.recargarEscena();
            }
        }
        if (agarrado)
        {
            transform.position = tramoAgarrado.transform.position;
        }
        Ataque();
    }
    private void FixedUpdate()
    {
        float Horizontal = Input.GetAxisRaw("Horizontal");
        animator.SetBool("Corriendo",Horizontal!=0);

        Vector3 velocidadfriccion = rgb2d.velocity;
        velocidadfriccion *= 0.75f;

        if (Suelo)
        {
            rgb2d.velocity = velocidadfriccion;
        }
        if (Horizontal !=0)
        {
            Corriendo(Horizontal);  
        }
        if (tocando)
        {
            tocando = false;
            sPlayer.color = colorOriginal;
        }
        
    }

    public void Ataque()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetBool("Ataque", true);
        }
        else animator.SetBool("Ataque", false);

        if (Input.GetButtonDown("Fire1") && Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("Ataque", true);
            rgb2d.AddForce(Vector2.up, ForceMode2D.Impulse);
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
        if (collision.gameObject.tag == "Enemigo")
        {
            tocado(collision.transform.position.x);
            Barra_de_vida.SendMessage("dañobtenido", 10);
        }
        if(collision.gameObject.tag == "Encima")
        {
            collision.gameObject.SendMessage("Muere");
        }
        if (collision.gameObject.tag == "Estacas")
        {
            Debug.Log("Quita Salud");
            //pierdeVida();
        }
        if (collision.gameObject.tag == "CaídaAlVacío")
        {
            Debug.Log("Quita Salud");
            GameController.recargarEscena();
        }
        if (collision.gameObject.tag == "Cueva1")
        {
            Ganaste.cargarEscena();
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            agarrado = true;
            tramoAgarrado = other.transform;
            rgb2d.isKinematic= true;
        }
    }
    private void tocado (float posX)
    {
        if (!tocando)
        {
            if (vida > 1)
            {
                Color nuevoColor = new Color(209f / 255f, 86f / 255f, 86f / 255f);
                sPlayer.color = nuevoColor;
                tocando = true;
                float lado = Mathf.Sign(posX - transform.position.x);
                rgb2d.velocity = Vector2.zero;
                rgb2d.AddForce(new Vector2(3f * -lado, 3f), ForceMode2D.Impulse);
                vida = vida - 10;
            }
            
        }
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Plataforma")
        {
            transform.parent = null;
        }
    }

        
}
