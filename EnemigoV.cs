using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoV : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float agroRange;
    [SerializeField] float moveSpeed;

    Animator ani;
    public int Vida_Enemigo;

    Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Morir();
    }
    

    // Update is called once per frame
    void Update()
    {
        float disToPlayer = Vector2.Distance(transform.position, player.position);

        if (disToPlayer < agroRange)
        {
            ChasePlayer();
        }
        else
        {
            StopChasingPlayer();
        }
    }

    void ChasePlayer()
    {
        if (transform.position.x < player.position.x)
        {
            rb2d.velocity = new Vector2(moveSpeed, 0);
            transform.localScale = new Vector2(-1, 1);
        }
        else
        {
            rb2d.velocity = new Vector2(-moveSpeed, 0);
            transform.localScale = new Vector2(1, 1);
        }
        ani.Play("Ataque");
    }

    void StopChasingPlayer()
    {
        rb2d.velocity = new Vector2(0, 0);
        ani.Play("Volando");
    }
    public void Morir()
    {
        if (Vida_Enemigo == 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Arma")
        {
            Vida_Enemigo = Vida_Enemigo - 1;
        }

    }
}