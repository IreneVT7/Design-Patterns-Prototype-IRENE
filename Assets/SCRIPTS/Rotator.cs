using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [Tooltip("Velocidad a la que rotan el objeto")]
    public float rotationSpeed;

    private void Update()
    {
        //rota continuamente el objeto al que este atachado el script
        transform.Rotate(new Vector3(0, 45, 0) * rotationSpeed * Time.deltaTime);
    }


}
