using UnityEngine;

public class GameStats : MonoBehaviour
{
    public int shotsFired = 0;
    public int ammoPickedUp = 0;
    public int dummiesKilled = 0;

    public void RegisterShot()
    {
        shotsFired++;
    }

    public void RegisterAmmoPickup()
    {
        ammoPickedUp++;
    }

    public void RegisterDummyKilled()
    {
        dummiesKilled++;
    }

    public void ResetStats()
    {
        shotsFired = 0;
        ammoPickedUp = 0;
        dummiesKilled = 0;
    }
}