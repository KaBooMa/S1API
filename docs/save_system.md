# Save System - S1API

---

## 💾 Overview

S1API provides a simple, clean, and safe way to save and load custom mod data.

Your data is separated from vanilla saves, minimizing the risk of corruption or loss.

---

## ➕ Saving Data

```csharp
var playerData = new MyPlayerData
{
    level = 5,
    nickname = "Legend"
};
SaveDataManager.SaveData("MyAwesomeMod", playerData);
```

---

## 📥 Loading Data

```csharp
var data = SaveDataManager.LoadData<MyPlayerData>("MyAwesomeMod");
if (data != null)
{
    Logger.Msg($"Welcome back, {data.nickname}!");
}
```

---

## 🛡️ Best Practices

- Always check if `LoadData` returns `null`
- Save frequently after important changes (level up, purchase, etc.)
- Avoid blocking save operations inside performance-critical code (use background threads if needed)

---

> S1API Save System is **built to be easy and safe** — use it to make persistent mods!

---
