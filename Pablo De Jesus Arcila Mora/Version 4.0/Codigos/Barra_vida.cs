using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Barra_vida : MonoBehaviour
{
    public Image Vida;
    public float hp, Vidamaxima = 100f;
    void Start()
    {
        hp = Vidamaxima;
    }
    public void dañobtenido(float valor)
    {
        hp = Mathf.Clamp(hp - valor, 0f, Vidamaxima);
        Vida.transform.localScale = new Vector2(hp / Vidamaxima, 1);
    }
}
