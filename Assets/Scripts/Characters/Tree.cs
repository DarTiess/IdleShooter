using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(MeshFilter))]
public class Tree : MonoBehaviour
{
    [SerializeField] private List<Mesh> _meshesList;
    private int indexMesh = 0;
    private MeshFilter _meshFilter;

    private PlayerMovement _player;
    [Inject]
    private void Initialization(PlayerMovement player)
    {
        _player = player;
    }
    // Start is called before the first frame update
    void Start()
    {
        _meshFilter = GetComponent<MeshFilter>();
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
        if (indexMesh < _meshesList.Count)
        {
            _meshFilter.mesh = _meshesList[indexMesh];
            indexMesh++;
        }
        else
        {
            _player.StopGettingTree();
            gameObject.SetActive(false);
        }

    }
}
