using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class search : MonoBehaviour
{
    [SerializeField] float time;
    [SerializeField] float jump;
    [SerializeField] float speed;

    [SerializeField] bool kind;

    Vector3 pos;
    Vector3 nowPos;

    public bool flag = true;

    void OnTriggerEnter(Collider c)
    {
        if (flag == true)
        {
            if (c.CompareTag("Player"))
            {

                if (kind == true)
                {
                    flag = false;

                    StartCoroutine(deray(c.gameObject.transform.position));
                }

            }
        }

        if (c.CompareTag("MainCamera"))
        {
            flag = true;
            this.gameObject.SetActive(false);
        }
    }

    IEnumerator deray(Vector3 playerPos)
    {
        Transform child = this.transform.GetChild(0);
        pos = child.position;
        pos.y = playerPos.y;

        while (pos.y > child.position.y)
        {
            child.position = Vector3.MoveTowards(child.position, pos, speed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(time);

        flag = true;
    }
}