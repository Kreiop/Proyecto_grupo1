using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static int nivel;
    public static void Pause()
    {
        Time.timeScale = 0;
    }
    public static void Return()
    {
        Time.timeScale = 1;
    }

    public static void recargarEscena()
    {
        SceneManager.LoadScene("Segundo nivel");
    }
    public void SegundoNivel()
    {
        SceneManager.LoadScene("Segundo nivel");
        nivel = 2;
        PlayerPrefs.SetFloat("Nivel", nivel);
    }
    public void Reiniciar()
    {
        if (PlayerPrefs.GetFloat("posicionx") != 0)
        {
            PlayerPrefs.DeleteKey("posicionx");
            PlayerPrefs.DeleteKey("posiciony");
            PlayerPrefs.DeleteKey("Velocidadx");
            PlayerPrefs.DeleteKey("Velocidady");
        }
    }
}
