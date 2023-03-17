using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private List<Sprite> _sprites = new List<Sprite>();
    private CubeType _cubeType;
    public FallAnimation FallAnimation;
    private Cell _cell;
    public Cell Cell
    {
        get { return _cell; }
        set
        {
            if (_cell == value) return;

            var oldCell = _cell;
            _cell = value;
            
            if (oldCell != null && Equals(oldCell.Cube, this))
            {
                oldCell.Cube = null;
            }

            if (value != null)
            {
                value.Cube = this;
                gameObject.name = _cell.gameObject.name + " " + GetType().Name;
                _spriteRenderer.sortingOrder = _cell.Y + 10;
            }
        }
    }
    public void PrepareCube(CubeBase cubeBase, CubeType cubeType)
    {
        _cubeType = cubeType;
        FallAnimation = cubeBase.FallAnimation;
        FallAnimation.Cube = this;

        switch (_cubeType)
        {
            case CubeType.BlueCube:
                _sprites = Services.GetSprite.BlueCubeSprites;
                _spriteRenderer = AddSprite(_sprites[0]);
                break;
            case CubeType.GreenCube:
                _sprites = Services.GetSprite.GreenCubeSprites;
                _spriteRenderer = AddSprite(_sprites[0]);
                break;
            case CubeType.PinkCube:
                _sprites = Services.GetSprite.PinkCubeSprites;
                _spriteRenderer = AddSprite(_sprites[0]);
                break;
            case CubeType.PurpleCube:
                _sprites = Services.GetSprite.PurpleCubeSprites;
                _spriteRenderer = AddSprite(_sprites[0]);
                break;
            case CubeType.RedCube:
                _sprites = Services.GetSprite.RedCubeSprites;
                _spriteRenderer = AddSprite(_sprites[0]);
                break;
            case CubeType.YellowCube:
                _sprites = Services.GetSprite.YellowCubeSprites;
                _spriteRenderer = AddSprite(_sprites[0]);
                break;
        }
    }

    public void Blast()
    {
        Services.GetParticle.PlayParticle(this);
        Cell.Cube = null;
        Cell = null;
        Destroy(gameObject);
    }

    public void RemoveCube()
    {
        Cell.Cube = null;
        Cell = null;
        Destroy(gameObject);
    }

    public void Fall()
    {
        FallAnimation.StartFall(Cell.GetFallTarget());
    }

    public CubeType GetCubeType()
    {
        return _cubeType;
    }

    public SpriteRenderer AddSprite(Sprite sprite)
    {
        var spriteRenderer = new GameObject("Sprite_0").AddComponent<SpriteRenderer>();
        spriteRenderer.transform.SetParent(transform);
        spriteRenderer.sprite = sprite;
        return spriteRenderer;
    }
    public void SetSpriteAccordingTo(int hintLevel)
    {
        _spriteRenderer.sprite = _sprites[hintLevel];
    }
}
