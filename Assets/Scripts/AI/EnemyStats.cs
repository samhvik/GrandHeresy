using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// basic stat sheet for AI
[CreateAssetMenu (menuName = "AI/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    public int moveSpeed   = 1;
    public int attackRange = 2;
    public int lookRange = 80;
    public float lookSphereCastRadius = 2.0f;
    public float searchTurnSpeed = 120f;
    public float searchDuration = 8.0f;
    public int  damage = 50;
}
