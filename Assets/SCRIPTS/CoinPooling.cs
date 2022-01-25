using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoinPooling : MonoBehaviour
{
    [Tooltip("Lista para administrar las monedas")]
    public List<CoinBehaviour> coinList;
    [Tooltip("Prefab de la moneda que debe instanciarse")]
    public GameObject prefabCoin;

    //SINGLETON
    public static CoinPooling instance;
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

    private void Start()
    {
        //inicializa la lista
        coinList = new List<CoinBehaviour>();

    }


    public GameObject InstantiateCoin(Transform trans)
    {

        if (coinList.Count > 0)
        {
            //Si hay monedas en la lista: coge la primera, la activa, le cambia la posicion y la quita de la lista
            GameObject coin = coinList[0].gameObject;
            coin.SetActive(true);
            coin.transform.position = trans.position;
            coinList.RemoveAt(0);

            return coin;
        }
        else
        {
            //Si no hay monedas en la lista: instancia una nueva
            GameObject coin = Instantiate(prefabCoin, trans.position, Quaternion.identity, transform);
            return coin;
        }
    }

    public void ReactivateCoins()
    {
        //una por una reactiva las 4 monedas instanciadas al principio de la partida y cambia su punto de spawn. despues de eso se quita de la lista
        for (int i = 0; i < 4; i++)
        {
            GameObject coin = coinList[i].gameObject;
            coin.SetActive(true);
            Spawner.instance.spawnerNumber = Random.Range(0, Spawner.instance.spawners.Length);
            coin.transform.position = Spawner.instance.spawners[Spawner.instance.spawnerNumber].transform.position;
            coinList.RemoveAt(i);
        }


    }

    public void MeterEnLista(CoinBehaviour coin)
    {
        //mete una moneda desactivada en la lista (el resto esta en el coinbehaviour)
        coinList.Add(coin);
    }


}
