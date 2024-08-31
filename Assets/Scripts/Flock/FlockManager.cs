using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace flockSpace
{
    public class FlockManager : MonoBehaviour
    {
        public static FlockManager instance;


        [Header("Civilain Settings")]
        [Range(0f, 15.0f)]
        public float minSpeed;
        [Range(0f, 15.0f)]
        public float maxSpeed;

        [Range(1f, 10.0f)]
        public float neighbourDistance;

        [Range(1.0f, 5f)]
        public float rotationSpeed;

        public GameObject civilainPrefab;
        public int numCivil = 20;
        public GameObject[] allCivilain;
        public Vector3 walkLimits = new Vector3(25, 25, 25);

        public Vector3 goalPos = Vector3.zero;

        private void Start()
        {
            instance = this;

            allCivilain = new GameObject[numCivil];

            for (int i = 0; i < numCivil; i++)
            {
                Vector3 pos = this.transform.position + new Vector3(Random.Range(0, walkLimits.x),
                    0, Random.Range(0, walkLimits.z));

                allCivilain[i] = Instantiate(civilainPrefab, pos, Quaternion.identity);
            }
            //goalPos = this.transform.position;
        }

        //private void Update()
        //{
        //    if (Random.Range(0, 5000) < 10)
        //    {
        //        goalPos = this.transform.position + new Vector3(Random.Range(-walkLimits.x, walkLimits.x),
        //            0, Random.Range(-walkLimits.z, walkLimits.z));
        //    }
        //}
    }
}

