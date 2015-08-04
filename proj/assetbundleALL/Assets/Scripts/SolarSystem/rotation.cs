using UnityEngine;
using System.Collections;

namespace solarsystem
{
    public class rotation : MonoBehaviour
    {
        public float speed = 10f;

        void Start()
        {

        }


        void Update()
        {
            transform.Rotate(transform.up,speed*Time.deltaTime,Space.World);
        }
    }
}
