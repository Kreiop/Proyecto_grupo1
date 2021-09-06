using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolaDeFuego : MonoBehaviour
{
    private Rigidbody2D rb;
    public float velocidad;
    private Vector2 Direccion;
    public GameObject portador;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        rb.velocity = Direccion * velocidad;
    }
    public void SetDireccion(Vector2 direccion)
    {
        Direccion = direccion;
    }
    public void DestruirBala()
    {
        Destroy(gameObject);
    }
}
