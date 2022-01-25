using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Tooltip("Array de spawners en los que las monedas pueden spawnear")]
    public Transform[] spawners;
    [HideInInspector]
    public int spawnerNumber;
    [HideInInspector]
    public bool needCoin;


    //SINGLETON
    public static Spawner instance;
    private void Awake()
    {
        //si no hay ninguna instancia de este script. este es el script correcto. si hay otro destruye este
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //si el juego acaba de comenzar: instancia 4 monedas en un spawner random y el juego ya ha comenzado (no acaba de comenzar)
        if (GameManager.instance.gameJustStarted == true)
        {

            Random.Range(0, CoinPooling.instance.coinList.Count);
            for (int i = 0; i < 4; i++)
            {
                spawnerNumber = Random.Range(0, spawners.Length);
                CoinPooling.instance.InstantiateCoin(spawners[spawnerNumber]);
            }

            GameManager.instance.gameJustStarted = false;
            GameManager.instance.gameStarted = true;
        }
        else if (GameManager.instance.gameStarted == true && needCoin == true)
        {
            //si el juego ya ha comenzado y se necesita una moneda llama a la funcion para reactivar todas y deja de necesitar una nueva

            // spawnerNumber = Random.Range(0, spawners.Length);
            // CoinPooling.instance.InstantiateCoin(spawners[spawnerNumber]);
            CoinPooling.instance.ReactivateCoins();
            needCoin = false;
        }
    }
}
