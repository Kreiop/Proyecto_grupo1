using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo_simple : MonoBehaviour
{
    [SerializeField] private Transform[] puntosMov;
    [SerializeField] private float velocidad;
    [SerializeField] private GameObject padre;

    private Rigidbody2D rgb2d;
    private SpriteRenderer sPlayer;
    private int i = 0;

    private Vector3 escalaIni, escalaTemp;
    private float miraDer = 1;
    // Start is called before the first frame update
    void Start()
    {
        escalaIni = transform.localScale;
        rgb2d = GetComponent<Rigidbody2D>();
        sPlayer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, puntosMov[i].transform.position, velocidad * Time.deltaTime);
        if (Vector2.Distance(transform.position, puntosMov[i].transform.position) < 0.1f)
        {
            if (puntosMov[i] != puntosMov[puntosMov.Length - 1]) i++;
            else i = 0;
            miraDer = Mathf.Sign(puntosMov[i].transform.position.x - transform.position.x);
            gira(miraDer);
        }
    }
    private void gira(float lado)
    {
        if (miraDer == -1)
        {
            escalaTemp = transform.localScale;
            escalaTemp.x = escalaTemp.x * -1;
        }
        else escalaTemp = escalaIni;
        transform.localScale = escalaTemp;
    }
    public void Muere()
    {
        Destroy(padre);
    }
    
}