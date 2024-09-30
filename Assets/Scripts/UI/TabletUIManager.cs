using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TabletUIManager : MonoBehaviour
{
    #region SCRIPTS
    [Header("Config")]
    [SerializeField] private GameStateManager gameStateManager = null;
    #endregion
    #region AUDIO
    [Header("Audio")]
    [SerializeField] private AudioManager audioManager = null;
    [SerializeField] private string clickEvent = null;
    [SerializeField] private string crossOutEvent = null;
    #endregion
    #region BAR VARIABLES
    [Header("Bar")]
    [SerializeField] private Button barButton = null;
    [SerializeField] private GameObject barPanel = null;
    [Header("Bar Blood")]
    [SerializeField] private Image barBloodTickImage = null;
    [SerializeField] private Image barBloodCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI barBloodProgressText = null;
    [SerializeField] private int barBloodMaxAmount = 0;
    private int barBloodCurrentAmount = 0;
    [Header("Bar Corpses")]
    [SerializeField] private Image barCorpsesTickImage = null;
    [SerializeField] private Image barCorpsesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI barCorpsesProgressText = null;
    [SerializeField] private int barCorpsesMaxAmount = 0;
    private int barCorpsesCurrentAmount = 0;
    [Header("Bar UV Cleanables")]
    [SerializeField] private Image barUVCleanablesTickImage = null;
    [SerializeField] private Image barUVCleanablesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI barUVCleanablesProgressText = null;
    [SerializeField] private int barUVCleanablesMaxAmount = 0;
    private int barUVCleanablesCurrentAmount = 0;
    [Header("Bar Bloody Objects")]
    [SerializeField] private Image barBloodyObjectsTickImage = null;
    [SerializeField] private Image barBloodyObjectsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI barBloodyObjectsProgressText = null;   
    [SerializeField] private int barBloodyObjectsMaxAmount = 0;
    private int barBloodyObjectsCurrentAmount = 0;
    [Header("Bar Arrangables")]
    [SerializeField] private Image barArrangablesTickImage = null;
    [SerializeField] private Image barArrangablesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI barArrangablesProgressText = null;
    [SerializeField] private int barArrangablesMaxAmount = 0;
    private int barArrangablesCurrentAmount = 0;
    [Header("Bar Weapons")]
    [SerializeField] private Image barWeaponsTickImage = null;
    [SerializeField] private Image barWeaponsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI barWeaponsProgressText = null;
    [SerializeField] private int barWeaponsMaxAmount = 0;
    private int barWeaponsCurrentAmount = 0;
    [Header("Bar Documents")]
    [SerializeField] private Image barDocumentsTickImage = null;
    [SerializeField] private Image barDocumentsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI barDocumentsProgressText = null;
    [SerializeField] private int barDocumentsMaxAmount = 0;
    private int barDocumentsCurrentAmount = 0;
    [Header("Bar Clothes")]
    [SerializeField] private Image barClothesTickImage = null;
    [SerializeField] private Image barClothesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI barClothesProgressText = null;
    [SerializeField] private int barClothesMaxAmount = 0;
    private int barClothesCurrentAmount = 0;
    [Header("Bar Miscellaneous")]
    [SerializeField] private Image barMiscellaneousTickImage = null;
    [SerializeField] private Image barMiscellaneousCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI barMiscellaneousProgressText = null;
    [SerializeField] private int barMiscellaneousMaxAmount = 0;
    private int barMiscellaneousCurrentAmount = 0;
    #endregion
    #region MEN'S BATHROOM VARIABLES
    [Header("Men's Bathroom")]
    [SerializeField] private Button menBathroomButton = null;
    [SerializeField] private GameObject menBathroomPanel = null;
    [Header("Men's Bathroom Blood")]
    [SerializeField] private Image menBathroomBloodTickImage = null;
    [SerializeField] private Image menBathroomBloodCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI menBathroomBloodProgressText = null;
    [SerializeField] private int menBathroomBloodMaxAmount = 0;
    private int menBathroomBloodCurrentAmount = 0;
    [Header("Men's Bathroom Corpses")]
    [SerializeField] private Image menBathroomCorpsesTickImage = null;
    [SerializeField] private Image menBathroomCorpsesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI menBathroomCorpsesProgressText = null;
    [SerializeField] private int menBathroomCorpsesMaxAmount = 0;
    private int menBathroomCorpsesCurrentAmount = 0;
    [Header("Men's Bathroom UV Cleanables")]
    [SerializeField] private Image menBathroomUVCleanablesTickImage = null;
    [SerializeField] private Image menBathroomUVCleanablesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI menBathroomUVCleanablesProgressText = null;
    [SerializeField] private int menBathroomUVCleanablesMaxAmount = 0;
    private int menBathroomUVCleanablesCurrentAmount = 0;
    [Header("Men's Bathroom Bloody Objects")]
    [SerializeField] private Image menBathroomBloodyObjectsTickImage = null;
    [SerializeField] private Image menBathroomBloodyObjectsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI menBathroomBloodyObjectsProgressText = null;
    [SerializeField] private int menBathroomBloodyObjectsMaxAmount = 0;
    private int menBathroomBloodyObjectsCurrentAmount = 0;
    [Header("Men's Bathroom Arrangables")]
    [SerializeField] private Image menBathroomArrangablesTickImage = null;
    [SerializeField] private Image menBathroomArrangablesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI menBathroomArrangablesProgressText = null;
    [SerializeField] private int menBathroomArrangablesMaxAmount = 0;
    private int menBathroomArrangablesCurrentAmount = 0;
    [Header("Men's Bathroom Weapons")]
    [SerializeField] private Image menBathroomWeaponsTickImage = null;
    [SerializeField] private Image menBathroomWeaponsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI menBathroomWeaponsProgressText = null;
    [SerializeField] private int menBathroomWeaponsMaxAmount = 0;
    private int menBathroomWeaponsCurrentAmount = 0;
    [Header("Men's Bathroom Documents")]
    [SerializeField] private Image menBathroomDocumentsTickImage = null;
    [SerializeField] private Image menBathroomDocumentsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI menBathroomDocumentsProgressText = null;
    [SerializeField] private int menBathroomDocumentsMaxAmount = 0;
    private int menBathroomDocumentsCurrentAmount = 0;
    [Header("Men's Bathroom Clothes")]
    [SerializeField] private Image menBathroomClothesTickImage = null;
    [SerializeField] private Image menBathroomClothesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI menBathroomClothesProgressText = null;
    [SerializeField] private int menBathroomClothesMaxAmount = 0;
    private int menBathroomClothesCurrentAmount = 0;
    [Header("Men's Bathroom Miscellaneous")]
    [SerializeField] private Image menBathroomMiscellaneousTickImage = null;
    [SerializeField] private Image menBathroomMiscellaneousCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI menBathroomMiscellaneousProgressText = null;
    [SerializeField] private int menBathroomMiscellaneousMaxAmount = 0;
    private int menBathroomMiscellaneousCurrentAmount = 0;
    #endregion
    #region WOMEN'S BATHROOM VARIABLES
    [Header("Women's Bathroom")]
    [SerializeField] private Button womenBathroomButton = null;
    [SerializeField] private GameObject womenBathroomPanel = null;
    [Header("Women's Bathroom Blood")]
    [SerializeField] private Image womenBathroomBloodTickImage = null;
    [SerializeField] private Image womenBathroomBloodCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI womenBathroomBloodProgressText = null;
    [SerializeField] private int womenBathroomBloodMaxAmount = 0;
    private int womenBathroomBloodCurrentAmount = 0;
    [Header("Women's Bathroom Corpses")]
    [SerializeField] private Image womenBathroomCorpsesTickImage = null;
    [SerializeField] private Image womenBathroomCorpsesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI womenBathroomCorpsesProgressText = null;
    [SerializeField] private int womenBathroomCorpsesMaxAmount = 0;
    private int womenBathroomCorpsesCurrentAmount = 0;
    [Header("Women's Bathroom UV Cleanables")]
    [SerializeField] private Image womenBathroomUVCleanablesTickImage = null;
    [SerializeField] private Image womenBathroomUVCleanablesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI womenBathroomUVCleanablesProgressText = null;
    [SerializeField] private int womenBathroomUVCleanablesMaxAmount = 0;
    private int womenBathroomUVCleanablesCurrentAmount = 0;
    [Header("Women's Bathroom Bloody Objects")]
    [SerializeField] private Image womenBathroomBloodyObjectsTickImage = null;
    [SerializeField] private Image womenBathroomBloodyObjectsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI womenBathroomBloodyObjectsProgressText = null;
    [SerializeField] private int womenBathroomBloodyObjectsMaxAmount = 0;
    private int womenBathroomBloodyObjectsCurrentAmount = 0;
    [Header("Women's Bathroom Arrangables")]
    [SerializeField] private Image womenBathroomArrangablesTickImage = null;
    [SerializeField] private Image womenBathroomArrangablesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI womenBathroomArrangablesProgressText = null;
    [SerializeField] private int womenBathroomArrangablesMaxAmount = 0;
    private int womenBathroomArrangablesCurrentAmount = 0;
    [Header("Women's Bathroom Weapons")]
    [SerializeField] private Image womenBathroomWeaponsTickImage = null;
    [SerializeField] private Image womenBathroomWeaponsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI womenBathroomWeaponsProgressText = null;
    [SerializeField] private int womenBathroomWeaponsMaxAmount = 0;
    private int womenBathroomWeaponsCurrentAmount = 0;
    [Header("Women's Bathroom Documents")]
    [SerializeField] private Image womenBathroomDocumentsTickImage = null;
    [SerializeField] private Image womenBathroomDocumentsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI womenBathroomDocumentsProgressText = null;
    [SerializeField] private int womenBathroomDocumentsMaxAmount = 0;
    private int womenBathroomDocumentsCurrentAmount = 0;
    [Header("Women's Bathroom Clothes")]
    [SerializeField] private Image womenBathroomClothesTickImage = null;
    [SerializeField] private Image womenBathroomClothesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI womenBathroomClothesProgressText = null;
    [SerializeField] private int womenBathroomClothesMaxAmount = 0;
    private int womenBathroomClothesCurrentAmount = 0;
    [Header("Women's Bathroom Miscellaneous")]
    [SerializeField] private Image womenBathroomMiscellaneousTickImage = null;
    [SerializeField] private Image womenBathroomMiscellaneousCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI womenBathroomMiscellaneousProgressText = null;
    [SerializeField] private int womenBathroomMiscellaneousMaxAmount = 0;
    private int womenBathroomMiscellaneousCurrentAmount = 0;
    #endregion
    #region KITCHEN VARIABLES
    [Header("Kitchen")]
    [SerializeField] private Button kitchenButton = null;
    [SerializeField] private GameObject kitchenPanel = null;
    [Header("Kitchen Blood")]
    [SerializeField] private Image kitchenBloodTickImage = null;
    [SerializeField] private Image kitchenBloodCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI kitchenBloodProgressText = null;
    [SerializeField] private int kitchenBloodMaxAmount = 0;
    private int kitchenBloodCurrentAmount = 0;
    [Header("Kitchen Corpses")]
    [SerializeField] private Image kitchenCorpsesTickImage = null;
    [SerializeField] private Image kitchenCorpsesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI kitchenCorpsesProgressText = null;
    [SerializeField] private int kitchenCorpsesMaxAmount = 0;
    private int kitchenCorpsesCurrentAmount = 0;
    [Header("Kitchen UV Cleanables")]
    [SerializeField] private Image kitchenUVCleanablesTickImage = null;
    [SerializeField] private Image kitchenUVCleanablesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI kitchenUVCleanablesProgressText = null;
    [SerializeField] private int kitchenUVCleanablesMaxAmount = 0;
    private int kitchenUVCleanablesCurrentAmount = 0;
    [Header("Kitchen Bloody Objects")]
    [SerializeField] private Image kitchenBloodyObjectsTickImage = null;
    [SerializeField] private Image kitchenBloodyObjectsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI kitchenBloodyObjectsProgressText = null;
    [SerializeField] private int kitchenBloodyObjectsMaxAmount = 0;
    private int kitchenBloodyObjectsCurrentAmount = 0;
    [Header("Kitchen Arrangables")]
    [SerializeField] private Image kitchenArrangablesTickImage = null;
    [SerializeField] private Image kitchenArrangablesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI kitchenArrangablesProgressText = null;
    [SerializeField] private int kitchenArrangablesMaxAmount = 0;
    private int kitchenArrangablesCurrentAmount = 0;
    [Header("Kitchen Weapons")]
    [SerializeField] private Image kitchenWeaponsTickImage = null;
    [SerializeField] private Image kitchenWeaponsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI kitchenWeaponsProgressText = null;
    [SerializeField] private int kitchenWeaponsMaxAmount = 0;
    private int kitchenWeaponsCurrentAmount = 0;
    [Header("Kitchen Documents")]
    [SerializeField] private Image kitchenDocumentsTickImage = null;
    [SerializeField] private Image kitchenDocumentsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI kitchenDocumentsProgressText = null;
    [SerializeField] private int kitchenDocumentsMaxAmount = 0;
    private int kitchenDocumentsCurrentAmount = 0;
    [Header("Kitchen Clothes")]
    [SerializeField] private Image kitchenClothesTickImage = null;
    [SerializeField] private Image kitchenClothesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI kitchenClothesProgressText = null;
    [SerializeField] private int kitchenClothesMaxAmount = 0;
    private int kitchenClothesCurrentAmount = 0;
    [Header("Kitchen Miscellaneous")]
    [SerializeField] private Image kitchenMiscellaneousTickImage = null;
    [SerializeField] private Image kitchenMiscellaneousCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI kitchenMiscellaneousProgressText = null;
    [SerializeField] private int kitchenMiscellaneousMaxAmount = 0;
    private int kitchenMiscellaneousCurrentAmount = 0;
    #endregion
    #region STORAGE ROOM VARIABLES
    [Header("Storage Room")]
    [SerializeField] private Button storageButton = null;
    [SerializeField] private GameObject storagePanel = null;
    [Header("Storage Room Blood")]
    [SerializeField] private Image storageBloodTickImage = null;
    [SerializeField] private Image storageBloodCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI storageBloodProgressText = null;
    [SerializeField] private int storageBloodMaxAmount = 0;
    private int storageBloodCurrentAmount = 0;
    [Header("Storage Room Corpses")]
    [SerializeField] private Image storageCorpsesTickImage = null;
    [SerializeField] private Image storageCorpsesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI storageCorpsesProgressText = null;
    [SerializeField] private int storageCorpsesMaxAmount = 0;
    private int storageCorpsesCurrentAmount = 0;
    [Header("Storage Room UV Cleanables")]
    [SerializeField] private Image storageUVCleanablesTickImage = null;
    [SerializeField] private Image storageUVCleanablesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI storageUVCleanablesProgressText = null;
    [SerializeField] private int storageUVCleanablesMaxAmount = 0;
    private int storageUVCleanablesCurrentAmount = 0;
    [Header("Storage Room Bloody Objects")]
    [SerializeField] private Image storageBloodyObjectsTickImage = null;
    [SerializeField] private Image storageBloodyObjectsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI storageBloodyObjectsProgressText = null;
    [SerializeField] private int storageBloodyObjectsMaxAmount = 0;
    private int storageBloodyObjectsCurrentAmount = 0;
    [Header("Storage Room Arrangables")]
    [SerializeField] private Image storageArrangablesTickImage = null;
    [SerializeField] private Image storageArrangablesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI storageArrangablesProgressText = null;
    [SerializeField] private int storageArrangablesMaxAmount = 0;
    private int storageArrangablesCurrentAmount = 0;
    [Header("Storage Room Weapons")]
    [SerializeField] private Image storageWeaponsTickImage = null;
    [SerializeField] private Image storageWeaponsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI storageWeaponsProgressText = null;
    [SerializeField] private int storageWeaponsMaxAmount = 0;
    private int storageWeaponsCurrentAmount = 0;
    [Header("Storage Room Documents")]
    [SerializeField] private Image storageDocumentsTickImage = null;
    [SerializeField] private Image storageDocumentsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI storageDocumentsProgressText = null;
    [SerializeField] private int storageDocumentsMaxAmount = 0;
    private int storageDocumentsCurrentAmount = 0;
    [Header("Storage Room Clothes")]
    [SerializeField] private Image storageClothesTickImage = null;
    [SerializeField] private Image storageClothesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI storageClothesProgressText = null;
    [SerializeField] private int storageClothesMaxAmount = 0;
    private int storageClothesCurrentAmount = 0;
    [Header("Storage Room Miscellaneous")]
    [SerializeField] private Image storageMiscellaneousTickImage = null;
    [SerializeField] private Image storageMiscellaneousCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI storageMiscellaneousProgressText = null;
    [SerializeField] private int storageMiscellaneousMaxAmount = 0;
    private int storageMiscellaneousCurrentAmount = 0;
    #endregion
    #region VIP ROOM VARIABLES
    [Header("VIP Room")]
    [SerializeField] private Button vipRoomButton = null;
    [SerializeField] private GameObject vipRoomPanel = null;
    [Header("VIP Room Blood")]
    [SerializeField] private Image vipRoomBloodTickImage = null;
    [SerializeField] private Image vipRoomBloodCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI vipRoomBloodProgressText = null;
    [SerializeField] private int vipRoomBloodMaxAmount = 0;
    private int vipRoomBloodCurrentAmount = 0;
    [Header("VIP Room Corpses")]
    [SerializeField] private Image vipRoomCorpsesTickImage = null;
    [SerializeField] private Image vipRoomCorpsesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI vipRoomCorpsesProgressText = null;
    [SerializeField] private int vipRoomCorpsesMaxAmount = 0;
    private int vipRoomCorpsesCurrentAmount = 0;
    [Header("VIP Room UV Cleanables")]
    [SerializeField] private Image vipRoomUVCleanablesTickImage = null;
    [SerializeField] private Image vipRoomUVCleanablesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI vipRoomUVCleanablesProgressText = null;
    [SerializeField] private int vipRoomUVCleanablesMaxAmount = 0;
    private int vipRoomUVCleanablesCurrentAmount = 0;
    [Header("VIP Room Bloody Objects")]
    [SerializeField] private Image vipRoomBloodyObjectsTickImage = null;
    [SerializeField] private Image vipRoomBloodyObjectsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI vipRoomBloodyObjectsProgressText = null;
    [SerializeField] private int vipRoomBloodyObjectsMaxAmount = 0;
    private int vipRoomBloodyObjectsCurrentAmount = 0;
    [Header("VIP Room Arrangables")]
    [SerializeField] private Image vipRoomArrangablesTickImage = null;
    [SerializeField] private Image vipRoomArrangablesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI vipRoomArrangablesProgressText = null;
    [SerializeField] private int vipRoomArrangablesMaxAmount = 0;
    private int vipRoomArrangablesCurrentAmount = 0;
    [Header("VIP Room Weapons")]
    [SerializeField] private Image vipRoomWeaponsTickImage = null;
    [SerializeField] private Image vipRoomWeaponsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI vipRoomWeaponsProgressText = null;
    [SerializeField] private int vipRoomWeaponsMaxAmount = 0;
    private int vipRoomWeaponsCurrentAmount = 0;
    [Header("VIP Room Documents")]
    [SerializeField] private Image vipRoomDocumentsTickImage = null;
    [SerializeField] private Image vipRoomDocumentsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI vipRoomDocumentsProgressText = null;
    [SerializeField] private int vipRoomDocumentsMaxAmount = 0;
    private int vipRoomDocumentsCurrentAmount = 0;
    [Header("VIP Room Clothes")]
    [SerializeField] private Image vipRoomClothesTickImage = null;
    [SerializeField] private Image vipRoomClothesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI vipRoomClothesProgressText = null;
    [SerializeField] private int vipRoomClothesMaxAmount = 0;
    private int vipRoomClothesCurrentAmount = 0;
    [Header("VIP Room Miscellaneous")]
    [SerializeField] private Image vipRoomMiscellaneousTickImage = null;
    [SerializeField] private Image vipRoomMiscellaneousCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI vipRoomMiscellaneousProgressText = null;
    [SerializeField] private int vipRoomMiscellaneousMaxAmount = 0;
    private int vipRoomMiscellaneousCurrentAmount = 0;
    #endregion
    #region BETTING ROOM VARIABLES
    [Header("Betting Room")]
    [SerializeField] private Button bettingRoomButton = null;
    [SerializeField] private GameObject bettingRoomPanel = null;
    [Header("Betting Room Blood")]
    [SerializeField] private Image bettingRoomBloodTickImage = null;
    [SerializeField] private Image bettingRoomBloodCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI bettingRoomBloodProgressText = null;
    [SerializeField] private int bettingRoomBloodMaxAmount = 0;
    private int bettingRoomBloodCurrentAmount = 0;
    [Header("Betting Room Corpses")]
    [SerializeField] private Image bettingRoomCorpsesTickImage = null;
    [SerializeField] private Image bettingRoomCorpsesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI bettingRoomCorpsesProgressText = null;
    [SerializeField] private int bettingRoomCorpsesMaxAmount = 0;
    private int bettingRoomCorpsesCurrentAmount = 0;
    [Header("Betting Room UV Cleanables")]
    [SerializeField] private Image bettingRoomUVCleanablesTickImage = null;
    [SerializeField] private Image bettingRoomUVCleanablesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI bettingRoomUVCleanablesProgressText = null;
    [SerializeField] private int bettingRoomUVCleanablesMaxAmount = 0;
    private int bettingRoomUVCleanablesCurrentAmount = 0;
    [Header("Betting Room Bloody Objects")]
    [SerializeField] private Image bettingRoomBloodyObjectsTickImage = null;
    [SerializeField] private Image bettingRoomBloodyObjectsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI bettingRoomBloodyObjectsProgressText = null;
    [SerializeField] private int bettingRoomBloodyObjectsMaxAmount = 0;
    private int bettingRoomBloodyObjectsCurrentAmount = 0;
    [Header("Betting Room Arrangables")]
    [SerializeField] private Image bettingRoomArrangablesTickImage = null;
    [SerializeField] private Image bettingRoomArrangablesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI bettingRoomArrangablesProgressText = null;
    [SerializeField] private int bettingRoomArrangablesMaxAmount = 0;
    private int bettingRoomArrangablesCurrentAmount = 0;
    [Header("Betting Room Weapons")]
    [SerializeField] private Image bettingRoomWeaponsTickImage = null;
    [SerializeField] private Image bettingRoomWeaponsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI bettingRoomWeaponsProgressText = null;
    [SerializeField] private int bettingRoomWeaponsMaxAmount = 0;
    private int bettingRoomWeaponsCurrentAmount = 0;
    [Header("Betting Room Documents")]
    [SerializeField] private Image bettingRoomDocumentsTickImage = null;
    [SerializeField] private Image bettingRoomDocumentsCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI bettingRoomDocumentsProgressText = null;
    [SerializeField] private int bettingRoomDocumentsMaxAmount = 0;
    private int bettingRoomDocumentsCurrentAmount = 0;
    [Header("Betting Room Clothes")]
    [SerializeField] private Image bettingRoomClothesTickImage = null;
    [SerializeField] private Image bettingRoomClothesCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI bettingRoomClothesProgressText = null;
    [SerializeField] private int bettingRoomClothesMaxAmount = 0;
    private int bettingRoomClothesCurrentAmount = 0;
    [Header("Betting Room Miscellaneous")]
    [SerializeField] private Image bettingRoomMiscellaneousTickImage = null;
    [SerializeField] private Image bettingRoomMiscellaneousCrossedOutImage = null;
    [SerializeField] private TextMeshProUGUI bettingRoomMiscellaneousProgressText = null;
    [SerializeField] private int bettingRoomMiscellaneousMaxAmount = 0;
    private int bettingRoomMiscellaneousCurrentAmount = 0;
    #endregion

    private List<GameObject> tabs = null;

    private void Awake()
    {
        tabs = new List<GameObject>()
        {
            barPanel,
            menBathroomPanel,
            womenBathroomPanel,
            kitchenPanel,
            storagePanel,
            vipRoomPanel,
            bettingRoomPanel
        };

        #region BAR CURRENT AMOUNT INIT
        barArrangablesCurrentAmount = 0;
        barBloodCurrentAmount = 0;
        barBloodyObjectsCurrentAmount = 0;
        barClothesCurrentAmount = 0;
        barCorpsesCurrentAmount = 0;
        barDocumentsCurrentAmount = 0;
        barMiscellaneousCurrentAmount = 0;
        barUVCleanablesCurrentAmount = 0;
        barWeaponsCurrentAmount = 0;
        #endregion
        #region MEN'S BATHROOM CURRENT AMOUNT INIT
        menBathroomArrangablesCurrentAmount = 0;
        menBathroomBloodCurrentAmount = 0;
        menBathroomBloodyObjectsCurrentAmount = 0;
        menBathroomClothesCurrentAmount = 0;
        menBathroomCorpsesCurrentAmount = 0;
        menBathroomDocumentsCurrentAmount = 0;
        menBathroomMiscellaneousCurrentAmount = 0;
        menBathroomUVCleanablesCurrentAmount = 0;
        menBathroomWeaponsCurrentAmount = 0;
        #endregion
        #region WOMEN'S BATHROOM CURRENT AMOUNT INIT
        womenBathroomArrangablesCurrentAmount = 0;
        womenBathroomBloodCurrentAmount = 0;
        womenBathroomBloodyObjectsCurrentAmount = 0;
        womenBathroomClothesCurrentAmount = 0;
        womenBathroomCorpsesCurrentAmount = 0;
        womenBathroomDocumentsCurrentAmount = 0;
        womenBathroomMiscellaneousCurrentAmount = 0;
        womenBathroomUVCleanablesCurrentAmount = 0;
        womenBathroomWeaponsCurrentAmount = 0;
        #endregion
        #region KITCHEN CURRENT AMOUNT INIT
        kitchenArrangablesCurrentAmount = 0;
        kitchenBloodCurrentAmount = 0;
        kitchenBloodyObjectsCurrentAmount = 0;
        kitchenClothesCurrentAmount = 0;
        kitchenCorpsesCurrentAmount = 0;
        kitchenDocumentsCurrentAmount = 0;
        kitchenMiscellaneousCurrentAmount = 0;
        kitchenUVCleanablesCurrentAmount = 0;
        kitchenWeaponsCurrentAmount = 0;
        #endregion
        #region STORAGE ROOM CURRENT AMOUNT INIT
        storageArrangablesCurrentAmount = 0;
        storageBloodCurrentAmount = 0;
        storageBloodyObjectsCurrentAmount = 0;
        storageClothesCurrentAmount = 0;
        storageCorpsesCurrentAmount = 0;
        storageDocumentsCurrentAmount = 0;
        storageMiscellaneousCurrentAmount = 0;
        storageUVCleanablesCurrentAmount = 0;
        storageWeaponsCurrentAmount = 0;
        #endregion
        #region VIP ROOM CROSSED OUT IMAGE
        vipRoomArrangablesCurrentAmount = 0;
        vipRoomBloodCurrentAmount = 0;
        vipRoomBloodyObjectsCurrentAmount = 0;
        vipRoomClothesCurrentAmount = 0;
        vipRoomCorpsesCurrentAmount = 0;
        vipRoomDocumentsCurrentAmount = 0;
        vipRoomMiscellaneousCurrentAmount = 0;
        vipRoomUVCleanablesCurrentAmount = 0;
        vipRoomWeaponsCurrentAmount = 0;
        #endregion
        #region BETTING ROOM CURRENT AMOUNT INIT
        bettingRoomArrangablesCurrentAmount = 0;
        bettingRoomBloodCurrentAmount = 0;
        bettingRoomBloodyObjectsCurrentAmount = 0;
        bettingRoomClothesCurrentAmount = 0;
        bettingRoomCorpsesCurrentAmount = 0;
        bettingRoomDocumentsCurrentAmount = 0;
        bettingRoomMiscellaneousCurrentAmount = 0;
        bettingRoomUVCleanablesCurrentAmount = 0;
        bettingRoomWeaponsCurrentAmount = 0;
        #endregion
        #region BUTTONS
        barButton.onClick.AddListener(() => { OpenTab(barPanel); });
        menBathroomButton.onClick.AddListener(() => { OpenTab(menBathroomPanel); });
        womenBathroomButton.onClick.AddListener(() => { OpenTab(womenBathroomPanel); });
        kitchenButton.onClick.AddListener(() => { OpenTab(kitchenPanel); });
        storageButton.onClick.AddListener(() => { OpenTab(storagePanel); });
        vipRoomButton.onClick.AddListener(() => { OpenTab(vipRoomPanel); });
        bettingRoomButton.onClick.AddListener(() => { OpenTab(bettingRoomPanel); });
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
        #region BAR TICK IMAGE
        UpdateText(barArrangablesProgressText, barArrangablesCurrentAmount, barArrangablesMaxAmount);
        UpdateText(barBloodProgressText, barBloodCurrentAmount, barBloodMaxAmount);
        UpdateText(barBloodyObjectsProgressText, barBloodyObjectsCurrentAmount, barBloodyObjectsMaxAmount);
        UpdateText(barClothesProgressText, barClothesCurrentAmount, barClothesMaxAmount);
        UpdateText(barCorpsesProgressText, barCorpsesCurrentAmount, barCorpsesMaxAmount);
        UpdateText(barDocumentsProgressText, barDocumentsCurrentAmount, barDocumentsMaxAmount);
        UpdateText(barMiscellaneousProgressText, barMiscellaneousCurrentAmount, barMiscellaneousMaxAmount);
        UpdateText(barUVCleanablesProgressText, barUVCleanablesCurrentAmount, barUVCleanablesMaxAmount);
        UpdateText(barWeaponsProgressText, barWeaponsCurrentAmount, barWeaponsMaxAmount);
        #endregion
        #region MEN'S BATHROOM TICK IMAGE
        UpdateText(menBathroomArrangablesProgressText, menBathroomArrangablesCurrentAmount, menBathroomArrangablesMaxAmount);
        UpdateText(menBathroomBloodProgressText, menBathroomBloodCurrentAmount, menBathroomBloodMaxAmount);
        UpdateText(menBathroomBloodyObjectsProgressText, menBathroomBloodyObjectsCurrentAmount, menBathroomBloodyObjectsMaxAmount);
        UpdateText(menBathroomClothesProgressText, menBathroomClothesCurrentAmount, menBathroomClothesMaxAmount);
        UpdateText(menBathroomCorpsesProgressText, menBathroomCorpsesCurrentAmount, menBathroomCorpsesMaxAmount);
        UpdateText(menBathroomDocumentsProgressText, menBathroomDocumentsCurrentAmount, menBathroomDocumentsMaxAmount);
        UpdateText(menBathroomMiscellaneousProgressText, menBathroomMiscellaneousCurrentAmount, menBathroomMiscellaneousMaxAmount);
        UpdateText(menBathroomUVCleanablesProgressText, menBathroomUVCleanablesCurrentAmount, menBathroomUVCleanablesMaxAmount);
        UpdateText(menBathroomWeaponsProgressText, menBathroomWeaponsCurrentAmount, menBathroomWeaponsMaxAmount);
        #endregion
        #region WOMEN'S BATHROOM TICK IMAGE
        UpdateText(womenBathroomArrangablesProgressText, womenBathroomArrangablesCurrentAmount, womenBathroomArrangablesMaxAmount);
        UpdateText(womenBathroomBloodProgressText, womenBathroomBloodCurrentAmount, womenBathroomBloodMaxAmount);
        UpdateText(womenBathroomBloodyObjectsProgressText, womenBathroomBloodyObjectsCurrentAmount, womenBathroomBloodyObjectsMaxAmount);
        UpdateText(womenBathroomClothesProgressText, womenBathroomClothesCurrentAmount, womenBathroomClothesMaxAmount);
        UpdateText(womenBathroomCorpsesProgressText, womenBathroomCorpsesCurrentAmount, womenBathroomCorpsesMaxAmount);
        UpdateText(womenBathroomDocumentsProgressText, womenBathroomDocumentsCurrentAmount, womenBathroomDocumentsMaxAmount);
        UpdateText(womenBathroomMiscellaneousProgressText, womenBathroomMiscellaneousCurrentAmount, womenBathroomMiscellaneousMaxAmount);
        UpdateText(womenBathroomUVCleanablesProgressText, womenBathroomUVCleanablesCurrentAmount, womenBathroomUVCleanablesMaxAmount);
        UpdateText(womenBathroomWeaponsProgressText, womenBathroomWeaponsCurrentAmount, womenBathroomWeaponsMaxAmount);
        #endregion
        #region KITCHEN TICK IMAGE
        UpdateText(kitchenArrangablesProgressText, kitchenArrangablesCurrentAmount, kitchenArrangablesMaxAmount);
        UpdateText(kitchenBloodProgressText, kitchenBloodCurrentAmount, kitchenBloodMaxAmount);
        UpdateText(kitchenBloodyObjectsProgressText, kitchenBloodyObjectsCurrentAmount, kitchenBloodyObjectsMaxAmount);
        UpdateText(kitchenClothesProgressText, kitchenClothesCurrentAmount, kitchenClothesMaxAmount);
        UpdateText(kitchenCorpsesProgressText, kitchenCorpsesCurrentAmount, kitchenCorpsesMaxAmount);
        UpdateText(kitchenDocumentsProgressText, kitchenDocumentsCurrentAmount, kitchenDocumentsMaxAmount);
        UpdateText(kitchenMiscellaneousProgressText, kitchenMiscellaneousCurrentAmount, kitchenMiscellaneousMaxAmount);
        UpdateText(kitchenUVCleanablesProgressText, kitchenUVCleanablesCurrentAmount, kitchenUVCleanablesMaxAmount);
        UpdateText(kitchenWeaponsProgressText, kitchenWeaponsCurrentAmount, kitchenWeaponsMaxAmount);
        #endregion
        #region STORAGE ROOM TICK IMAGE
        UpdateText(storageArrangablesProgressText, storageArrangablesCurrentAmount, storageArrangablesMaxAmount);
        UpdateText(storageBloodProgressText, storageBloodCurrentAmount, storageBloodMaxAmount);
        UpdateText(storageBloodyObjectsProgressText, storageBloodyObjectsCurrentAmount, storageBloodyObjectsMaxAmount);
        UpdateText(storageClothesProgressText, storageClothesCurrentAmount, storageClothesMaxAmount);
        UpdateText(storageCorpsesProgressText, storageCorpsesCurrentAmount, storageCorpsesMaxAmount);
        UpdateText(storageDocumentsProgressText, storageDocumentsCurrentAmount, storageDocumentsMaxAmount);
        UpdateText(storageMiscellaneousProgressText, storageMiscellaneousCurrentAmount, storageMiscellaneousMaxAmount);
        UpdateText(storageUVCleanablesProgressText, storageUVCleanablesCurrentAmount, storageUVCleanablesMaxAmount);
        UpdateText(storageWeaponsProgressText, storageWeaponsCurrentAmount, storageWeaponsMaxAmount);
        #endregion
        #region VIP ROOM TICK IMAGE
        UpdateText(vipRoomArrangablesProgressText, vipRoomArrangablesCurrentAmount, vipRoomArrangablesMaxAmount);
        UpdateText(vipRoomBloodProgressText, vipRoomBloodCurrentAmount, vipRoomBloodMaxAmount);
        UpdateText(vipRoomBloodyObjectsProgressText, vipRoomBloodyObjectsCurrentAmount, vipRoomBloodyObjectsMaxAmount);
        UpdateText(vipRoomClothesProgressText, vipRoomClothesCurrentAmount, vipRoomClothesMaxAmount);
        UpdateText(vipRoomCorpsesProgressText, vipRoomCorpsesCurrentAmount, vipRoomCorpsesMaxAmount);
        UpdateText(vipRoomDocumentsProgressText, vipRoomDocumentsCurrentAmount, vipRoomDocumentsMaxAmount);
        UpdateText(vipRoomMiscellaneousProgressText, vipRoomMiscellaneousCurrentAmount, vipRoomMiscellaneousMaxAmount);
        UpdateText(vipRoomUVCleanablesProgressText, vipRoomUVCleanablesCurrentAmount, vipRoomUVCleanablesMaxAmount);
        UpdateText(vipRoomWeaponsProgressText, vipRoomWeaponsCurrentAmount, vipRoomWeaponsMaxAmount);
        #endregion
        #region BETTING ROOM TICK IMAGE
        UpdateText(bettingRoomArrangablesProgressText, bettingRoomArrangablesCurrentAmount, bettingRoomArrangablesMaxAmount);
        UpdateText(bettingRoomBloodProgressText, bettingRoomBloodCurrentAmount, bettingRoomBloodMaxAmount);
        UpdateText(bettingRoomBloodyObjectsProgressText, bettingRoomBloodyObjectsCurrentAmount, bettingRoomBloodyObjectsMaxAmount);
        UpdateText(bettingRoomClothesProgressText, bettingRoomClothesCurrentAmount, bettingRoomClothesMaxAmount);
        UpdateText(bettingRoomCorpsesProgressText, bettingRoomCorpsesCurrentAmount, bettingRoomCorpsesMaxAmount);
        UpdateText(bettingRoomDocumentsProgressText, bettingRoomDocumentsCurrentAmount, bettingRoomDocumentsMaxAmount);
        UpdateText(bettingRoomMiscellaneousProgressText, bettingRoomMiscellaneousCurrentAmount, bettingRoomMiscellaneousMaxAmount);
        UpdateText(bettingRoomUVCleanablesProgressText, bettingRoomUVCleanablesCurrentAmount, bettingRoomUVCleanablesMaxAmount);
        UpdateText(bettingRoomWeaponsProgressText, bettingRoomWeaponsCurrentAmount, bettingRoomWeaponsMaxAmount);
        #endregion

        //TODO: make it work
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

    private void OpenTab(GameObject selectedTab)
    {
        foreach (GameObject tab in tabs)
        {
            tab.SetActive(tab == selectedTab);
        }
    }

    private void UpdateText(TextMeshProUGUI text, int currentAmount, int maxAmount)
    {
        text.text = $"{currentAmount:D2}/{maxAmount:D2}";
    }
}