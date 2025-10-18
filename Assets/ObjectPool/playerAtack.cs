using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class playerAtack : MonoBehaviour
{
    Transform bullets;

    [SerializeField] GameObject target;
    [SerializeField] private float _interval;

    [Header("弾のプレハブ"), SerializeField]
    GameObject[] _bulletPlefab;

    float interval;
    private float _timer = 0.0f;

    void Start()
    {
        bullets = new GameObject("PlayerBullets").transform;
    }

    void Update()
    {
        if (Mathf.Approximately(Time.timeScale, 0f)) { return; }
        GunAttack();
    }

    public void GunAttack()
    {

        if (Input.GetKey(KeyCode.X) && _timer <= 0.0f)
        {
            // 生成位置
            Vector3 pos = target.transform.position + target.transform.forward;

            //アクティブでないオブジェクト探索（bullets内から）
            foreach (Transform t in bullets)
            {
                if (!t.gameObject.activeSelf)
                {
                    //ある場合
                    t.transform.position = pos;
                    t.gameObject.SetActive(true);
                    _timer = _interval;
                    return;
                }
            }
            //ない場合
            Instantiate(_bulletPlefab[0], pos, Quaternion.identity, bullets);
            _timer = _interval;
        }

        // タイマーの値を減らす
        if (_timer > 0.0f)
        {
            _timer -= Time.deltaTime;
        }

    }
}
