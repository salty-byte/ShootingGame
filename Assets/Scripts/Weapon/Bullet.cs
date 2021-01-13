using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
  [SerializeField]
  int speed = 10;
  public int Speed
  {
    get => speed;
    set => speed = value;
  }

  [SerializeField]
  int power = 1;
  public int Power
  {
    get => power;
    set => power = value;
  }

  public void SetVelocity(Vector3 direction)
  {
    GetComponent<Rigidbody2D>().velocity = direction.normalized * speed;
  }
}
