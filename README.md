# S1API - Ultimate Developer Documentation

This is the official documentation for S1API, the unofficial modding API for Schedule I, designed to provide deep, accurate, and reliable entry points into the game's systems.

---

# 📚 Table of Contents

- [DeadDrops](#deaddrops)
- [GameTime](#gametime)
- [Internal Abstraction](#internal-abstraction)
- [Items](#items)
- [Leveling](#leveling)
- [Money](#money)
- [NPCs](#npcs)
- [PhoneApp](#phoneapp)
- [Products](#products)
- [Quests](#quests)
- [Saveables](#saveables)
- [Storages](#storages)
- [UI](#ui)

---

# 📦 Modules and Classes

## DeadDrops

<details>
<summary>DeadDropManager (S1API.DeadDrops)</summary>

### Purpose

Manages the spawning, tracking, and removal of Dead Drop loot containers in the world. Interfaces with SaveSystem for persistence.

### Methods

#### `void SpawnDeadDrop(Vector3 location)`

| Parameter  | Type      | Description                                       |
| :--------- | :-------- | :------------------------------------------------ |
| `location` | `Vector3` | The 3D coordinates where the drop will be placed. |

**Returns:** `void`

### Behavior

- Registers new drops into Saveables.
- Drop becomes immediately interactable.
- Included in game saves.

### Usage Example

```csharp
var manager = new DeadDropManager();
manager.SpawnDeadDrop(new Vector3(500f, 0f, 800f));
```

### Notes

- Validate location with `WorldUtils.IsLocationWalkable`.
- Dead Drops should not overlap with world obstacles.

</details>

<details>
<summary>DeadDropInstance (S1API.DeadDrops)</summary>

### Purpose

Represents a single active Dead Drop entity in the world.

### Properties

| Property      | Type      | Description                          |
| :------------ | :-------- | :----------------------------------- |
| `Location`    | `Vector3` | Where the Dead Drop resides.         |
| `IsCollected` | `bool`    | Whether the player has collected it. |

### Methods

#### `void Collect()`

Marks the Dead Drop as collected, deregistering it from active Saveables.

### Usage Example

```csharp
var drop = new DeadDropInstance();
if (!drop.IsCollected)
{
    drop.Collect();
}
```

### Notes

- May trigger quests if tied to mission objectives.

</details>

---

## GameTime

<details>
<summary>TimeManager (S1API.GameTime)</summary>

### Purpose

Manages the passage of time in-game, including advancing hours, days, and triggering time-sensitive events.

### Methods

#### `void AdvanceHour()`

Advances the clock by one hour.

#### `GameDateTime GetCurrentTime()`

Fetches the current world time.

### Usage Example

```csharp
var timeManager = new TimeManager();
timeManager.AdvanceHour();
Console.WriteLine(timeManager.GetCurrentTime().Hour);
```

### Notes

- Some NPC behaviors change based on hour of day.

</details>

---

## Internal Abstraction

<details>
<summary>Registerable (S1API.Internal.Abstraction)</summary>

### Purpose

Base class for systems or objects that must be registered with central managers.

### Notes

- Not directly used by players, but essential for framework extension.

</details>

<details>
<summary>Saveable (S1API.Internal.Abstraction)</summary>

### Purpose

Defines entities that can be serialized into game save files.

### Notes

- Used by systems like DeadDrops, Inventories, etc.

</details>

---

## Items

<details>
<summary>ItemManager (S1API.Items)</summary>

### Purpose

Manages item creation, lookup, and assignment in the game.

### Methods

#### `ItemInstance CreateItem(string itemId, int quantity)`

| Parameter  | Type     | Description                    |
| :--------- | :------- | :----------------------------- |
| `itemId`   | `string` | ID reference of the item type. |
| `quantity` | `int`    | Number of items to create.     |

**Returns:** `ItemInstance`

### Usage Example

```csharp
var itemManager = new ItemManager();
var newItem = itemManager.CreateItem("cocaine_brick", 1);
```

### Notes

- Make sure `itemId` matches the registry keys in the Product definitions.

</details>

<details>
<summary>ItemSlotInstance (S1API.Items)</summary>

### Purpose

Represents a slot holding an item in inventory or storage.

### Methods

#### `void AddQuantity(int amount)`

Increases the quantity of the held item.

### Usage Example

```csharp
var slot = new ItemSlotInstance();
slot.AddQuantity(5);
```

</details>

---

## Leveling

<details>
<summary>LevelManager (S1API.Leveling)</summary>

### Purpose

Handles player progression, level gains, and experience calculation.

### Methods

- (To be documented based on full source parsing)

### Notes

- Connected to quest completions and milestones.

</details>

---

## Money

<details>
<summary>CashInstance (S1API.Money)</summary>

### Purpose

Represents a tangible money object in-game.

### Methods

#### `void AddQuantity(float amount)`

Increases money held.

#### `void SetQuantity(float newQuantity)`

Sets total money to a fixed value.

### Usage Example

```csharp
var cash = new CashInstance();
cash.AddQuantity(500f);
```

</details>

---

## NPCs

<details>
<summary>NPC (S1API.NPCs)</summary>

### Purpose

Represents a non-player character (NPC) in the world, capable of sending messages and interacting with the player.

### Methods

#### `void SendTextMessage(string message, Response[]? responses = null, float responseDelay = 1f, bool network = true)`

Sends a text message from the NPC to the player.

| Parameter       | Type          | Description                           |
| :-------------- | :------------ | :------------------------------------ |
| `message`       | `string`      | Text to send.                         |
| `responses`     | `Response[]?` | Optional reply choices.               |
| `responseDelay` | `float`       | Time before allowing response.        |
| `network`       | `bool`        | Whether to sync message over network. |

### Usage Example

```csharp
var npc = new NPC();
npc.SendTextMessage("Meet me at the drop point.");
```

</details>

---

## PhoneApp

<details>
<summary>PhoneApp (S1API.PhoneApp)</summary>

### Purpose

Represents an application installed on the in-game smartphone.

### Notes

- Phone apps provide interaction UIs for player utility (e.g., shops, communication).

</details>

<details>
<summary>MyAwesomeApp (S1API.PhoneApp)</summary>

### Purpose

Template base for custom modded apps built by developers.

### Methods

- Extend UI and functionality for personal or public mods.

### Notes

- Highly customizable base class.

</details>

---

## Quests

<details>
<summary>Quest (S1API.Quests)</summary>

### Purpose

Represents a quest or mission that the player can undertake.

### Methods

#### `void Begin()`

- Starts the quest.

#### `void Complete()`

- Marks the quest as completed successfully.

#### `void Cancel()`

- Cancels the quest prematurely.

#### `void Fail()`

- Marks the quest as failed.

#### `void End()`

- Finishes the quest cleanup.

### Usage Example

```csharp
var quest = new Quest();
quest.Begin();
```

</details>

<details>
<summary>QuestEntry (S1API.Quests)</summary>

### Purpose

Represents an individual step or objective within a Quest.

### Methods

#### `void Begin()`

- Starts the step.

#### `void Complete()`

- Completes the step.

#### `void SetState(QuestState questState)`

- Changes the state of the entry.

</details>

<details>
<summary>QuestManager (S1API.Quests)</summary>

### Purpose

Global quest management system handling all active, completed, and failed quests.

### Notes

- Tracks quest states, triggers events.

</details>

---

## Products

<details>
<summary>ProductManager (S1API.Products)</summary>

### Purpose

Manages creation, definition, and tracking of all in-game product types such as drugs, packaging, and crafted items.

### Notes

- Interfaces with crafting, selling, and inventory systems.

</details>

<details>
<summary>ProductDefinition (S1API.Products)</summary>

### Purpose

Defines base attributes for a single type of product (e.g., cocaine brick, meth baggie).

### Properties

- Weight, Purity, Price data, etc.

### Notes

- Used heavily by `ItemManager` and `ProductManager`.

</details>

---

## Saveables

<details>
<summary>SaveableField (S1API.Saveables)</summary>

### Purpose

Represents a serializable field tied to save/load operations for objects like inventories, NPCs, player data.

### Notes

- Used throughout all persistent systems.

</details>

---

## Storages

<details>
<summary>StorageInstance (S1API.Storages)</summary>

### Purpose

Represents an interactive container or inventory storage (e.g., warehouse, player stash).

### Methods

#### `bool CanItemFit(ItemInstance itemInstance, int quantity = 1)`

Checks if the given item can fit into this storage instance.

#### `void AddItem(ItemInstance itemInstance)`

Adds a new item into the storage.

### Usage Example

```csharp
var storage = new StorageInstance();
var item = new ItemInstance();

if (storage.CanItemFit(item))
{
    storage.AddItem(item);
}
```

</details>

---

## UI

<details>
<summary>UIFactory (S1API.UI)</summary>

### Purpose

Helper class to create UI elements programmatically.

### Methods

- Create buttons, labels, panels, windows, etc.

### Notes

- Used mainly inside custom Phone Apps.

</details>

<details>
<summary>ClickHandler (S1API.UI)</summary>

### Purpose

Handles click interaction logic for UI elements.

### Methods

- Basic OnClick callbacks for UI events.

</details>

---
