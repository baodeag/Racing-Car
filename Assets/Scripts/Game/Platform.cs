using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private bool isFalling = false;
    private Rigidbody rb;
    private PlatformSpawner platformSpawner;

    public float vanishDelay = 3.0f;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; 
        platformSpawner = FindObjectOfType<PlatformSpawner>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && !isFalling)
        {
            isFalling = true;
            StartCoroutine(FallPlatform());
        }
    }

    private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Player"))
    {
        StartCoroutine(VanishAfterDelay());
    }
}


    private IEnumerator VanishAfterDelay()
    {
        yield return new WaitForSeconds(vanishDelay);
        DestroyPlatform();
    }

    private IEnumerator FallPlatform()
    {   
        yield return new WaitForSeconds(0.5f);
        rb.isKinematic = false; 
        rb.constraints = RigidbodyConstraints.FreezeRotation; 
        rb.velocity = new Vector3(0f, -5f, 0f); 
        
    }

    private void DestroyPlatform()
{
    if (platformSpawner != null)
    {
        platformSpawner.DecreaseGeneratedPlatforms(); 
    }

    Destroy(gameObject);
}

}
