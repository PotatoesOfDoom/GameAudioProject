using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarLanding : MonoBehaviour
{
private FMOD.Studio.EventInstance carCollision;
public bool grounded = true;
    // Start is called before the first frame update
    void Start()
    {
        carCollision = FMODUnity.RuntimeManager.CreateInstance("event:/Interactable/Collisions/CarLand");
        carCollision.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
    }
    
    

    void OnCollisionEnter(Collision col){
    
        if (col.gameObject.tag != "car" && !grounded){
            carCollision.start();
            carCollision.release();
            grounded = true;
        }
    }
    
    void OnCollisionExit(){
        grounded = false;
    }
}
