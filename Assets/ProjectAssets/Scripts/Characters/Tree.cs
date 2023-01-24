using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Tree : MonoBehaviour
{
    [SerializeField]private List<Mesh> _meshesList;
    int indexMesh=0;
    private MeshFilter _meshFilter;

    PlayerMovement _player;
    [Inject]
    private void Initialization(PlayerMovement player)
    {
        _player = player;
    }
    // Start is called before the first frame update
    void Start()
    {
        _meshFilter= GetComponent<MeshFilter>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Melee"))
        {
           ChangeMesh();
        }
    }
    void ChangeMesh()
    {
        if(indexMesh<_meshesList.Count)
        {
             _meshFilter.mesh=_meshesList[indexMesh];
             indexMesh++;
           _player.GetTreeBlock(transform);
        }
        else
        {
            _player.StopGettingTree();
            gameObject.SetActive(false);
        }
       
    }
}
