namespace GameFramework
{
    using d4160.GameFramework;
    using UnityEngine.GameFoundation;

    public class GameFoundationDataController : DataControllerBase
    {
        public GameItemCatalog GameItemCatalog => GetScriptable<GameItemCatalog>(0);
        public StatCatalog StatCatalog => GetScriptable<StatCatalog>(1);
        public InventoryCatalog InventoryCatalog => GetScriptable<InventoryCatalog>(2);

        public Inventory MainInventory => Inventory.main;
        public Inventory Wallet => InventoryManager.wallet;

        public Inventory GetInventoryAt(int index) => InventoryManager.GetInventories()[index];

        public override T GetScriptable<T>(int dataIdx = 0)
        {
            var database = GameFoundationSettings.database;
            switch(dataIdx)
            {
                case 0:
                    return database.gameItemCatalog as T;
                case 1:
                    return database.statCatalog as T;
                case 2:
                    return database.inventoryCatalog as T;
            }

            return null;
        }

        public void SetMainItemIntStat(string itemDefinitionId, string statDefinitionId, int value)
        {
            InventoryItem item = MainInventory.GetItem(itemDefinitionId);
            item.SetStatInt(statDefinitionId, value);
        }

        public void SetMainItemIntStat(int itemDefinitionHash, int statDefinitionHash, int value)
        {
            InventoryItem trial = MainInventory.GetItem(itemDefinitionHash);
            trial.SetStatInt(statDefinitionHash, value);
        }

        public void SetMainItemFloatStat(string itemDefinitionId, string statDefinitionId, float value)
        {
            InventoryItem trial = MainInventory.GetItem(itemDefinitionId);
            trial.SetStatFloat(statDefinitionId, value);
        }

        public void SetMainItemFloatStat(int itemDefinitionHash, int statDefinitionHash, int value)
        {
            InventoryItem trial = MainInventory.GetItem(itemDefinitionHash);
            trial.SetStatFloat(statDefinitionHash, value);
            //StatManager.
        }
    }
}