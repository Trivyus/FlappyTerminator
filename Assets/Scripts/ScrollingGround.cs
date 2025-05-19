using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingGround : MonoBehaviour
{
    [SerializeField] private Transform[] _groundPieces;
    [SerializeField] private Camera _mainCamera;

    private float _spriteWidth;
    private float _nextSwapPointX;

    void Start()
    {
        _spriteWidth = _groundPieces[0].GetComponent<SpriteRenderer>().bounds.size.x;
        _nextSwapPointX = _groundPieces[0].position.x + _spriteWidth;
        _groundPieces[1].position = new Vector3(_nextSwapPointX, _groundPieces[0].position.y, _groundPieces[0].position.z);
    }

    void Update()
    {
        float cameraRightEdge = _mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;

        if (cameraRightEdge > _nextSwapPointX)
        {
            Transform backPiece = _groundPieces[0].position.x < _groundPieces[1].position.x ? _groundPieces[0] : _groundPieces[1];
            backPiece.position = new Vector3(_nextSwapPointX + _spriteWidth, backPiece.position.y, backPiece.position.z);
            _nextSwapPointX += _spriteWidth;
        }
    }
}