using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace flockSpace
{
    public class Flock : MonoBehaviour
    {

        float speed;
        bool turning = false;

        // Start is called before the first frame update
        void Start()
        {
            speed = Random.Range(FlockManager.instance.minSpeed, FlockManager.instance.maxSpeed);
        }

        // Update is called once per frame
        void Update()
        {
            Bounds b = new Bounds(FlockManager.instance.transform.position, FlockManager.instance.walkLimits*2);

            if (!b.Contains(transform.position))
            {
                turning = true;
            }
            else
            {
                turning = false;
            }

            if (turning)
            {
                Vector3 direction = FlockManager.instance.goalPos-transform.position - transform.position;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction),
                    FlockManager.instance.rotationSpeed*Time.deltaTime);
            }
            else
            {
                if (Random.Range(0, 100) < 10)
                {
                    speed = Random.Range(FlockManager.instance.minSpeed, FlockManager.instance.maxSpeed);
                }

                if (Random.Range(0, 100) < 10)
                {
                    ApplyRules();
                }
            }
            this.transform.Translate(0, 0, speed * Time.deltaTime);
        }

        void ApplyRules()
        {
            GameObject[] gos;
            gos = FlockManager.instance.allCivilain;

            Vector3 vcenter = Vector3.zero;
            Vector3 vavoid = Vector3.zero;
            float groupSpeed = 0.01f;
            float neighbourDistance;
            int groupSize = 0;

            foreach(GameObject go in gos)
            {
                if(go != this.gameObject)
                {
                    //We take the distance between this gameobject and the "neighbor"
                    neighbourDistance = Vector3.Distance(go.transform.position, this.transform.position);
                    if(neighbourDistance <= FlockManager.instance.neighbourDistance)
                    {
                        vcenter += go.transform.position;
                        groupSize++;
                    
                        if(neighbourDistance < 3.0f)
                        {
                            //We tell our gameobject to avoid the neighbor if its too close;
                            vavoid = vavoid + (this.transform.position - go.transform.position);
                        }

                        Flock anotherFlock = go.GetComponent<Flock>();
                        groupSpeed = groupSpeed + anotherFlock.speed;
                    }
                }
            }

            if(groupSize > 0)
            {
                vcenter = vcenter/groupSize + (FlockManager.instance.goalPos - this.transform.position);
                
                speed = groupSpeed / groupSize;
                if(speed > FlockManager.instance.maxSpeed)
                {
                    speed = FlockManager.instance.maxSpeed;
                }

                //Our new direction we want our civilain to head in;
                Vector3 direction = (vcenter + vavoid) - transform.position;
                if(direction != Vector3.zero)
                {
                    //Turn the civilain slowly towards the direction it should head to;
                    transform.rotation = Quaternion.Slerp(transform.rotation,
                        Quaternion.LookRotation(direction),
                        FlockManager.instance.rotationSpeed*Time.deltaTime);
                }
            }
        }
    }

}