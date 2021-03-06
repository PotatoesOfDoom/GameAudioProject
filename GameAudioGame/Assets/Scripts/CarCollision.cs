using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarCollision : MonoBehaviour
{
    private FMOD.Studio.EventInstance carCollision;
        private FMOD.Studio.EventInstance instance;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Person")
        {
            var person = collision.gameObject;
            Animator anim = person.GetComponent<Animator>();
            anim.SetInteger("deathTransition", Random.Range(0, 5));
            FMODUnity.RuntimeManager.PlayOneShot("event:/Interactable/Collisions/HitPerson");            
            person.GetComponent<Collider>().enabled = false;
        }

        if (collision.gameObject.tag == "Car")
        {
            carCollision = FMODUnity.RuntimeManager.CreateInstance("event:/Car Sounds/Crash Sounds");
            carCollision.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
            carCollision.start();
            carCollision.release();
            var car = collision.gameObject;
            car.GetComponent<Rigidbody>().AddExplosionForce(500, car.transform.position, 10.0f, 10.0F);
            
        }
        
        if (collision.gameObject.tag == "Speaker")
        {
            instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            instance = FMODUnity.RuntimeManager.CreateInstance("event:/Music/SpeakerMusic");
            instance.start();
            instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
            instance.setParameterByName("SpeakerMusicType", Random.Range(0, 5));
        }
        
        
        if (collision.gameObject.tag == "Building")
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Car Sounds/Building Crash Sounds");
            FMODUnity.RuntimeManager.PlayOneShot("event:/Car Sounds/Crash Sounds");
        }

        if (collision.gameObject.tag == "Tree")
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Car Sounds/Tree Collision");
        }
        
        if (collision.gameObject.tag == "Target")
        {
            SceneManager.LoadScene("PizzaDelivered");
        }

    }
}
