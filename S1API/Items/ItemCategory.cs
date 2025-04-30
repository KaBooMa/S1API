namespace S1API.Items
{
    /// <summary>
    /// A list of item categories available in-game.
    /// </summary>
    public enum ItemCategory
    {
        /// <summary>
        /// Represents items such as Cocaine, Weed, etc.
        /// Oddly, SpeedGrow is in this category as of (v0.3.4f8).
        /// </summary>
        Product,
        
        /// <summary>
        /// Represents items such as Baggies, Bricks, Jars, etc.
        /// </summary>
        Packaging,
        
        /// <summary>
        /// Represents items such as Soil, Fertilizer, Pots, etc. 
        /// </summary>
        Growing,
        
        /// <summary>
        /// Represents equipment tools such as the clippers.
        /// Oddly, trash bags is in this category as of (v0.3.4f8).
        /// </summary>
        Tools,
        
        /// <summary>
        /// Represents items such as TV, Trash Can, Bed, etc.
        /// </summary>
        Furniture,
        
        /// <summary>
        /// Represents items such as Floor Lamps, Halogen Lights, etc.
        /// </summary>
        Lighting,
        
        /// <summary>
        /// Represents cash-based items.
        /// </summary>
        Cash,
        
        /// <summary>
        /// Represents items such as Cuke, Energy Drink, etc.
        /// </summary>
        Consumable,
        
        /// <summary>
        /// Represents items such as Drying Rack, Brick Press, Mixing Station, etc.
        /// </summary>
        Equipment,
        
        /// <summary>
        /// Represents items such as Acid, Banana, Chili, etc.
        /// </summary>
        Ingredient,
        
        /// <summary>
        /// Represents items such as GoldBar, WallClock, WoodSign, etc.
        /// </summary>
        Decoration,
        
        /// <summary>
        /// Represents clothing items.
        /// </summary>
        Clothing
    }
}