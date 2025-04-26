# Example Code - S1API

---

## 🎮 Subscribe to Events

```csharp
EventManager.OnPlayerSpawned += () =>
{
    Logger.Msg("Player spawned!");
};
```

---

## 💾 Save Player Stats

```csharp
SaveDataManager.SaveData("MyMod", new MyStatsData { Level = 10 });
```

---

## 📜 Create a Quest

```csharp
QuestManager.AddQuest(new Quest
{
    QuestId = "get_paid",
    Title = "First Salary",
    Description = "Earn your first $1,000.",
    Reward = 1000
});
```

---

## 🏭 Listen for Warehouse Builds

```csharp
EventManager.OnWarehouseBuilt += (id) =>
{
    Logger.Msg($"Warehouse {id} is now yours!");
};
```

---
