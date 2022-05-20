using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour 
{
    public Text costText;

    public GameObject[] gunGos;

    public Transform bulletHolder;
    public GameObject[] bullet1Gos;
    public GameObject[] bullet2Gos;
    public GameObject[] bullet3Gos;
    public GameObject[] bullet4Gos;
    public GameObject[] bullet5Gos;

    public int lv=0;//等级

    private int costIndex = 0;//炮弹的下标

    //每一炮的花费和伤害
    private int[] oneShootCosts = { 5, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 }; 


    void Update()
    {
        ChangeBulletCost();
        Fire();
    }

    void Fire()
    {
        GameObject[] useBullets= bullet5Gos;
        int bulletIndex;
        if(Input.GetMouseButtonDown(0))
        {
            if(costIndex/4==0)
            {
                useBullets = bullet1Gos;
            }
            else if(costIndex/4==1)
            {
                useBullets = bullet2Gos;
            }
            else if(costIndex/4==2)
            {
                useBullets = bullet3Gos;
            }
            else if(costIndex/4==3)
            {
                useBullets = bullet4Gos;
            }
            else if(costIndex/4==4)
            {
                useBullets = bullet5Gos;
            }
            bulletIndex = lv % 10;
            GameObject bullet=Instantiate(useBullets[bulletIndex]);
            bullet.transform.SetParent(bulletHolder, false);
            bullet.transform.position = gunGos[costIndex / 4].transform.Find("FirePos").transform.position;
            bullet.transform.rotation = gunGos[costIndex / 4].transform.Find("FirePos").transform.rotation;
            bullet.AddComponent<Ef_AutoMove>().speed = 10f;
            bullet.GetComponent<Ef_AutoMove>().dir = Vector3.up;
        }
    }
    void ChangeBulletCost()
    {
        if(Input.GetAxis("Mouse ScrollWheel")<0)
        {
            OnButtonSubDown();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            OnButtonAddDown();
        }
    }

    public void OnButtonAddDown()
    {
        gunGos[costIndex / 4].SetActive(false);
        costIndex=(costIndex+1)%oneShootCosts.Length;
        gunGos[costIndex / 4].SetActive(true);
        costText.text = "$" + oneShootCosts[costIndex];
    }

    public void OnButtonSubDown()
    {
        gunGos[costIndex / 4].SetActive(false);
        costIndex = (costIndex - 1 + oneShootCosts.Length) % oneShootCosts.Length;
        gunGos[costIndex / 4].SetActive(true);
        costText.text = "$" + oneShootCosts[costIndex];
    }


}
