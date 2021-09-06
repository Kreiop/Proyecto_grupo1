using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DibujarLineaPlataformas : MonoBehaviour
{
    public Transform Desde;
    public Transform Hasta;

    private void OnDrawGizmosSelected()
    {
        //Se dibuja una linea que nos sirve para ver mejor lo que hacemos
        if (Desde != null && Hasta != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(Desde.position, Hasta.position) ;
            Gizmos.DrawSphere(Desde.position,0.15f);
            Gizmos.DrawSphere(Hasta.position, 0.15f);
        }
    }
}
