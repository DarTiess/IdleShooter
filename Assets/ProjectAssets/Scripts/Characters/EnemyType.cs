
using UnityEngine;

public enum EnemyBehavior
 {
     Moved,
     Fixed
 }
   public enum EnemyMove
    {
        Walking,
        Flying
    }


[System.Serializable]
    public class Enemy
    {

         public EnemyBehavior _behavior;
         public EnemyMove _typeOfMove;
         public float _speedMove;
         public float _distanceFromPlayer;
         public float _timeToStay;
         public int _health;
         public float _speedAttack;
         public int _makeDamage;
         public EnemyMovement enemyPref;
    }