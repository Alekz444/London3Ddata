using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour
{

    // Update is called once per frame
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.name == "Plane")
        {
            Debug.Log("COLLISION DETECTED");
        }
    }
}
