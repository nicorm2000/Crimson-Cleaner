using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TabletUIManager : MonoBehaviour
{
    #region BAR VARIABLES
    [Header("Bar")]
    [SerializeField] private Button barButton = null;
    [SerializeField] private GameObject barPanel = null;
    [Header("Bar Blood")]
    [SerializeField] private Image barBloodTickImage = null;
    [SerializeField] private Image barBloodCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI barBloodProgressText = null;
    [Header("Bar Corpses")]
    [SerializeField] private Image barCorpsesTickImage = null;
    [SerializeField] private Image barCorpsesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI barCorpsesProgressText = null;
    [Header("Bar UV Cleanables")]
    [SerializeField] private Image barUVCleanablesTickImage = null;
    [SerializeField] private Image barUVCleanablesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI barUVCleanablesProgressText = null;
    [Header("Bar Bloody Objects")]
    [SerializeField] private Image barBloodyObjectsTickImage = null;
    [SerializeField] private Image barBloodyObjectsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI barBloodyObjectsProgressText = null;   
    [Header("Bar Arrangables")]
    [SerializeField] private Image barArrangablesTickImage = null;
    [SerializeField] private Image barArrangablesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI barArrangablesProgressText = null;
    [Header("Bar Weapons")]
    [SerializeField] private Image barWeaponsTickImage = null;
    [SerializeField] private Image barWeaponsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI barWeaponsProgressText = null;
    [Header("Bar Documents")]
    [SerializeField] private Image barDocumentsTickImage = null;
    [SerializeField] private Image barDocumentsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI barDocumentsProgressText = null;
    [Header("Bar Clothes")]
    [SerializeField] private Image barClothesTickImage = null;
    [SerializeField] private Image barClothesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI barClothesProgressText = null;
    [Header("Bar Miscellaneous")]
    [SerializeField] private Image barMiscellaneousTickImage = null;
    [SerializeField] private Image barMiscellaneousCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI barMiscellaneousProgressText = null;
    #endregion
    #region MEN'S BATHROOM VARIABLES
    [Header("Men's Bathroom")]
    [SerializeField] private Button menBathroomButton = null;
    [SerializeField] private GameObject menBathroomPanel = null;
    [Header("Men's Bathroom Blood")]
    [SerializeField] private Image menBathroomBloodTickImage = null;
    [SerializeField] private Image menBathroomBloodCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI menBathroomBloodProgressText = null;
    [Header("Men's Bathroom Corpses")]
    [SerializeField] private Image menBathroomCorpsesTickImage = null;
    [SerializeField] private Image menBathroomCorpsesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI menBathroomCorpsesProgressText = null;
    [Header("Men's Bathroom UV Cleanables")]
    [SerializeField] private Image menBathroomUVCleanablesTickImage = null;
    [SerializeField] private Image menBathroomUVCleanablesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI menBathroomUVCleanablesProgressText = null;
    [Header("Men's Bathroom Bloody Objects")]
    [SerializeField] private Image menBathroomBloodyObjectsTickImage = null;
    [SerializeField] private Image menBathroomBloodyObjectsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI menBathroomBloodyObjectsProgressText = null;
    [Header("Men's Bathroom Arrangables")]
    [SerializeField] private Image menBathroomArrangablesTickImage = null;
    [SerializeField] private Image menBathroomArrangablesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI menBathroomArrangablesProgressText = null;
    [Header("Men's Bathroom Weapons")]
    [SerializeField] private Image menBathroomWeaponsTickImage = null;
    [SerializeField] private Image menBathroomWeaponsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI menBathroomWeaponsProgressText = null;
    [Header("Men's Bathroom Documents")]
    [SerializeField] private Image menBathroomDocumentsTickImage = null;
    [SerializeField] private Image menBathroomDocumentsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI menBathroomDocumentsProgressText = null;
    [Header("Men's Bathroom Clothes")]
    [SerializeField] private Image menBathroomClothesTickImage = null;
    [SerializeField] private Image menBathroomClothesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI menBathroomClothesProgressText = null;
    [Header("Men's Bathroom Miscellaneous")]
    [SerializeField] private Image menBathroomMiscellaneousTickImage = null;
    [SerializeField] private Image menBathroomMiscellaneousCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI menBathroomMiscellaneousProgressText = null;
    #endregion
    #region WOMEN'S BATHROOM VARIABLES
    [Header("Women's Bathroom")]
    [SerializeField] private Button womenBathroomButton = null;
    [SerializeField] private GameObject womenBathroomPanel = null;
    [Header("Women's Bathroom Blood")]
    [SerializeField] private Image womenBathroomBloodTickImage = null;
    [SerializeField] private Image womenBathroomBloodCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI womenBathroomBloodProgressText = null;
    [Header("Women's Bathroom Corpses")]
    [SerializeField] private Image womenBathroomCorpsesTickImage = null;
    [SerializeField] private Image womenBathroomCorpsesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI womenBathroomCorpsesProgressText = null;
    [Header("Women's Bathroom UV Cleanables")]
    [SerializeField] private Image womenBathroomUVCleanablesTickImage = null;
    [SerializeField] private Image womenBathroomUVCleanablesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI womenBathroomUVCleanablesProgressText = null;
    [Header("Women's Bathroom Bloody Objects")]
    [SerializeField] private Image womenBathroomBloodyObjectsTickImage = null;
    [SerializeField] private Image womenBathroomBloodyObjectsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI womenBathroomBloodyObjectsProgressText = null;
    [Header("Women's Bathroom Arrangables")]
    [SerializeField] private Image womenBathroomArrangablesTickImage = null;
    [SerializeField] private Image womenBathroomArrangablesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI womenBathroomArrangablesProgressText = null;
    [Header("Women's Bathroom Weapons")]
    [SerializeField] private Image womenBathroomWeaponsTickImage = null;
    [SerializeField] private Image womenBathroomWeaponsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI womenBathroomWeaponsProgressText = null;
    [Header("Women's Bathroom Documents")]
    [SerializeField] private Image womenBathroomDocumentsTickImage = null;
    [SerializeField] private Image womenBathroomDocumentsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI womenBathroomDocumentsProgressText = null;
    [Header("Women's Bathroom Clothes")]
    [SerializeField] private Image womenBathroomClothesTickImage = null;
    [SerializeField] private Image womenBathroomClothesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI womenBathroomClothesProgressText = null;
    [Header("Women's Bathroom Miscellaneous")]
    [SerializeField] private Image womenBathroomMiscellaneousTickImage = null;
    [SerializeField] private Image womenBathroomMiscellaneousCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI womenBathroomMiscellaneousProgressText = null;
    #endregion
    #region KITCHEN VARIABLES
    [Header("Kitchen")]
    [SerializeField] private Button kitchenButton = null;
    [SerializeField] private GameObject kitchenPanel = null;
    [Header("Kitchen Blood")]
    [SerializeField] private Image kitchenBloodTickImage = null;
    [SerializeField] private Image kitchenBloodCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI kitchenBloodProgressText = null;
    [Header("Kitchen Corpses")]
    [SerializeField] private Image kitchenCorpsesTickImage = null;
    [SerializeField] private Image kitchenCorpsesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI kitchenCorpsesProgressText = null;
    [Header("Kitchen UV Cleanables")]
    [SerializeField] private Image kitchenUVCleanablesTickImage = null;
    [SerializeField] private Image kitchenUVCleanablesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI kitchenUVCleanablesProgressText = null;
    [Header("Kitchen Bloody Objects")]
    [SerializeField] private Image kitchenBloodyObjectsTickImage = null;
    [SerializeField] private Image kitchenBloodyObjectsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI kitchenBloodyObjectsProgressText = null;
    [Header("Kitchen Arrangables")]
    [SerializeField] private Image kitchenArrangablesTickImage = null;
    [SerializeField] private Image kitchenArrangablesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI kitchenArrangablesProgressText = null;
    [Header("Kitchen Weapons")]
    [SerializeField] private Image kitchenWeaponsTickImage = null;
    [SerializeField] private Image kitchenWeaponsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI kitchenWeaponsProgressText = null;
    [Header("Kitchen Documents")]
    [SerializeField] private Image kitchenDocumentsTickImage = null;
    [SerializeField] private Image kitchenDocumentsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI kitchenDocumentsProgressText = null;
    [Header("Kitchen Clothes")]
    [SerializeField] private Image kitchenClothesTickImage = null;
    [SerializeField] private Image kitchenClothesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI kitchenClothesProgressText = null;
    [Header("Kitchen Miscellaneous")]
    [SerializeField] private Image kitchenMiscellaneousTickImage = null;
    [SerializeField] private Image kitchenMiscellaneousCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI kitchenMiscellaneousProgressText = null;
    #endregion
    #region STORAGE ROOM VARIABLES
    [Header("Storage Room")]
    [SerializeField] private Button storageButton = null;
    [SerializeField] private GameObject storagePanel = null;
    [Header("Storage Room Blood")]
    [SerializeField] private Image storageBloodTickImage = null;
    [SerializeField] private Image storageBloodCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI storageBloodProgressText = null;
    [Header("Storage Room Corpses")]
    [SerializeField] private Image storageCorpsesTickImage = null;
    [SerializeField] private Image storageCorpsesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI storageCorpsesProgressText = null;
    [Header("Storage Room UV Cleanables")]
    [SerializeField] private Image storageUVCleanablesTickImage = null;
    [SerializeField] private Image storageUVCleanablesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI storageUVCleanablesProgressText = null;
    [Header("Storage Room Bloody Objects")]
    [SerializeField] private Image storageBloodyObjectsTickImage = null;
    [SerializeField] private Image storageBloodyObjectsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI storageBloodyObjectsProgressText = null;
    [Header("Storage Room Arrangables")]
    [SerializeField] private Image storageArrangablesTickImage = null;
    [SerializeField] private Image storageArrangablesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI storageArrangablesProgressText = null;
    [Header("Storage Room Weapons")]
    [SerializeField] private Image storageWeaponsTickImage = null;
    [SerializeField] private Image storageWeaponsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI storageWeaponsProgressText = null;
    [Header("Storage Room Documents")]
    [SerializeField] private Image storageDocumentsTickImage = null;
    [SerializeField] private Image storageDocumentsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI storageDocumentsProgressText = null;
    [Header("Storage Room Clothes")]
    [SerializeField] private Image storageClothesTickImage = null;
    [SerializeField] private Image storageClothesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI storageClothesProgressText = null;
    [Header("Storage Room Miscellaneous")]
    [SerializeField] private Image storageMiscellaneousTickImage = null;
    [SerializeField] private Image storageMiscellaneousCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI storageMiscellaneousProgressText = null;
    #endregion
    #region VIP ROOM VARIABLES
    [Header("VIP Room")]
    [SerializeField] private Button vipRoomButton = null;
    [SerializeField] private GameObject vipRoomPanel = null;
    [Header("VIP Room Blood")]
    [SerializeField] private Image vipRoomBloodTickImage = null;
    [SerializeField] private Image vipRoomBloodCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI vipRoomBloodProgressText = null;
    [Header("VIP Room Corpses")]
    [SerializeField] private Image vipRoomCorpsesTickImage = null;
    [SerializeField] private Image vipRoomCorpsesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI vipRoomCorpsesProgressText = null;
    [Header("VIP Room UV Cleanables")]
    [SerializeField] private Image vipRoomUVCleanablesTickImage = null;
    [SerializeField] private Image vipRoomUVCleanablesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI vipRoomUVCleanablesProgressText = null;
    [Header("VIP Room Bloody Objects")]
    [SerializeField] private Image vipRoomBloodyObjectsTickImage = null;
    [SerializeField] private Image vipRoomBloodyObjectsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI vipRoomBloodyObjectsProgressText = null;
    [Header("VIP Room Arrangables")]
    [SerializeField] private Image vipRoomArrangablesTickImage = null;
    [SerializeField] private Image vipRoomArrangablesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI vipRoomArrangablesProgressText = null;
    [Header("VIP Room Weapons")]
    [SerializeField] private Image vipRoomWeaponsTickImage = null;
    [SerializeField] private Image vipRoomWeaponsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI vipRoomWeaponsProgressText = null;
    [Header("VIP Room Documents")]
    [SerializeField] private Image vipRoomDocumentsTickImage = null;
    [SerializeField] private Image vipRoomDocumentsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI vipRoomDocumentsProgressText = null;
    [Header("VIP Room Clothes")]
    [SerializeField] private Image vipRoomClothesTickImage = null;
    [SerializeField] private Image vipRoomClothesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI vipRoomClothesProgressText = null;
    [Header("VIP Room Miscellaneous")]
    [SerializeField] private Image vipRoomMiscellaneousTickImage = null;
    [SerializeField] private Image vipRoomMiscellaneousCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI vipRoomMiscellaneousProgressText = null;
    #endregion
    #region BETTING ROOM VARIABLES
    [Header("Betting Room")]
    [SerializeField] private Button bettingRoomButton = null;
    [SerializeField] private GameObject bettingRoomPanel = null;
    [Header("Betting Room Blood")]
    [SerializeField] private Image bettingRoomBloodTickImage = null;
    [SerializeField] private Image bettingRoomBloodCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI bettingRoomBloodProgressText = null;
    [Header("Betting Room Corpses")]
    [SerializeField] private Image bettingRoomCorpsesTickImage = null;
    [SerializeField] private Image bettingRoomCorpsesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI bettingRoomCorpsesProgressText = null;
    [Header("Betting Room UV Cleanables")]
    [SerializeField] private Image bettingRoomUVCleanablesTickImage = null;
    [SerializeField] private Image bettingRoomUVCleanablesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI bettingRoomUVCleanablesProgressText = null;
    [Header("Betting Room Bloody Objects")]
    [SerializeField] private Image bettingRoomBloodyObjectsTickImage = null;
    [SerializeField] private Image bettingRoomBloodyObjectsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI bettingRoomBloodyObjectsProgressText = null;
    [Header("Betting Room Arrangables")]
    [SerializeField] private Image bettingRoomArrangablesTickImage = null;
    [SerializeField] private Image bettingRoomArrangablesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI bettingRoomArrangablesProgressText = null;
    [Header("Betting Room Weapons")]
    [SerializeField] private Image bettingRoomWeaponsTickImage = null;
    [SerializeField] private Image bettingRoomWeaponsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI bettingRoomWeaponsProgressText = null;
    [Header("Betting Room Documents")]
    [SerializeField] private Image bettingRoomDocumentsTickImage = null;
    [SerializeField] private Image bettingRoomDocumentsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI bettingRoomDocumentsProgressText = null;
    [Header("Betting Room Clothes")]
    [SerializeField] private Image bettingRoomClothesTickImage = null;
    [SerializeField] private Image bettingRoomClothesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI bettingRoomClothesProgressText = null;
    [Header("Betting Room Miscellaneous")]
    [SerializeField] private Image bettingRoomMiscellaneousTickImage = null;
    [SerializeField] private Image bettingRoomMiscellaneousCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI bettingRoomMiscellaneousProgressText = null;
#endregion

    private void Awake()
    {
        #region BUTTONS
        barButton.onClick.AddListener(() => { });
        menBathroomButton.onClick.AddListener(() => { });
        womenBathroomButton.onClick.AddListener(() => { });
        kitchenButton.onClick.AddListener(() => { });
        storageButton.onClick.AddListener(() => { });
        vipRoomButton.onClick.AddListener(() => { });
        bettingRoomButton.onClick.AddListener(() => { });
        #endregion
        #region BAR TICK IMAGE
        SetImageState(barBloodTickImage);
        SetImageState(barArrangablesTickImage);
        SetImageState(barBloodyObjectsTickImage);
        SetImageState(barClothesTickImage);
        SetImageState(barCorpsesTickImage);
        SetImageState(barDocumentsTickImage);
        SetImageState(barMiscellaneousTickImage);
        SetImageState(barUVCleanablesTickImage);
        SetImageState(barWeaponsTickImage);
        #endregion
        #region MEN'S BATHROOM TICK IMAGE
        SetImageState(menBathroomMiscellaneousTickImage);
        SetImageState(menBathroomArrangablesTickImage);
        SetImageState(menBathroomBloodTickImage);
        SetImageState(menBathroomBloodyObjectsTickImage);
        SetImageState(menBathroomClothesTickImage);
        SetImageState(menBathroomCorpsesTickImage);
        SetImageState(menBathroomDocumentsTickImage);
        SetImageState(menBathroomUVCleanablesTickImage);
        SetImageState(menBathroomWeaponsTickImage);
        #endregion
        #region WOMEN'S BATHROOM TICK IMAGE
        SetImageState(womenBathroomArrangablesTickImage);
        SetImageState(womenBathroomBloodTickImage);
        SetImageState(womenBathroomBloodyObjectsTickImage);
        SetImageState(womenBathroomClothesTickImage);
        SetImageState(womenBathroomDocumentsTickImage);
        SetImageState(womenBathroomMiscellaneousTickImage);
        SetImageState(womenBathroomWeaponsTickImage);
        SetImageState(womenBathroomUVCleanablesTickImage);
        SetImageState(womenBathroomCorpsesTickImage);
        #endregion
        #region KITCHEN TICK IMAGE
        SetImageState(kitchenArrangablesTickImage);
        SetImageState(kitchenBloodTickImage);
        SetImageState(kitchenBloodyObjectsTickImage);
        SetImageState(kitchenClothesTickImage);
        SetImageState(kitchenCorpsesTickImage);
        SetImageState(kitchenDocumentsTickImage);
        SetImageState(kitchenMiscellaneousTickImage);
        SetImageState(kitchenWeaponsTickImage);
        SetImageState(kitchenUVCleanablesTickImage);
        #endregion
        #region STORAGE ROOM TICK IMAGE
        SetImageState(storageArrangablesTickImage);
        SetImageState(storageBloodTickImage);
        SetImageState(storageBloodyObjectsTickImage);
        SetImageState(storageClothesTickImage);
        SetImageState(storageCorpsesTickImage);
        SetImageState(storageDocumentsTickImage);
        SetImageState(storageMiscellaneousTickImage);
        SetImageState(storageUVCleanablesTickImage);
        SetImageState(storageWeaponsTickImage);
        #endregion
        #region VIP ROOM TICK IMAGE
        SetImageState(vipRoomArrangablesTickImage);
        SetImageState(vipRoomBloodTickImage);
        SetImageState(vipRoomBloodyObjectsTickImage);
        SetImageState(vipRoomClothesTickImage);
        SetImageState(vipRoomCorpsesTickImage);
        SetImageState(vipRoomDocumentsTickImage);
        SetImageState(vipRoomMiscellaneousTickImage);
        SetImageState(vipRoomUVCleanablesTickImage);
        SetImageState(vipRoomWeaponsTickImage);
        #endregion
        #region BETTING ROOM TICK IMAGE
        SetImageState(bettingRoomClothesTickImage);
        SetImageState(bettingRoomMiscellaneousTickImage);
        SetImageState(bettingRoomDocumentsTickImage);
        SetImageState(bettingRoomArrangablesTickImage);
        SetImageState(bettingRoomBloodTickImage);
        SetImageState(bettingRoomBloodyObjectsTickImage);
        SetImageState(bettingRoomCorpsesTickImage);
        SetImageState(bettingRoomUVCleanablesTickImage);
        SetImageState(bettingRoomWeaponsTickImage);
        #endregion
        #region BAR CROSSED OUT IMAGE
        SetImageFillValue(barArrangablesCrossedOutImage);
        SetImageFillValue(barBloodCrossedOutImage);
        SetImageFillValue(barBloodyObjectsCrossedOutImage);
        SetImageFillValue(barClothesCrossedOutImage);
        SetImageFillValue(barCorpsesCrossedOutImage);
        SetImageFillValue(barDocumentsCrossedOutImage);
        SetImageFillValue(barMiscellaneousCrossedOutImage);
        SetImageFillValue(barUVCleanablesCrossedOutImage);
        SetImageFillValue(barWeaponsCrossedOutImage);
        #endregion
        #region MEN'S BATHROOM CROSSED OUT IMAGE
        SetImageFillValue(menBathroomArrangablesCrossedOutImage);
        SetImageFillValue(menBathroomBloodCrossedOutImage);
        SetImageFillValue(menBathroomBloodyObjectsCrossedOutImage);
        SetImageFillValue(menBathroomClothesCrossedOutImage);
        SetImageFillValue(menBathroomCorpsesCrossedOutImage);
        SetImageFillValue(menBathroomDocumentsCrossedOutImage);
        SetImageFillValue(menBathroomMiscellaneousCrossedOutImage);
        SetImageFillValue(menBathroomUVCleanablesCrossedOutImage);
        SetImageFillValue(menBathroomWeaponsCrossedOutImage);
        #endregion
        #region WOMEN'S BATHROOM CROSSED OUT IMAGE
        SetImageFillValue(womenBathroomArrangablesCrossedOutImage);
        SetImageFillValue(womenBathroomBloodCrossedOutImage);
        SetImageFillValue(womenBathroomBloodyObjectsCrossedOutImage);
        SetImageFillValue(womenBathroomClothesCrossedOutImage);
        SetImageFillValue(womenBathroomCorpsesCrossedOutImage);
        SetImageFillValue(womenBathroomDocumentsCrossedOutImage);
        SetImageFillValue(womenBathroomMiscellaneousCrossedOutImage);
        SetImageFillValue(womenBathroomUVCleanablesCrossedOutImage);
        SetImageFillValue(womenBathroomWeaponsCrossedOutImage);
        #endregion
        #region KITCHEN CROSSED OUT IMAGE
        SetImageFillValue(kitchenArrangablesCrossedOutImage);
        SetImageFillValue(kitchenBloodCrossedOutImage);
        SetImageFillValue(kitchenBloodyObjectsCrossedOutImage);
        SetImageFillValue(kitchenClothesCrossedOutImage);
        SetImageFillValue(kitchenCorpsesCrossedOutImage);
        SetImageFillValue(kitchenDocumentsCrossedOutImage);
        SetImageFillValue(kitchenMiscellaneousCrossedOutImage);
        SetImageFillValue(kitchenUVCleanablesCrossedOutImage);
        SetImageFillValue(kitchenWeaponsCrossedOutImage);
        #endregion
        #region STORAGE ROOM CROSSED OUT IMAGE
        SetImageFillValue(storageArrangablesCrossedOutImage);
        SetImageFillValue(storageBloodCrossedOutImage);
        SetImageFillValue(storageBloodyObjectsCrossedOutImage);
        SetImageFillValue(storageClothesCrossedOutImage);
        SetImageFillValue(storageCorpsesCrossedOutImage);
        SetImageFillValue(storageDocumentsCrossedOutImage);
        SetImageFillValue(storageMiscellaneousCrossedOutImage);
        SetImageFillValue(storageUVCleanablesCrossedOutImage);
        SetImageFillValue(storageWeaponsCrossedOutImage);
        #endregion
        #region VIP ROOM CROSSED OUT IMAGE
        SetImageFillValue(vipRoomArrangablesCrossedOutImage);
        SetImageFillValue(vipRoomBloodCrossedOutImage);
        SetImageFillValue(vipRoomBloodyObjectsCrossedOutImage);
        SetImageFillValue(vipRoomClothesCrossedOutImage);
        SetImageFillValue(vipRoomCorpsesCrossedOutImage);
        SetImageFillValue(vipRoomDocumentsCrossedOutImage);
        SetImageFillValue(vipRoomMiscellaneousCrossedOutImage);
        SetImageFillValue(vipRoomUVCleanablesCrossedOutImage);
        SetImageFillValue(vipRoomWeaponsCrossedOutImage);
        #endregion
        #region BETTING ROOM CROSSED OUT IMAGE
        SetImageFillValue(bettingRoomArrangablesCrossedOutImage);
        SetImageFillValue(bettingRoomBloodCrossedOutImage);
        SetImageFillValue(bettingRoomBloodyObjectsCrossedOutImage);
        SetImageFillValue(bettingRoomClothesCrossedOutImage);
        SetImageFillValue(bettingRoomCorpsesCrossedOutImage);
        SetImageFillValue(bettingRoomDocumentsCrossedOutImage);
        SetImageFillValue(bettingRoomMiscellaneousCrossedOutImage);
        SetImageFillValue(bettingRoomUVCleanablesCrossedOutImage);
        SetImageFillValue(bettingRoomWeaponsCrossedOutImage);
        #endregion
    }

    private void SetImageState(Image image, bool state = false)
    {
        image.enabled = state;
    }

    private void SetImageFillValue(Image image, float amount = 0)
    {
        image.fillAmount = amount;
    }

    private IEnumerator FillImageOverTime(Image image, float targetFillAmount = 1f, float duration = 1f)
    {
        float startFill = image.fillAmount;
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            image.fillAmount = Mathf.Lerp(startFill, targetFillAmount, timeElapsed / duration);
            yield return null;
        }

        image.fillAmount = targetFillAmount;
    }
}