using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
   [SerializeField] private float _rotateSpeed = 20.0f;
   [SerializeField] private GameObject _explosionPrefab;
   private SpawnManager _spawnManager;

   private void Start()
   {
       _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
       if (_spawnManager == null)
       {
           Debug.LogError("Spawn Manager is null");
       }
   }

   // Update is called once per frame
    void Update()
    {
        // rotate the object
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag( "Laser"))
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            _spawnManager.StartSpawning();
            Destroy(this.gameObject,0.25f);   
        }
    }
}
