﻿using System.Collections;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    // ----------------------------------------------- Data members ----------------------------------------------
    public Transform blueCar, whiteCar, blueEndPos, whiteEndPos;
    public AudioSource blueSound, whiteSound;
    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    public IEnumerator MoveCar(Transform car, Transform endPos, float time)
    {
        float elapsedTime = 0;
        Vector3 startingPos = car.position;
        while (elapsedTime < time)
        {
            car.position = Vector3.Lerp(startingPos, endPos.position, Mathf.SmoothStep(0.0f, 8.0f, elapsedTime));
            elapsedTime += Time.deltaTime / time;
            yield return null;
        }
    }
    // --------------------------------------------------------------------
    public IEnumerator MoveCars()
    {
        StartCoroutine(MoveCar(blueCar, blueEndPos, 10f));
        yield return new WaitForSeconds(1f);
        StartCoroutine(MoveCar(whiteCar, whiteEndPos, 10f));
        yield return null;
    }
    // --------------------------------------------------------------------
    public void MoveTheDamnCars()
    {
        StartCoroutine(MoveCars());
    }
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}