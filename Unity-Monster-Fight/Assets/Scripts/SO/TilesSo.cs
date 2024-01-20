using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Data/Tiles Data", fileName = "Background_SO")]
public class TilesSo : ScriptableObject
{
    [Header("Tiles Data")] 
    [SerializeField] private Sprite[] _sprites;
    public Sprite Sprite { get; protected set; }

    public Sprite GetRandomSprite()
    {
        Sprite = _sprites[Random.Range(0, _sprites.Length)];
        return Sprite;
    }
}
