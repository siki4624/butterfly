using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBullet : MonoBehaviour
{
    [SerializeField, Header("‰Á‘¬“x‚Ì‘å‚«‚³")]
    private float _accelerationMagnitude = default;

    private float startTime;
    [SerializeField, Header("Å‘å‘¶‘±ŽžŠÔ")]
    private float maxLifetime = default;

    void OnEnable()
    {
        startTime = Time.time; // ‰ŠúŽžŠÔ‚ð•Û‘¶
    }

    void Start()
    {
        startTime = Time.time; // ‰ŠúŽžŠÔ‚ð•Û‘¶
    }

    void Update()
    {
        transform.position += transform.forward * _accelerationMagnitude * Time.deltaTime;

        // ˆê’èŽžŠÔŒo‰ßŒã‚Éíœ
         if (Time.time - startTime >= maxLifetime)
        {
            startTime = Time.time;
            this.gameObject.SetActive(false);
        }
    }
}
