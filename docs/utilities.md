# Utilities - S1API

---

## 🧰 Logger

Simple wrapper for MelonLogger.

```csharp
Logger.Msg("Hello from my mod!");
Logger.Warning("Something might be wrong...");
Logger.Error("Critical failure!");
```

---

## 🧪 ReflectionHelper

Safely get private fields, methods, or types.

```csharp
var privateField = ReflectionHelper.GetPrivateField<MyClass, int>(instance, "myFieldName");
```

---

## 🧬 TypeHelper

Find types safely by name.

```csharp
var type = TypeHelper.FindType("GameNamespace.PlayerController");
```

---

> These tools help your mod survive Schedule I updates!

---
