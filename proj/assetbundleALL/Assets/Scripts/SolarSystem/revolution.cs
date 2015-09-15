using UnityEngine;
using System.Collections;

namespace solarsystem
{
    public class revolution : MonoBehaviour
    {
        public float speed = 10f;
        public Transform target;

        void Start()
        {
            //Debug.Log(ClassLibrary1.Class1.dosome()+"...");
        }


        void Update()
        {
            transform.RotateAround(target.position, Vector3.up, speed * Time.deltaTime);
        }
    }

}