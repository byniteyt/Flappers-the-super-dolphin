using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace fireAttackVFXNameSpace
{


    public class InstantiateScript : MonoBehaviour
    {
        //fireBall
        public GameObject fireBallPrefab;// Prefab containing the fire ball effect
        public Transform fireBallspawnPoint;// Where the effect should appear


        //tornado
        public GameObject tornadoPrefab;// Prefab containing the tornado effect
        public Transform tornadospawnPoint;// Where the effect should appear
        public float tornadoDuration = 8f; // Lifetime of the effects in seconds


        //fire zone
        public GameObject fireZonePrefab;// Prefab containing the fire zone effect
        public Transform fireZoneSpawnPoint;// Where the effect should appear
        public float fireZoneDuration = 8f; // Lifetime of the effects in seconds

        //fire meteors
        public GameObject fireMeteorsPrefab;// Prefab containing the fire meteors effect
        public Transform fireMeteorsSpawnPoint;// Where the effect should appear
        public float fireMeteorsDuration = 8f; // Lifetime of the effects in seconds




        // Update is called once per frame
        void Update()
        {
            //fireBall
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                spawnFireBall();
            }

            //tornado
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                spawnTornadoFunction();
            }

            //fire zone
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                spawnFireZoneFunction();
            }

            //fire meteors
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                spawnFireMeteorsFunction();
            }

        }

        //spawning the fire ball effect function
        public void spawnFireBall()
        {
            // Instantiate the fire ball
            GameObject fireBallInstance = Instantiate(fireBallPrefab, fireBallspawnPoint.position, fireBallspawnPoint.rotation);
        }

        //spawning the tornado effect function
        public void spawnTornadoFunction()
        {
            // Instantiate tornado
            GameObject tornadoInstance = Instantiate(tornadoPrefab, tornadospawnPoint.position, tornadospawnPoint.rotation);
            VisualEffect vfxGraph = tornadoInstance.GetComponent<VisualEffect>();
            ParticleSystem sparks = tornadoInstance.GetComponentInChildren<ParticleSystem>();

            // changes the vfx life time
            if (vfxGraph != null)
            {
                vfxGraph.SetFloat("life time", tornadoDuration * 0.9f); 
                vfxGraph.Play();
            }

            // changes the sparks life time
            if (sparks != null)
            {
                var mainModule = sparks.main; // Access the main module of the Particle System
                mainModule.duration = tornadoDuration * 0.7f; // Set the duration of the Particle System
                sparks.Play(); // Play the Particle System
            }

            // Destroy the tornado after the duration
            Destroy(tornadoInstance, tornadoDuration);
        }

        //spawning the fire zone effect function
        public void spawnFireZoneFunction()
        {

            // Instantiate fire zone
            GameObject fireZoneInstance = Instantiate(fireZonePrefab, fireZoneSpawnPoint.position, fireZoneSpawnPoint.rotation);
            VisualEffect vfxGraph = fireZoneInstance.GetComponent<VisualEffect>();
            ParticleSystem sparks = fireZoneInstance.GetComponentInChildren<ParticleSystem>();

            // changes the vfx life time
            if (vfxGraph != null)
            {
                vfxGraph.SetFloat("lifeTime", fireZoneDuration * 0.9f); 
                vfxGraph.Play();
            }

            // changes the sparks life time and delay
            if (sparks != null)
            {
                var mainModule = sparks.main; // Access the main module of the Particle System
                mainModule.duration = fireZoneDuration * 0.7f; // Set the life time of the Particle System
                mainModule.startDelay = fireZoneDuration * 0.2f; // Set the delay of the Particle System
                sparks.Play(); // Play the Particle System
            }

            // Destroy the fire zone after the duration
            Destroy(fireZoneInstance, fireZoneDuration);
        }



        //spawning the fire meteors effect function
        public void spawnFireMeteorsFunction()
        {

            // Instantiate fire meteors
            GameObject fireMeteorsInstance = Instantiate(fireMeteorsPrefab, fireMeteorsSpawnPoint.position, fireMeteorsSpawnPoint.rotation);
            VisualEffect vfxGraph = fireMeteorsInstance.GetComponent<VisualEffect>();

            // changes the vfx life time
            if (vfxGraph != null)
            {
                vfxGraph.SetFloat("effectLifeTime", fireMeteorsDuration);
                vfxGraph.Play();
            }

            // Destroy the fire meteors effect after the duration
            Destroy(fireMeteorsInstance, fireMeteorsDuration * 1.4f);
        }

    }

}
