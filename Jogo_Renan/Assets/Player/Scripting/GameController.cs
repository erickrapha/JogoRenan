using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    public Text heathText;
    public static GameController instance;

    //O Awake � chamado antes de todos os m�todos Start() do projeto
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateLives(int value)
    {
        heathText.text = "x " + value.ToString();
    }
}
