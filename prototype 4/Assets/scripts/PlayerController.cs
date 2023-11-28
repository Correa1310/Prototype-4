using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
        private float powerupStreangth = 15.0f;
        private Rigidbody playerRb;
        public float Speed = 5f;
        private GameObject FocalPoint;
        public bool hasPowerup = true;
        public GameObject powerupIndicator;
        
        
        
        // Start is called before the first frame update
        void Start()
        {
            playerRb = GetComponent<Rigidbody>();
            FocalPoint = GameObject.Find("Focal Point");
        }

        // Update is called once per frame
        void Update()
        {
            float forwardInput = Input.GetAxis("Vertical");

            playerRb.AddForce(FocalPoint.transform.forward * Speed * forwardInput);

        powerupIndicator.transform.position = transform.position;
        }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            powerupIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;

            Debug.Log("Collider with: " + collision.gameObject.name + "with powerup set to" + hasPowerup);
            enemyRigidbody.AddForce(awayFromPlayer * powerupStreangth, ForceMode.Impulse);
        }
    }
}
