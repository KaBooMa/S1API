# Quest System - S1API

---

## 🎯 Overview

Create, manage, and complete quests dynamically in Schedule I using S1API!

---

## ➕ Adding a Quest

```csharp
QuestManager.AddQuest(new Quest
{
    QuestId = "first_deal",
    Title = "Seal Your First Deal",
    Description = "Sell your first batch successfully.",
    Reward = 5000
});
```

---

## ✅ Completing a Quest

```csharp
QuestManager.CompleteQuest("first_deal");
```

---

## ❌ Failing a Quest

```csharp
QuestManager.FailQuest("first_deal");
```

---

## 🛠️ Notes

- Quest IDs must be unique
- Rewards can be anything you want — cash, items, status

---

> Build RPG-like experiences inside Schedule I!

---
