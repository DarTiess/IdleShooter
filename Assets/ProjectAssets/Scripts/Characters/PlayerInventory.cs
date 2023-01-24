using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]private GameObject _gun;
    [SerializeField]private GameObject _melee;
    // Start is called before the first frame update
    void Start()
    {
       GetGun();
    }

    public void GetGun()
    {
         _gun.gameObject.SetActive(true);
        _melee.gameObject.SetActive(false);
    }

    public void GetMelee()
    {
         _gun.gameObject.SetActive(false);
        _melee.gameObject.SetActive(true);
    }
}
