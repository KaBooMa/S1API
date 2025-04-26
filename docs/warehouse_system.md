# Warehouse System - S1API

---

## 🏭 Overview

Manage and interact with Warehouses using S1API!

Expand player properties, track upgrades, or trigger bonuses.

---

## 🛠️ Warehouse Events

- `OnWarehouseBuilt`
- `OnWarehouseUpgraded`
- `OnWarehouseEntered`

---

## 🔨 Example: Listen for Warehouse Built

```csharp
EventManager.OnWarehouseBuilt += (warehouseId) =>
{
    Logger.Msg($"Warehouse {warehouseId} was built!");
};
```

---

> Future updates will include full warehouse content customization.

---
