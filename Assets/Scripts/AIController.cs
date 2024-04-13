using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _deathTexturePrefabs;
    [SerializeField]
    private float _explosionSize = 2f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Die();
        }
    }

    void Die()
    {
        GameObject[] textures = new GameObject[_deathTexturePrefabs.Length];
        for (int i = 0; i < _deathTexturePrefabs.Length; i++)
        {
            textures[i] = Instantiate(_deathTexturePrefabs[i], transform.position, Quaternion.identity);
        }

        foreach (var texture in textures)
        {
            texture.GetComponent<Rigidbody2D>().velocity = Random.insideUnitSphere * _explosionSize;
        }

        Destroy(gameObject);
    }
}
