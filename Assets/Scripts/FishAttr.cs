using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAttr : MonoBehaviour 
{
	public int maxNum;
	public int maxSpeed;

	private void OnTriggerEnter2D(Collider2D collision)
    {
		if(collision.tag=="Border")
        {
            Destroy(gameObject);
        }
    }
}
