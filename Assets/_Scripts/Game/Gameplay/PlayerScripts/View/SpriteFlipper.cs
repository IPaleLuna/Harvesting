using UnityEngine;

public class SpriteFlipper
{
    private readonly SpriteRenderer _playerSpriteRenderer;
    private readonly Transform _shadowSpriteTransform;

    private readonly Vector2 _shadowRight;
    private readonly Vector2 _shadowLeft;
    
    private bool isFlipped = false; 

    public SpriteFlipper(SpriteRenderer playerSpriteRenderer, Transform shadowSpriteTransform)
    {
        _playerSpriteRenderer = playerSpriteRenderer;
        _shadowSpriteTransform = shadowSpriteTransform;
        
        _shadowRight = _shadowSpriteTransform.localPosition;
        _shadowLeft = _shadowRight;
        _shadowLeft.x *= -1;
    }

    public bool IsFlip(Vector2 direction)
    {
        if(direction.x < 0)
            isFlipped = true;
        else if(direction.x > 0)
            isFlipped = false;
        
        return isFlipped;
    }
    
    public void ApplyFlip(bool value)
    {
        _playerSpriteRenderer.flipX = value;
        _shadowSpriteTransform.localPosition = value ? _shadowLeft : _shadowRight;
    }
}
