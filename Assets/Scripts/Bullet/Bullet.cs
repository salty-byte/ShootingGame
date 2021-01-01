using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
  public int speed = 10;

  public int power = 1;

  public void SetVelocity(Vector3 direction)
  {
    GetComponent<Rigidbody2D>().velocity = direction.normalized * speed;
  }
}