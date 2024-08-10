using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxEffect : MonoBehaviour
{
    public Camera cam; 
    public Transform followTarget; 

    //Starting position for the parallax game object 
    Vector2 startingPosition;

    float startingZ;

    //Distance that the camera has moved from the starting position of the parallax object
    Vector2 camMoveSinceStart => (Vector2)cam.transform.position - startingPosition;

    float zdistanceFromTarget => transform.position.z - followTarget.transform.position.z;

    float clippingPlane => (cam.transform.position.z + (zdistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));

    float parallaxFactor => Mathf.Abs(zdistanceFromTarget) / clippingPlane; 

    

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position; 
        startingZ = transform.position.z;
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPosition = startingPosition + camMoveSinceStart * parallaxFactor; 

        transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
        
    }
}
