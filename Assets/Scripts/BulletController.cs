using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private GameObject mermiizi;
    [SerializeField] private bool flipped;
    [SerializeField] private float xVelocity;

    private void Update()
    {
        transform.Translate(Vector2.right * 10 * Time.deltaTime);
        Destroy(gameObject, 2.5f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Enemy>() != null)
        {
            other.GetComponent<Enemy>().stats.TakeDamage(5);
            Destroy(gameObject);
        }
        else if (other.GetComponent<Player>() != null)
        {
            other.GetComponent<Player>().stats.TakeDamage(5);
            Destroy(gameObject);
        }
        else if (other.CompareTag("InteractableBullet"))
        {
            GameObject mermi_izi = Instantiate(mermiizi, transform.position, Quaternion.identity);
            Destroy(mermi_izi, 1f);
            Destroy(gameObject);
        }
    }

    public void FlipBullet()
    {
        if (flipped)
            return;


        xVelocity = xVelocity * -1;
        flipped = true;
        transform.Rotate(0, 180, 0);
    }
}
