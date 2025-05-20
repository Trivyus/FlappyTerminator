using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingGround : MonoBehaviour
{
    [SerializeField] private Transform[] _groundPieces;
    [SerializeField] private Camera _mainCamera;

    private float _spriteWidth;
    private float _nextSwapPointX;

    private void Start()
    {
        _spriteWidth = _groundPieces[0].GetComponent<SpriteRenderer>().bounds.size.x;

        for (int i = 1; i < _groundPieces.Length; i++)
            _groundPieces[i].position = new Vector3(_groundPieces[i - 1].position.x + _spriteWidth, _groundPieces[i - 1].position.y, _groundPieces[i - 1].position.z);

        _nextSwapPointX = _groundPieces[^1].position.x;
    }

    void Update()
    {
        float cameraRightEdge = _mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;

        if (cameraRightEdge > _nextSwapPointX)
        {
            Transform leftmostPiece = _groundPieces[0];

            for (int i = 1; i < _groundPieces.Length; i++)
                if (_groundPieces[i].position.x < leftmostPiece.position.x)
                    leftmostPiece = _groundPieces[i];

            leftmostPiece.position = new Vector3(_nextSwapPointX + _spriteWidth, leftmostPiece.position.y, leftmostPiece.position.z);

            _nextSwapPointX += _spriteWidth;
        }
    }
}