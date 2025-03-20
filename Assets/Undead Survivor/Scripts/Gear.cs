using UnityEngine;

public class Gear : MonoBehaviour
{
    public ItemData.EItemType type;
    public float rate;

    Player player;
    private void Awake()
    {
        player = GameManager.instance.player;
    }
    public void Init(ItemData data)
    {
        // Basic Set
        name = "Gear " + data.itemId;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;

        // Property Set
        type = data.itemType;
        rate = data.damages[0];
        ApplyGear();
    }

    public void LevelUp(float rate)
    {
        this.rate = rate;
        ApplyGear();
    }

    void ApplyGear()
    {
        switch (type)
        {
            case ItemData.EItemType.Glove:
                RateUp();
                break;
            case ItemData.EItemType.Shoe:
                speedUp();
                break;
        }
    }

    void RateUp()
    {
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();

        foreach(var weapon in weapons)
        {
            switch (weapon.id)
            {
                case 0:
                    weapon.speed = 150 + (150 * rate);
                    break;
                default:
                    weapon.speed = 0.5f * (1f - rate);
                    break;
            }
        }
    }

    void speedUp()
    {
        float speed = 3;
        player.speed = speed + speed * rate;
    }
}
