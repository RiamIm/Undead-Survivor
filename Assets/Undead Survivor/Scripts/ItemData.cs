using UnityEngine;


[CreateAssetMenu(fileName = "Item", menuName = "Scriptble Object/ItemData")]
public class ItemData : ScriptableObject // 다양한 데이터를 저장하는 에셋
{
    public enum EItemType
    {
        Melee,
        Range,
        Glove,
        Shoe,
        Heal
    }

    [Header("# Main Info")]
    public EItemType itemType;
    public int itemId;
    public string itemName;
    public string itemDesc;
    public Sprite itemIcon;

    [Header("# Level Data")]
    public float baseDamage;
    public int baseCount;
    public float[] damages;
    public int[] counts;

    [Header("# Weapon")]
    public GameObject projectile;
}
