using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento_horizontal : MonoBehaviour
{
    public float velocidad;
    private Rigidbody2D Rigidbody2D;    //Variable de tipo Rigidbody2D
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();   //Le asignamos a la VARIABLE Rigidbody2D el componente Rigidbody2D
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(velocidad, Rigidbody2D.velocity.y); //Aqui es donde se hace posible el movimiento

    }
}
