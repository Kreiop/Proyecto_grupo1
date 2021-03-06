using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;    //Variable de tipo Rigidbody2D
    private float Horizontal;           //Creamos una variable global para el movimiento horizontal
    public float Velocidad;             //Una variable para la velocidad
    public float Fuerzadesalto;         //Una variable para controlar la fuerza de salto
    private bool Suelo;                 //Variable que capta si el personaje esta o no tocando el suelo

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();   //Le asignamos a la VARIABLE Rigidbody2D el componente Rigidbody2D
    }

    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal"); //Movimiento por medio de presionar las teclas A(-1) y D(1) que vienen definidas por defecto

        Debug.DrawRay(transform.position, Vector3.down*0.21f,Color.black); //Muestra el rayo desplegado por el raycast del condicional 

        if (Physics2D.Raycast(transform.position, Vector3.down, 0.21f))    //Este condicional determina si se esta colisionando o no con el suelo
        {                                                        
            Suelo = true;
        }
        else Suelo = false;

        
        if (Input.GetKeyUp(KeyCode.W) && Suelo)               
        {
            Salto();//En este condicional se captura la presion sobre la tecla w y aplica una  
        }           //fuerza hacia el eje positivo de la Y
    }
    private void Salto()
    {
        Rigidbody2D.AddForce(Vector2.up * Fuerzadesalto);
    }

    private void FixedUpdate()                       //Aqui las instrucciones se ejecutan mas veces por segundo
    {
        Rigidbody2D.velocity = new Vector2(Horizontal, Rigidbody2D.velocity.y); //Aqui es donde se hace posible el movimiento 
    }
}
