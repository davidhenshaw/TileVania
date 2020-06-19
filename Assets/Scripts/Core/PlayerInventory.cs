public struct PlayerInventory
{
    public int lives { get; set; }
    public int cherries { get; set; }
    public int gems { get; set; }


}

public enum InventoryUpdateType
{
    ALL, Life, Cherry, Gem
}