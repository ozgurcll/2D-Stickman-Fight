using UnityEngine;

public enum EquipmentType
{
    Weapon,
    Potion
}

public enum WeaponType
{
    Gun,
    Sword,
    Potion
}

[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Equipment")]
public class ItemData_Equipment : ItemData
{
    public EquipmentType equipmentType;
    public WeaponType weaponType;

    public int damage;
    public int critChance;
    public int critPower;
    public int health;
    public int addHealth;

    public void AddModifier()
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
        AddLayer();

        playerStats.damage.AddModifier(damage);
        playerStats.critChance.AddModifier(critChance);
        playerStats.critPower.AddModifier(critPower);
        playerStats.maxHealth.AddModifier(health);
        playerStats.IncreaseHealthBy(addHealth);
    }


    public void RemoveModifier()
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
        RemoveLayer();

        playerStats.damage.RemoveModifier(damage);
        playerStats.critChance.RemoveModifier(critChance);
        playerStats.critPower.RemoveModifier(critPower);
        playerStats.maxHealth.RemoveModifier(health);
    }
    private void AddLayer()
    {
        if (weaponType == WeaponType.Gun)
        {
            PlayerManager.instance.player.haveGun = true;
            PlayerManager.instance.player.haveSword = false;
            PlayerManager.instance.player.anim.SetBool("HaveGun", true);
            PlayerManager.instance.player.anim.SetBool("HaveSword", false);
            PlayerManager.instance.player.anim.SetLayerWeight(2, 1);
            PlayerManager.instance.player.anim.SetLayerWeight(1, 0);
            PlayerManager.instance.player.anim.SetLayerWeight(0, 0);
        }
        else if (weaponType == WeaponType.Sword)
        {
            PlayerManager.instance.player.haveGun = false;
            PlayerManager.instance.player.haveSword = true;
            PlayerManager.instance.player.anim.SetBool("HaveSword", true);
            PlayerManager.instance.player.anim.SetBool("HaveGun", false);
            PlayerManager.instance.player.anim.SetLayerWeight(1, 1);
            PlayerManager.instance.player.anim.SetLayerWeight(2, 0);
            PlayerManager.instance.player.anim.SetLayerWeight(0, 0);
        }
    }

    private void RemoveLayer()
    {
        if (weaponType == WeaponType.Gun)
        {
            PlayerManager.instance.player.haveGun = false;
            PlayerManager.instance.player.haveSword = false;
            PlayerManager.instance.player.anim.SetBool("HaveSword", false);
            PlayerManager.instance.player.anim.SetBool("HaveGun", false);
            PlayerManager.instance.player.anim.SetLayerWeight(0, 1);
            PlayerManager.instance.player.anim.SetLayerWeight(1, 0);
            PlayerManager.instance.player.anim.SetLayerWeight(2, 0);
        }
        else if (weaponType == WeaponType.Sword)
        {
            PlayerManager.instance.player.haveGun = false;
            PlayerManager.instance.player.haveSword = false;
            PlayerManager.instance.player.anim.SetBool("HaveSword", false);
            PlayerManager.instance.player.anim.SetBool("HaveGun", false);
            PlayerManager.instance.player.anim.SetLayerWeight(0, 1);
            PlayerManager.instance.player.anim.SetLayerWeight(1, 0);
            PlayerManager.instance.player.anim.SetLayerWeight(2, 0);
        }
    }
}
