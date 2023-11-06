using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{   
    public string proximaFase;
    public Text heathText;
    public static GameController instance;

    //O Awake � chamado antes de todos os m�todos Start() do projeto
    void Awake()
    {
        instance = this;
    }
    public void CarregarProximaFase()
    {
        SceneManager.LoadScene(proximaFase);
    }
    public void UpdateLives(int value)
    {
        heathText.text = "x " + value.ToString();
    }
}
