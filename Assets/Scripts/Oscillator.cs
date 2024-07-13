using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2.0f;

    float movementFactor;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        Debug.Log(startingPosition);

    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) return; // Hans linje, er det bedre?
        //if (period > 0)
        //{
            float cycles = Time.time / period; // Continueally growing over time

            const float tau = Mathf.PI * 2; // Constant value of 6.283
            float rawSineWave = Mathf.Sin(cycles * tau); // going from -1 to 1

            movementFactor = (rawSineWave + 1f) / 2f; // Recalculated to go from 0 to 1 so it's cleaner

            Vector3 offset = movementVector * movementFactor;
            transform.position = startingPosition + offset;
        //}
    }
}
