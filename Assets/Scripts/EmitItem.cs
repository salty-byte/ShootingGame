public class EmitItem
{
  public int enemyId { get; private set; }

  public EnemyEmitter.Direction direction { get; private set; }

  public int hp { get; private set; }

  public int point { get; private set; }

  public float position { get; private set; }

  public float rotation { get; private set; }

  public float scale { get; private set; }

  public float time { get; private set; }

  public int moveType { get; private set; }

  public EmitItem(
    int id,
    EnemyEmitter.Direction direction,
    float time,
    float position = 0.5f,
    float rotation = 0f,
    float scale = 1f,
    int moveType = 0,
    int hp = 10,
    int point = 100
  )
  {
    this.enemyId = id;
    this.direction = direction;
    this.time = time;
    this.position = position;
    this.rotation = rotation;
    this.scale = scale;
    this.moveType = moveType;
    this.hp = hp;
    this.point = point;
  }
}
