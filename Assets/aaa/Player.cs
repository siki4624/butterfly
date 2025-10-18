using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Player : MonoBehaviour
{
    [SerializeField] float spdX = 70;
    [SerializeField] float spdY = 60;
    [SerializeField] float spdZ = 100;
    [SerializeField] float jumpPower = 80;
    public bool canControl = true;
    public Mode mode = Mode.move;
    public enum Mode
    {
        move,
        jump,
        dead,
    }

    [SerializeField] int canResurrection = 0;//アクティブな復活アイテムの数
    [SerializeField] float chargeResurrection = 0;//復活アイテムチャージゲージ
    [SerializeField] int chargeResurrectionRate = 0;//復活アイテムの貯まるレート
    [SerializeField] float distace;

    [SerializeField] MeshRenderer butterFlyMesh;
    [SerializeField] Transform butterFly;

    Rigidbody rb;
    BoxCollider collider;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<BoxCollider>();

        rb.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canControl && mode == Mode.move) Move();
        JumpTrigger();
        distace = transform.position.z;
    }

    void Move()
    {
        float inputX = Input.GetAxis("Horizontal") * Time.deltaTime * spdX;
        float inputY = Input.GetAxis("Vertical") * Time.deltaTime * spdY;
        float inputZ = Time.deltaTime * spdZ;

        Vector3 targetPos = transform.position + new Vector3(inputX, inputY, inputZ);
        rb.position = Vector3.Lerp(rb.position, targetPos, 1); // ← 0.1fを上げると追従が速くなる
    }


    void JumpTrigger()
    {
        if (Input.GetKeyDown(KeyCode.Space) && mode == Mode.move)
        {
            StartCoroutine(Jumping());
        }
    }

    IEnumerator Jumping()
    {
        Mode beforeMode = mode;
        mode = Mode.jump;

        canControl = false;
        Vector3 dire = new Vector3(Input.GetAxis("Horizontal") / 2, 2, 0);

        float jp = jumpPower;
        while (jp > 0)
        {

            Vector3 newPos = transform.position;
            newPos += dire * jp * Time.deltaTime;
            newPos.z += spdZ * Time.deltaTime;

            rb.position = newPos;
            jp -= 0.35f;

            yield return null;
        }

        mode = beforeMode;
        canControl = true;

    }

    IEnumerator Dead()//しぬ
    {
        Debug.Log("HIT");

        if (canResurrection > 0)
        {
            StartCoroutine(Resurrection());
            yield break;
        }
        mode = Mode.dead;
    }

    IEnumerator Resurrection()//復活
    {
        Debug.Log("Resurrection");

        transform.position = new Vector3(0, 0, transform.position.z);
        StartCoroutine(MutekiTIme());
        yield break;
    }

    IEnumerator MutekiTIme()
    {
        var mat = butterFlyMesh.material;
        Color color = mat.color;

        color.a = 0.5f;
        mat.color = color;

        yield return new WaitForSeconds(1f);

        color.a = 1; ;
        mat.color = color;

    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(Dead());
        }
    }
}
