using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ef_DestroySelf : MonoBehaviour {

	public float time = 1f;
	void Start()
    {
        Destroy(gameObject, time);
    }

}
