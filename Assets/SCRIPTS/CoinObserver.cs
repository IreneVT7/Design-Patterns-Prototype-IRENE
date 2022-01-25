using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinObserver : MonoBehaviour
{
    [Tooltip("Lista del observer")]
    public List<CoinBehaviour> observers;

    //SINGLETON
    public static CoinObserver instance;
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


    public void NotifyObservers()
    {
        //notifica a las monedas que el jugador esta recogiendo una
        foreach (var item in observers)
        {
            item.PickUp();
        }
    }

    public void AddObserver(CoinBehaviour coin)
    {
        //mete la moneda en la lista observer
        observers.Add(coin);
    }


}
