using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMaker : MonoBehaviour
{
    [SerializeField] GameObject stageBase;
    [SerializeField] mapObjct[] walls;

    [SerializeField] mapObjct[] enemys;

    [SerializeField]  float posZ = 10;

    [System.Serializable]
    public class mapObjct
    {
        public GameObject obj;
        public float spawaRate = 0.3f;
        public Vector3 spawnConditionMin = new Vector3(-3.5f, -6f);
        public Vector3 spawnConditionMax = new Vector3(3.5f, 3.8f);

        public int limitNum = 0;
        public int limit = 0;

        public bool Spawn(float _posZ, Transform reObj)
        {
            if (limit > 0)
            {
                limit--;
                return false;
            }
            if (Random.Range(0f, 1f) > spawaRate) { return false; }

            float randomX = Random.Range(spawnConditionMin.x, spawnConditionMax.x);
            float randomy = Random.Range(spawnConditionMin.y, spawnConditionMax.y);

            Vector3 pos = new Vector3(randomX, randomy, _posZ);

            foreach (Transform t in reObj)
            {
                if (!t.gameObject.activeSelf)
                {
                    //Ç†ÇÈèÍçá
                    t.transform.position = pos;
                    t.gameObject.SetActive(true);
                    limit = limitNum;
                    return true;
                }
            }
            //Ç»Ç¢èÍçá
            Instantiate(obj, pos, Quaternion.identity, reObj);
            limit = limitNum;
            return true;

        }

        void Start()
        {
            
        }
    }

    IEnumerator Start()
    {
        Transform reObj = new GameObject("OBJ").transform;

        while (posZ < 100)
        {
            //Debug.Log(posZ);
            foreach (var mapObjct in enemys)
            {
                bool spawned = mapObjct.Spawn(posZ,reObj);

                if (spawned)
                {
                    posZ += 1;
                    break;
                }
            }
            posZ++;
            yield return null;
        }

        yield break;
    }
}
