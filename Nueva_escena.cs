using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Nueva_escena : MonoBehaviour
{
    public float tiempoIni;
    public float tiempoFin;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tiempoIni += Time.deltaTime;
        if (tiempoIni >= tiempoFin)
        {
            SceneManager.LoadScene("Menú");
        }
    }
}
