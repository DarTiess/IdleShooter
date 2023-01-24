using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTreeStack : MonoBehaviour
{
    [SerializeField]private GameObject _treeBlock;
    [SerializeField] private int _countBlock=15;
    [SerializeField]private Transform _blockPlace;
   [SerializeField] private List<GameObject> _blockList= new List<GameObject>();
    GameObject _block;
    private int _indexBlock=0;
    private float _yPosForBlock;
    [SerializeField]private float _jumpForce;
    [SerializeField]private float _boxHeight;
       [Header("Magnet")]
    [SerializeField] private float _timeMagnet;
    [SerializeField] private int _countSteepMagnet;
    [SerializeField] private AnimationCurve _changeY;
    private float _steep;
    private float _timeInSteep;
    // Start is called before the first frame update
    void Start()
    {
           _steep = 1f / _countSteepMagnet;
        _timeInSteep = _timeMagnet / _countSteepMagnet;
        _block=Instantiate(_treeBlock, _blockPlace.position, _blockPlace.rotation);
        _block.SetActive(false);
    // InitStackOfTreeBlocks();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitStackOfTreeBlocks()
    {
        for (int i = 0; i < _countBlock; i++)
        {
            GameObject block=Instantiate(_treeBlock, _blockPlace.position, _blockPlace.rotation);
            block.transform.parent = _blockPlace;
            block.SetActive(false);
            _blockList.Add(block);
            _yPosForBlock=_blockPlace.position.y;
        }
    }

    bool _isGettingTree;
    public void TakeTreeBlockToStack(Transform treePosition)
    {
        if(!_isGettingTree)
        {
            _isGettingTree = true;
            if (_indexBlock < _blockList.Count)
        {
        Debug.Log("getTree");
                
            /*    _blockList[_indexBlock].transform.position=treePosition.position;
         
        _blockList[_indexBlock].gameObject.SetActive(true);
         _blockList[_indexBlock].transform.DOJump(new Vector3(_blockPlace.position.x, _yPosForBlock, _blockPlace.position.z), _jumpForce, 1, 1.5f).
                OnComplete(() =>
                {
                    _blockList[_indexBlock].transform.parent=_blockPlace;
                    _blockList[_indexBlock].transform.position = new Vector3(_blockPlace.position.x, _yPosForBlock, _blockPlace.position.z);
                     
                  _yPosForBlock+=_boxHeight;
                    _indexBlock++;
                    _isGettingTree=false;
                });*/

                StartCoroutine(Taking(treePosition));
        }
        }
        
    }
  
   private IEnumerator Taking(Transform treePos)
    {
        _block.SetActive(true);
         _block.transform.position=treePos.position;
         _block.transform.parent=null;
        _blockList[_indexBlock].gameObject.SetActive(true);
        for (int i = 0; i <= _countSteepMagnet; i++)
        {

            Vector3 pos = Vector3.Lerp(treePos.position,new Vector3(_blockPlace.position.x, _yPosForBlock, _blockPlace.position.z), i * _steep);
            pos.y += _changeY.Evaluate(i * _steep);
            _block.transform.position = pos;

           _block.transform.rotation = Quaternion.Lerp(treePos.rotation, _blockPlace.transform.rotation, i * _steep);
            
            
            yield return new WaitForSeconds(_timeInSteep);
         
        }
        _block.transform.parent = _blockPlace.transform;
        _block.transform.position = new Vector3(_blockPlace.position.x, _yPosForBlock, _blockPlace.position.z);
       _block.transform.rotation = _blockPlace.transform.rotation;
        _yPosForBlock+=_boxHeight;
                    _indexBlock++;
         _isGettingTree=false;
        _block.SetActive(false);
    }
}