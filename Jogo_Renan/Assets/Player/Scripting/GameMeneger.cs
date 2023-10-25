using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMeneger : MonoBehaviour
{
    public string proximaFase;

   public void CarregarProximaFase()
    {
        SceneManager.LoadScene(proximaFase);
    }
}
