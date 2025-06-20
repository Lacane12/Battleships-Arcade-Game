using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public ShipController[] controllers;

    private void Update()
    {
        int counter = 0;
        for (int i = 0; i < controllers.Length; i++)
        {
            if (controllers[i].isDead)
                counter++;
        }

        if (counter >= controllers.Length)
            StartCoroutine(Restart());
    }

    IEnumerator Restart() 
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }
}
