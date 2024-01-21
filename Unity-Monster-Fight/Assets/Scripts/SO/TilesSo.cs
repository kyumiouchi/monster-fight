using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Data/Tiles Data", fileName = "Background_SO")]
public class TilesSo : ScriptableObject
{
    [Header("Tiles Data")] 
    [SerializeField] private Sprite[] _sprites;

    public int Count => _sprites.Length;

    public Sprite GetSpriteByPosition(int position)
    {
        return _sprites[position];
    }

    public Sprite this[int position] => _sprites[position];
}
