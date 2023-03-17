using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour, IProvidable
{
    public List<Sprite> BlueCubeSprites = new List<Sprite>();
    public List<Sprite> GreenCubeSprites = new List<Sprite>();
    public List<Sprite> PinkCubeSprites = new List<Sprite>();
    public List<Sprite> PurpleCubeSprites = new List<Sprite>();
    public List<Sprite> RedCubeSprites = new List<Sprite>();
    public List<Sprite> YellowCubeSprites = new List<Sprite>();

    void Awake()
    {
        Services.Register(this);
    }
}
