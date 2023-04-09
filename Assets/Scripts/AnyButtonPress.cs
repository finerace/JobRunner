using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnyButtonPress : MonoBehaviour
{
    [SerializeField] private bool cooldownIsOut = false;
    [SerializeField] private bool isEndGame;

    private void Start()
    {
        StartCoroutine(Cooldown());
    }

    private void Update()
    {
        if(!cooldownIsOut)
            return;

        if (isEndGame)
        {
            if (Input.anyKey)
                SceneManager.LoadScene(0);
        }
        else if (Input.anyKey)
            SceneManager.LoadScene(1);
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(2);

        cooldownIsOut = true;
    }
    
}
