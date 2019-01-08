using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Astofena_spiders : MonoBehaviour, IAbility
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("start");
            Action();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            CancelInvoke("InstanceSpider");
        }
    }

    public void Action()
    {
        InvokeRepeating("InstanceSpider", 0f, 1f);
    }

    [SerializeField]
    GameObject spiderPref;
    [SerializeField]
    Transform leftBound;
    [SerializeField]
    Transform rightBound;
    void InstanceSpider ()
    {
        Debug.Log("1");
        float xPositionSpider = Random.Range(leftBound.position.x, rightBound.position.x);
        GameObject zombyInstance = Instantiate(spiderPref, new Vector3(xPositionSpider, leftBound.position.y, 0f), Quaternion.identity);
        Enemy_invokedZomby spiderScript = zombyInstance.GetComponent<Enemy_invokedZomby>();
        spiderScript.Alert(GetComponent<Enemy>().target);
    }
}
