using UnityEngine;
using System.Collections;

//the centroid of the bullet storm, as well as the generator
public class BulletGenerator : MonoBehaviour {

  public BulletPool Pool1;
  public BulletPool Pool2;
  // Use this for initialization
  void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  //calculates a point on a circle where to move a bullet to
  Vector3 PointOnCircle(int index, float radiusX, float radiusY, int MaxItems)
  {
    var i = (index * 1.0) / MaxItems;
    float angle = (float)i * Mathf.PI * 2.0f;
    var x = Mathf.Sin(angle) * radiusX;
    var y = Mathf.Cos(angle) * radiusY;
    Vector3 pos = new Vector3(x, y, 0) + this.transform.localPosition;

    //float fx = this.transform.localPosition.x + Mathf.Cos((float)(index * Mathf.PI / 180.0f)) * radius;
    //float fy = this.transform.localPosition.y + Mathf.Sin((float)(index * Mathf.PI / 180.0f)) * radius;
    return pos;
  }

  //generates a radial spray of bullets from a pool of bullets
  public void GenerateRadial(BulletPool pool, int NumBullets, float radius, float Power)
  {
    System.Random r = new System.Random();
    
    //create some holes    

    for ( int i = 0; i< NumBullets; i++)
    {
      GameObject g = pool.RecycleBullet();
      Vector3 pos = PointOnCircle(i, radius, radius, NumBullets);
      g.GetComponent<Rigidbody>().MovePosition(pos);
      g.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
      g.GetComponent<Rigidbody>().AddExplosionForce( 1540, this.transform.localPosition, 300, 0, ForceMode.Force);
    }
  }

  public void Test()
  {    
    GenerateRadial(Pool1, 50, 22, 10);
    GenerateRadial(Pool2, 20, 33, 15);
  }
}
