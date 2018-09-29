using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeExplosion : MonoBehaviour {
    //public CircleCollider2D range;
    public float range = 5f;
    public float force = 1f;
    public float delay = 3f;
    public GameObject visualEffect;
    public float visualEffectLifetime = 2f;

    private void Awake()
    {
        StartCoroutine(Explode());
    }

    IEnumerator Explode() {
        yield return new WaitForSeconds(delay);
        List<Rigidbody2D> rbs = new List<Rigidbody2D>();
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, range);
        foreach (Collider2D c in cols)
        {
            Rigidbody2D rb = c.GetComponent<Rigidbody2D>();
            if (rb != null && rbs.Contains(rb) == false)
            {
                rbs.Add(rb);
                Vector2 explosionForce = c.transform.position - transform.position;
                float rangeModifier = 1 - (explosionForce.magnitude / range);//fade force to edge of range
                explosionForce = explosionForce.normalized * force * rangeModifier;
                rb.AddForce(explosionForce);
            }
        }
        GameObject spawnedEffect = Instantiate(visualEffect, transform.position,Quaternion.identity);
        Destroy(spawnedEffect,visualEffectLifetime);
        Destroy(gameObject);
    }
}
