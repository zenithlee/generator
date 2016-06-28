using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//max performace pool of bullets
public class BulletPool : MonoBehaviour {

  public GameObject BulletTemplate;
  List<GameObject> Bullets = new List<GameObject>();
  int PoolSize = 300; //the higher this number, the longer the game will take to start up, but reuse will be instant
  int PoolIndex = 0;

  //grabs an object from the pool, or reuses an old one
  public GameObject RecycleBullet()
  {
    PoolIndex++;
    if (PoolIndex >= Bullets.Count) PoolIndex = 0;
    return Bullets[PoolIndex];    
  }

	// Use this for initialization
	void Start () {
    for (int i = 0; i < PoolSize; i++)
    {
      GameObject o = Instantiate(BulletTemplate);
      o.GetComponent<Rigidbody>().Sleep();
      o.transform.parent = this.transform;
      o.transform.localPosition = new Vector3(-1000, 0, 0);
      Bullets.Add(o);
    }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
