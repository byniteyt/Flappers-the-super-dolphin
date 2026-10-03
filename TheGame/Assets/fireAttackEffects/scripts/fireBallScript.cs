using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace fireAttackVFXNameSpace
{


    public class fireBallScript : MonoBehaviour
    {
        private bool GotHit = false;

        // Reference to the Visual Effect (VFX Graph)
        public VisualEffect vfxPrefab;

        // Reference to the object to disable
        public GameObject objectToDisable;

        // Called when the object this script is attached to collides with another object

        public Rigidbody rb;

        //movement 
        public float speed = 2f;
        public float maxSpeed = 3f;   // Maximum speed
        public float acceleration = 2f; // Speed increment per second

        // Speed of rotation in degrees per second
        public float rotationSpeed = 100f;

        // Start is called before the first frame update
        void Start()
        {
            rb = this.GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            // Gradually increase the speed until it reaches maxSpeed
            if (speed < maxSpeed)
            {
                speed += acceleration * Time.fixedDeltaTime;
                speed = Mathf.Min(speed, maxSpeed); // Clamp speed to maxSpeed
            }

            // Apply force to the Rigidbody
            rb.AddForce(Vector3.right * speed);

            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }

        //impact script
        private void OnCollisionEnter(Collision collision)
        {
            if (GotHit == false)
            {
                // Play the VFX if it's assigned
                if (vfxPrefab != null)
                {
                    // Instantiate the VFX at the collision point
                    VisualEffect vfxInstance = Instantiate(vfxPrefab, collision.contacts[0].point, Quaternion.identity);

                    // Optionally send a "play" event to the VFX
                    vfxInstance.SendEvent("OnPlay");

                    //destroying the impact effect
                    Destroy(vfxInstance.gameObject, 1f);

                }

                // Disable the object if it's assigned
                if (objectToDisable != null)
                {

                    // Destroy the fire ball after a short duration 
                    Destroy(this.gameObject);
                    GotHit = true;


                }

                // Optionally, destroy this game object (uncomment the line below if needed)
                // Destroy(gameObject);
            }
        }
    }
}
