using System.Collections.Generic;

[System.Serializable]
public class Zone
{
    public string zoneName;
    public Dictionary<string, int> currentAmounts = new Dictionary<string, int>();
    public Dictionary<string, int> maxAmounts = new Dictionary<string, int>();

    public List<Clean> blood;
    public int bloodCurrentAmmount = 0;
    public List<DisposableObject> corpses;
    public int corpsesCurrentAmmount = 0;
    public List<Clean> uvCleanables;
    public int uvCleanablesCurrentAmmount = 0;
    public List<Clean> bloodyObjects;
    public int bloodyObjectsCurrentAmmount = 0;
    public List<SnappableObject> arrabgables;
    public int arrabgablesCurrentAmmount = 0;
    public List<RetrievableObject> weapons;
    public int weaponsCurrentAmmount = 0;
    public List<RetrievableObject> documents;
    public int documentsCurrentAmmount = 0;
    public List<RetrievableObject> clothes;
    public int clothesCurrentAmmount = 0;
    public List<RetrievableObject> miscellaneous;
    public int miscellaneousCurrentAmmount = 0;

    public void InitializeZone()
    {
        maxAmounts["blood"] = blood.Count;
        maxAmounts["corpses"] = corpses.Count;
        maxAmounts["uvCleanables"] = uvCleanables.Count;
        maxAmounts["bloodyObjects"] = bloodyObjects.Count;
        maxAmounts["arrabgables"] = arrabgables.Count;
        maxAmounts["weapons"] = weapons.Count;
        maxAmounts["documents"] = documents.Count;
        maxAmounts["clothes"] = clothes.Count;
        maxAmounts["miscellaneous"] = miscellaneous.Count;

        currentAmounts["blood"] = bloodCurrentAmmount;
        currentAmounts["corpses"] = corpsesCurrentAmmount;
        currentAmounts["uvCleanables"] = uvCleanablesCurrentAmmount;
        currentAmounts["bloodyObjects"] = bloodyObjectsCurrentAmmount;
        currentAmounts["arrabgables"] = arrabgablesCurrentAmmount;
        currentAmounts["weapons"] = weaponsCurrentAmmount;
        currentAmounts["documents"] = documentsCurrentAmmount;
        currentAmounts["clothes"] = clothesCurrentAmmount;
        currentAmounts["miscellaneous"] = miscellaneousCurrentAmmount;
    }
}
