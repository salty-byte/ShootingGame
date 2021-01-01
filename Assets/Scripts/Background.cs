using UnityEngine;

public class Background : MonoBehaviour
{
  [SerializeField]
  float speed = 1f;

  Renderer render;

  void Start()
  {
    render = GetComponent<Renderer>();
  }

  public void SetLoopTime(float time)
  {
    speed = time > 0 ? 1f / time : 1f;
  }

  public void CalcOffset(float elapsedTime)
  {
    float y = Mathf.Repeat(elapsedTime * speed, 1f);
    render.sharedMaterial.SetTextureOffset("_MainTex", new Vector2(0f, y));
  }
}
