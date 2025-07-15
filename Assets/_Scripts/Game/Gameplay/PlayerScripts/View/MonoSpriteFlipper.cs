using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class MonoSpriteFlipper : MonoBehaviour, IViewComponent
{
    [SerializeField]
    private SpriteRenderer _playerSpriteRenderer;

    [SerializeField]
    private Transform _shadowSpriteTransform;
    
    private SpriteFlipper _flipper;

    private void OnValidate()
    {
        _playerSpriteRenderer ??= GetComponent<SpriteRenderer>();
    }
    
    private void Awake()
    {
        _flipper = new(_playerSpriteRenderer, _shadowSpriteTransform);
    }

    public void OnInputDirectionChanged(Vector2 direction)
    {
        _flipper.ApplyFlip(_flipper.IsFlip(direction));
    }
}
