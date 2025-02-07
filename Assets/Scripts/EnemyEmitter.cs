﻿using System.Collections;
using UnityEngine;

public class EnemyEmitter : SingletonMonoBehaviour<EnemyEmitter>
{
  public enum Direction
  {
    UP,
    DOWN,
    LEFT,
    RIGHT,
  }

  [SerializeField]
  GameObject[] enemyObjects = default;

  [SerializeField]
  RectTransform fieldRectTransform = default;

  float time;

  EmitItem[] items;

  int index;

  bool isPlaying;
  public bool IsPlaying
  {
    get => isPlaying;
    set => isPlaying = value;
  }

  MoveManager moveManager;

  GameObject objectField;

  void Start()
  {
    moveManager = MoveManager.Instance;
  }

  public void Initialize(GameObject field)
  {
    this.objectField = field;

    items = new EmitItem[] {
      new EmitItem(1, Direction.UP, 6.0f, 0.2f, hp: 8, point: 100, moveType: 2),
      new EmitItem(1, Direction.UP, 6.5f, 0.2f, hp: 8, point: 100, moveType: 2),
      new EmitItem(1, Direction.UP, 7.0f, 0.2f, hp: 8, point: 100, moveType: 2),
      new EmitItem(1, Direction.UP, 7.0f, 0.7f, hp: 8, point: 100, moveType: 4),
      new EmitItem(1, Direction.UP, 7.5f, 0.2f, hp: 8, point: 100, moveType: 2),
      new EmitItem(1, Direction.UP, 8.0f, 0.2f, hp: 8, point: 100, moveType: 2),
      new EmitItem(0, Direction.UP, 8.0f, 0.8f, hp: 8, point: 100, moveType: 1),
      new EmitItem(1, Direction.UP, 12.0f, 0.7f, hp: 8, point: 100, moveType: 2),
      new EmitItem(1, Direction.UP, 12.5f, 0.7f, hp: 8, point: 100, moveType: 2),
      new EmitItem(1, Direction.UP, 13.0f, 0.7f, hp: 8, point: 100, moveType: 2),
      new EmitItem(1, Direction.UP, 13.5f, 0.7f, hp: 8, point: 100, moveType: 2),
      new EmitItem(1, Direction.UP, 14.0f, 0.7f, hp: 8, point: 100, moveType: 2),
      new EmitItem(0, Direction.UP, 16.0f, 0.3f, hp: 8, point: 100, moveType: 1),
      new EmitItem(1, Direction.UP, 18.0f, 0.1f, hp: 8, point: 100, moveType: 2),
      new EmitItem(1, Direction.UP, 18.5f, 0.1f, hp: 8, point: 100, moveType: 2),
      new EmitItem(1, Direction.UP, 19.0f, 0.1f, hp: 8, point: 100, moveType: 2),
      new EmitItem(1, Direction.UP, 19.0f, 0.7f, hp: 8, point: 100, moveType: 4),
      new EmitItem(1, Direction.UP, 19.5f, 0.1f, hp: 8, point: 100, moveType: 2),
      new EmitItem(1, Direction.UP, 20.0f, 0.1f, hp: 8, point: 100, moveType: 2),
      new EmitItem(0, Direction.UP, 23.0f, 0.5f, hp: 8, point: 100, moveType: 1),
      new EmitItem(1, Direction.UP, 24.0f, 0.4f, hp: 8, point: 100, moveType: 3),
      new EmitItem(1, Direction.UP, 24.5f, 0.4f, hp: 8, point: 100, moveType: 3),
      new EmitItem(1, Direction.UP, 25.0f, 0.4f, hp: 8, point: 100, moveType: 3),
      new EmitItem(1, Direction.UP, 25.5f, 0.8f, hp: 8, point: 100, moveType: 3),
      new EmitItem(1, Direction.UP, 28.0f, 0.4f, hp: 8, point: 100, moveType: 3),
      new EmitItem(1, Direction.UP, 29.0f, 0.9f, hp: 8, point: 100, moveType: 3),
      new EmitItem(1, Direction.UP, 32.0f, 0.4f, hp: 8, point: 100, moveType: 3),
      new EmitItem(1, Direction.LEFT, 32.5f, 0.8f, hp: 8, point: 100, moveType: 5),
      new EmitItem(1, Direction.UP, 34.0f, 0.7f, hp: 8, point: 100, moveType: 3),
      new EmitItem(1, Direction.RIGHT, 35.0f, 0.9f, hp: 8, point: 100, moveType: 5),
      new EmitItem(2, Direction.UP, 40.0f, 0.5f, hp: 100, point: 1000),
    };

    time = 0f;
    index = 0;
  }

  public IEnumerator Loop()
  {
    isPlaying = true;

    while (isPlaying && index < items.Length)
    {
      time += Time.deltaTime;

      while (index < items.Length && items[index].Time <= time)
      {
        Emit(items[index]);
        index++;
      }

      yield return new WaitForEndOfFrame();
    }
  }

  void Emit(EmitItem item)
  {
    var corners = new Vector3[4];
    fieldRectTransform.GetWorldCorners(corners);
    Vector3 min = corners[0];
    Vector3 max = corners[2];
    var position = new Vector3(0, 0, 0);

    switch (item.Direction)
    {
      case Direction.DOWN:
        position.x = Mathf.Lerp(min.x, max.x, item.Position);
        position.y = min.y;
        break;
      case Direction.LEFT:
        position.x = min.x;
        position.y = Mathf.Lerp(min.y, max.y, item.Position);
        break;
      case Direction.RIGHT:
        position.x = max.x;
        position.y = Mathf.Lerp(min.y, max.y, item.Position);
        break;
      default:
        position.x = Mathf.Lerp(min.x, max.x, item.Position);
        position.y = max.y;
        break;
    }

    var obj = Instantiate(
      enemyObjects[item.EnemyId],
      position,
      transform.rotation,
      objectField.transform
    );
    moveManager.AppendSequence(MoveModels.Create(obj.transform, item.MoveType));
    var enemy = obj.GetComponent<Enemy>();
    enemy.Hp = item.Hp;
    enemy.Point = item.Point;
  }

  public GameObject GetEmitField()
  {
    return objectField;
  }
}
