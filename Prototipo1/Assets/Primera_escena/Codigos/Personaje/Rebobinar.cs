using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rebobinar : MonoBehaviour
{
    public bool Rebobinando;   //Esta variable define si se esta o no rebobinando
    List<Vector3> Posicicones; //Vector que almacenara las posiciones 

    void Start()
    {
        Posicicones = new List<Vector3>();   
    }


    private void FixedUpdate()
    {
        if (Rebobinando)        
            Rebobinar();
        else
            Almacenar();

        void Rebobinar()
        {
            if (Posicicones.Count > 0)
            {
                transform.position = Posicicones[0];   //Cuando se rebobine queremos que nuestro objeto vaya desde la ultima posicion hasta la
                Posicicones.RemoveAt(0);               //primera, asi que lo que hacemos es mover el objeto a la ultima posicion y borrar esta 
            }                                          //posicion del vector


        }

        void Almacenar()                                
        {
            Posicicones.Insert(0,transform.position);  //Si no se esta rebobinando queremos que se guarden las posiciones en el vector Posiciones
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))                //La tecla clave para empezar a rebobinar es la z
            EmpezarRebobinar();
        if (Input.GetKeyUp(KeyCode.Z))
            TerminarRebobinar();

        void EmpezarRebobinar()
        {
            Rebobinando = true;
        }
        void TerminarRebobinar()
        {
            Rebobinando = false;
        }
    }
}
