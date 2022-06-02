using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameController : MonoBehaviour 
{
    public Text goldText;
    public Text lvText;
    public Text lvNameText;
    public Text smallCountdownText;
    public Text bigCountdownText;

    public Button bigCountdownButton;
    public Button backButton;
    public Button settingButton;

    public Slider expSlider;

    public Text costText;

    public GameObject[] gunGos;

    public Transform bulletHolder;
    public GameObject[] bullet1Gos;
    public GameObject[] bullet2Gos;
    public GameObject[] bullet3Gos;
    public GameObject[] bullet4Gos;
    public GameObject[] bullet5Gos;

    public int lv=0;//等级
    public int gold = 500;
    public int exp = 0;
    public const int bigCountdown=240;
    public const int smallCountdown = 60;
    public float bigTimer=bigCountdown;
    public float smallTimer=smallCountdown;
    

    private int costIndex = 0;//炮弹的下标

    //每一炮的花费和伤害
    private int[] oneShootCosts = { 5, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };

    private string[] lvNames = { "新手", "入门", "黑铁", "青铜", "白银", "黄金", "白金", "钻石", "大师", "宗师" };

    void UpdateUI()
    {

        bigTimer -= Time.deltaTime;
        smallTimer -= Time.deltaTime;
        if(smallTimer<=0)
        {
            smallTimer = smallCountdown;
            gold += 50;
        }
        if(bigTimer<=0&&bigCountdownButton.gameObject.activeSelf==false)
        {
            bigCountdownText.gameObject.SetActive(false);
            bigCountdownButton.gameObject.SetActive(true);
        }

        //经验等级换算：升级所需经验=1000+200*当前等级
        while(exp>=1000+200*lv)
        {
            exp -= 1000 + 200 * lv;
            lv++;
        }
        goldText.text = "$"+gold.ToString();
        lvText.text = lv.ToString();
        if (lv / 10 <= 9)
        {
            lvNameText.text = lvNames[lv / 10];
        }
        else lvNameText.text = lvNames[9];
        smallCountdownText.text = "  " + (int)smallTimer / 10 + "  " + (int)smallTimer % 10;
        bigCountdownText.text = (int)bigTimer + "s";
        expSlider.value = (float)exp / (1000 + 200 * lv);
    }

    void Update()
    {
        ChangeBulletCost();
        Fire();
        UpdateUI();
    }

    void Fire()
    {
        GameObject[] useBullets= bullet5Gos;
        int bulletIndex;
        if(Input.GetMouseButtonDown(0)&&!EventSystem.current.IsPointerOverGameObject())
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
            bullet.GetComponent<BulletAttr>().damage = oneShootCosts[costIndex];
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
