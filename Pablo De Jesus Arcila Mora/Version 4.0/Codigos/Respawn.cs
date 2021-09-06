using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    private float posicionx, posiciony;
    private float Velocidadx, Velocidady;
    private int Vidanumero;
    public GameObject Vida;


    void Start()
    {
        
        if (PlayerPrefs.GetFloat("posicionx")!=0)
        {
            transform.position = (new Vector3(PlayerPrefs.GetFloat("posicionx"), PlayerPrefs.GetFloat("posiciony"),-1f));
            Vida.GetComponent<Barra_vida>().hp = PlayerPrefs.GetFloat("Vidanumero");
            Vida.GetComponent<Barra_vida>().dañobtenido(0);
        } 
    }
    public void FixedUpdate()
    {
        if (Vida.GetComponent<Barra_vida>().hp == 0 )//corregir
        {
            if (PlayerPrefs.GetFloat("posicionx") != 0)
            {
                transform.position = (new Vector3(PlayerPrefs.GetFloat("posicionx"), PlayerPrefs.GetFloat("posiciony"), -1f));
                Vida.GetComponent<Barra_vida>().hp = PlayerPrefs.GetFloat("Vidanumero");
                Vida.GetComponent<Barra_vida>().dañobtenido(0);
            }
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Daño")
        {
            if (PlayerPrefs.GetFloat("posicionx") != 0)
            {
                transform.position = (new Vector3(PlayerPrefs.GetFloat("posicionx"), PlayerPrefs.GetFloat("posiciony"), -1f));
                Vida.GetComponent<Barra_vida>().hp = PlayerPrefs.GetFloat("Vidanumero");
                Vida.GetComponent<Barra_vida>().dañobtenido(0);
            }
        }
    }
    public void PuntoAparacion(float x,float y)
    {
        PlayerPrefs.SetFloat("posicionx",x);
        PlayerPrefs.SetFloat("posiciony", y);
    }
    public void VelocidadDeAparicion(float vx , float vy )
    {
        PlayerPrefs.SetFloat("Velocidadx",vx);
        PlayerPrefs.SetFloat("Velocidady",vy);
    }
    public void VidaDeAparicion(float vidaparicion)
    {
        PlayerPrefs.SetFloat("Vidanumero",vidaparicion);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CheckPoint"))
        {
            VidaDeAparicion(Vida.GetComponent<Barra_vida>().hp);
        }
    }

}
