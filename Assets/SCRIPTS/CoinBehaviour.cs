using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    private bool playerInside;
    private void Awake()
    {
        // cuando este script que esta dentro de cada moneda comienza a funcionar mete la moneda en la lista del observer
        CoinObserver.instance.AddObserver(this);
    }

    public void PickUp()
    {
        //si el jugador esta dentro del trigger suma una moneda, desactiva la moneda, 
        //indica al script del jugador que esta recogiendo algo y deja de estar dentro del trigger para evitar errores
        if (playerInside)
        {
            GameManager.instance.coinCount++;
            this.gameObject.SetActive(false);
            BasicCharacterStateMachine.instance.pickingUp = true;
            playerInside = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //cuando el jugador entra en el trigger la variable indica que esta dentro (true)
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("entra en collider");
            playerInside = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        //cuando el jugador se sale del trigger la variable indica que esta fuera (false)
        if (other.gameObject.CompareTag("Player"))
        {
            playerInside = false;

        }
    }

    private void OnDisable()
    {
        //cuando la moneda se desactiva, pide que se instancie una nueva y se mete en la lista de coinpooling
        Spawner.instance.needCoin = true;
        CoinPooling.instance.MeterEnLista(this);
    }

}
