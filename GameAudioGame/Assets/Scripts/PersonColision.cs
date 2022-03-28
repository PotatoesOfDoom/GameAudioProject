using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonColision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Person")
        {
            var person = collision.gameObject;
            Animator anim = person.GetComponent<Animator>();
            anim.SetInteger("deathTransition", Random.Range(0, 5));
            //person.GetComponent<Rigidbody>().AddExplosionForce(5, person.transform.position, 1.0f, 3.0F);
            person.GetComponent<Collider>().enabled = false;
        }

        if (collision.gameObject.tag == "Car")
        {
            Debug.Log("explode");
            var car = collision.gameObject;
            car.GetComponent<Rigidbody>().AddExplosionForce(500, car.transform.position, 10.0f, 10.0F);
            //person.GetComponent<Collider>().enabled = false;
        }
    }
}
