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

    //O Awake é chamado antes de todos os métodos Start() do projeto
    void Awake()
    {
        instance = this;
    }
    public void CarregarProximaFase()
    {
        Invoke(nameof(Load), 1);
    }
    public void UpdateLives(int value)
    {
        heathText.text = "x " + value.ToString();
    }
    private void Load()
    {
        SceneManager.LoadScene(proximaFase);
    }
}
