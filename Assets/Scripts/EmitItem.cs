public class EmitItem
{
  public int EnemyId { get; private set; }

  public EnemyEmitter.Direction Direction { get; private set; }

  public int Hp { get; private set; }

  public int Point { get; private set; }

  public float Position { get; private set; }

  public float Rotation { get; private set; }

  public float Scale { get; private set; }

  public float Time { get; private set; }

  public int MoveType { get; private set; }

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
    this.EnemyId = id;
    this.Direction = direction;
    this.Time = time;
    this.Position = position;
    this.Rotation = rotation;
    this.Scale = scale;
    this.MoveType = moveType;
    this.Hp = hp;
    this.Point = point;
  }
}
