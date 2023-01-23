using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform firePlace;
    [SerializeField] private GameObject bulletPref;
    [SerializeField] private int countBullets;
    [SerializeField] private float _attackDuration;
    [SerializeField] private int _attackPower;
    List<GameObject> _bulletsList=new List<GameObject>();
   
     GameObject currentBullet;

    private bool _isOnAtack;
   
   
    private void Start()
    {
        InitBullets();
    }
    public void InitBullets()
    {
        for (int i = 0; i < countBullets; i++)
        {
            var bullet=Instantiate(bulletPref, firePlace.position, firePlace.rotation);
            bullet.SetActive(false);
            bullet.transform.parent = gameObject.transform;
           bullet.transform.position =firePlace.position;
            _bulletsList.Add(bullet);
        }
    }
    public void AttackEnemy(Transform enemyPos)
      {              
         if (!_isOnAtack)
         {
              _isOnAtack= true;
              PushBalls(enemyPos);
         }  
     } 
      void PushBalls(Transform target)
      {        
           currentBullet=GetFreeBall();
           currentBullet.transform.position =firePlace.position; 
            currentBullet.transform.parent = gameObject.transform;
            currentBullet.SetActive(true);
            currentBullet.transform.DOMove(target.position+ new Vector3(0,1.5f,0), _attackDuration).OnComplete(() =>
            {
               target.gameObject.GetComponent<EnemyMovement>().TakeDamage(_attackPower);
               currentBullet.transform.position = target.transform.position;
               currentBullet.transform.parent = target.transform;
               currentBullet.SetActive(false);
                _isOnAtack = false;
              
            });
      }
  
    private GameObject GetFreeBall()
    {
       GameObject bull=null;
        foreach (var bullet in _bulletsList)
        {
            if (!bullet.activeInHierarchy)
            {
               bull= bullet;
                break;
            }
        }
        return bull;
    }
}
