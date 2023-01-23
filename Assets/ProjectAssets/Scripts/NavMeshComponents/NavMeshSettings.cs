using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshSettings : MonoBehaviour
{

#pragma warning disable 0649 

  
    [SerializeField] private float _timeUpdate;
    private float _timerUpdate;

#pragma warning restore 0649  

    private NavMeshSurface _surface;

    private void Start()
    {
        _surface = GetComponent<NavMeshSurface>();
        _surface.BuildNavMesh();
    }

    void Update()
    {
        
    }

    private void UpdateNavMesh()
    {
        _surface.UpdateNavMesh(_surface.navMeshData);
        _surface.AddData();
    }
    
#if UNITY_EDITOR

    [CustomEditor(typeof(NavMeshSettings))]
    public class ItemEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            NavMeshSettings item = target as NavMeshSettings;
            
            item.Start();
            item.UpdateNavMesh();
        }
    }
#endif
}
