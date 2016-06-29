using UnityEngine;
using System.Collections;

public class Spinner : MonoBehaviour {

  public float Speed = 5;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
    this.transform.localRotation *= Quaternion.Euler(Time.deltaTime* Speed, Time.deltaTime* Speed, Time.deltaTime* Speed);
	}
}
