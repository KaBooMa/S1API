# Event System - S1API

---

## 📢 Overview

S1API provides a full **EventManager** that fires signals at important gameplay moments.

Mods can subscribe easily to avoid needing their own Harmony patches.

---

## 🧍‍♂️ Player Events

- `OnPlayerSpawned`
- `OnPlayerDied`
- `OnPlayerRespawned`
- `OnMoneyChanged`
- `OnInventoryChanged`

## 🌍 World Events

- `OnWorldLoaded`
- `OnWorldUnloaded`
- `OnNightStarted`
- `OnNightEnded`

## 🏭 Warehouse Events

- `OnWarehouseBuilt`
- `OnWarehouseUpgraded`
- `OnWarehouseEntered`

## 📜 Quest Events

- `OnQuestAccepted`
- `OnQuestCompleted`
- `OnQuestFailed`

## 📦 Save Events

- `OnSaveDataLoaded`
- `OnSaveDataSaved`

## 📱 Phone Events (Future)

- `OnPhoneAppOpened`
- `OnPhoneAppClosed`

## 👮 Crime Events (Future)

- `OnPlayerWantedLevelChanged`
- `OnPlayerArrested`
- `OnPoliceRaidStarted`
- `OnPoliceRaidEnded`
- `OnPlayerEscapedPolice`

---

## 📚 Example Usage

```csharp
EventManager.OnPlayerSpawned += () =>
{
    Logger.Msg("Player has spawned!");
};

EventManager.OnMoneyChanged += (int money) =>
{
    Logger.Msg($"New money amount: {money}");
};
```

---

> Hook into the world without writing your own patches!

---
