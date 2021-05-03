using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    public GameObject personaje;
    void Update()
    {
        Vector3 posicion = transform.position;
        posicion.x = personaje.transform.position.x;
        transform.position = posicion;
    }
}
