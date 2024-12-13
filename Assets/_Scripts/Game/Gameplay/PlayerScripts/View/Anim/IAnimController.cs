using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAnimController
{
    public void OnInputDirectionChanged(Vector2 direction);
    public void ResetAnim();
    
    public void SetSpriteLib(int index);
}
