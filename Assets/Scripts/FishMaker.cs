using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMaker : MonoBehaviour 
{
	public Transform[] genPostions;
	public GameObject[] fishPrefabs;
	public Transform fishHolder;
	public float waveGenTime = 0.3f;
	public float fishGenTime=0.5f;
	// Use this for initialization
	void Start () 
	{
		InvokeRepeating("MakeFishes", 0,waveGenTime);
	}
	void MakeFishes()
    {
		int genPosIndex = Random.Range(0, genPostions.Length);
		int fishPreIndex = Random.Range(0, fishPrefabs.Length);
		int maxNum = fishPrefabs[fishPreIndex].GetComponent<FishAttr>().maxNum;
		int maxSpeed = fishPrefabs[fishPreIndex].GetComponent<FishAttr>().maxSpeed;
		int num = Random.Range((maxNum / 2) + 1, maxNum);
		int speed = Random.Range((maxSpeed / 2)+1, maxSpeed);
		int moveType = Random.Range(0, 2); //0--直线 1--曲线
		int angOffset; //直线的倾斜角
		int angSpeed;  //曲线的角速度

		if(moveType==0)
        {
			//直走
			angOffset = Random.Range(-22, 22);
			StartCoroutine(GenStraightFish(genPosIndex, fishPreIndex, num, speed, angOffset));
        }
        else
        {
			//转弯
			if(Random.Range(0,2)==0)
            {
				angSpeed = Random.Range(-15, -9);
            }
			else
            {
				angSpeed = Random.Range(9, 15);
            }
			StartCoroutine(GenTurnFish(genPosIndex, fishPreIndex, num, speed, angSpeed));
        }

	}
	IEnumerator GenStraightFish(int genPosIndex,int fishPreIndex,int num,int speed,int angOffset)
    {
		for(int i=0;i<num;i++)
        {
			GameObject fish= Instantiate(fishPrefabs[fishPreIndex]);
			fish.transform.SetParent(fishHolder, false);
			fish.transform.localPosition = genPostions[genPosIndex].localPosition;
			fish.transform.localRotation = genPostions[genPosIndex].localRotation;
			fish.transform.Rotate(0, 0, angOffset);
			fish.GetComponent<SpriteRenderer>().sortingOrder += i;
			fish.AddComponent<Ef_AutoMove>().speed = speed;
			yield return new WaitForSeconds(fishGenTime);
        }
    }
	IEnumerator GenTurnFish(int genPosIndex, int fishPreIndex, int num, int speed, int angSpeed)
	{
		for (int i = 0; i < num; i++)
		{
			GameObject fish = Instantiate(fishPrefabs[fishPreIndex]);
			fish.transform.SetParent(fishHolder, false);
			fish.transform.localPosition = genPostions[genPosIndex].localPosition;
			fish.transform.localRotation = genPostions[genPosIndex].localRotation;
			fish.GetComponent<SpriteRenderer>().sortingOrder += i;
			fish.AddComponent<Ef_AutoMove>().speed = speed;
			fish.AddComponent<Ef_AutoRotate>().speed = angSpeed;
			yield return new WaitForSeconds(fishGenTime);
		}
	}
}
