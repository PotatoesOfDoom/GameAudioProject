// Patrol.cs
using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class Patrol : MonoBehaviour
{

    public GameObject pointHolder;
    public Transform[] points;
    public int destinationReference = 0;

    private Vector3 curVelocity;
    private Vector3 wantedVelocity;
    private Vector3 steerVelocity;
    [SerializeField]
    private float maxSteerForce = 0.1f;


    void Start()
    {
        points = pointHolder.GetComponentsInChildren<Transform>();

        SetNextPoint();
        GoToPoint();
    }


        void SetNextPoint()
        {
            if (points.Length == 0)
                return;
            destinationReference = (destinationReference + 1) % points.Length;
            //transform.LookAt(points[destinationReference].position);

        }

        void GoToPoint()
        {
        Seek(this.gameObject, points[destinationReference], 5.0f);
        //this.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, points[destinationReference].position, 5.0f*Time.deltaTime);
        }


        public void Update()
        {
            //If we are close to the target point, we will switch to the next point
            if (CalculateDistance() < 0.5f)
            {
                SetNextPoint();
            }
            //Head to the target point
            GoToPoint();
        }

        float CalculateDistance()
        {
            //See how far away point is
            Vector3 current = this.transform.position;
            Vector3 target = points[destinationReference].position;
            return Vector3.Distance(current, target);
        }

        public void Seek(GameObject seeker, Vector3 target, float maxVelocity)
        {
            //Finds the desired velocity (velocity of obj we are seeking)
            wantedVelocity = (target - seeker.transform.position).normalized; //Find desired velocity
            wantedVelocity = wantedVelocity * maxVelocity;

            //Finds the steering velocity required to steer to the target
            steerVelocity = wantedVelocity - curVelocity;
            steerVelocity = Vector3.ClampMagnitude(steerVelocity, maxSteerForce);

            //Adds current velocity and steering velocity
            curVelocity += steerVelocity;
            curVelocity = Vector3.ClampMagnitude(curVelocity, maxVelocity);

            //Moves the position towards the object being seeked
            seeker.transform.position += curVelocity * Time.deltaTime;
            seeker.transform.forward += curVelocity * Time.deltaTime;
        }

    public void Seek(GameObject seeker, Transform target, float maxVelocity)
    {
        Seek(seeker, target.transform.position, maxVelocity);
    }
}
    
