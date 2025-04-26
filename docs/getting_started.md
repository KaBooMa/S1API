# Getting Started with S1API

---

## 📥 Installing S1API (Coming Soon)

Installation instructions for modders and users will be posted here once the first public release is ready.

---

## 🧰 Using S1API in Your Mod

1. Reference `S1API.dll` in your mod project.
2. Include the appropriate using statements:

```csharp
using S1API;
using S1API.Managers;
using S1API.Events;
using S1API.Utilities;
```

3. Start developing!

---

## 🛠 Recommended Setup

- IDE: Visual Studio 2022+ or Rider
- References:
  - MelonLoader
  - Schedule I game assemblies
  - S1API.dll
- Target Framework: .NET Framework 4.7.2 (or matching Schedule I)

---

## 🚀 Quick Example: Save Player Stats

```csharp
SaveDataManager.SaveData("MyMod", new MySaveData { Money = 1000, Rank = 2 });
```

---

## 📚 Explore the Docs

Check the rest of the [Documentation](../docs/overview.md) to dive deeper!

---
