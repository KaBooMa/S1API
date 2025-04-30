# Coding Standards
While I don't want to be overly-strict on coding style, there are specific standards to apply.
The standards in place aren't meant to cause headache.
They are to keep the project consistent and predictable.

## General Best Practice
* Please review the codebase thoroughly before doing a PR.

## File and Namespace Structure
* All classes must exist in a logical namespace matching the folder structure.
* Internal API code should be placed in `S1API.Internal` sub-namespaces.
```C#
namespace S1API.Internal.Utils { ... }
```

## Naming Conventions
These are purely based on my preference.
Feel free to discuss with me if you feel otherwise.
* In general, naming follows the default Jetbrains Rider suggestions.
* **PascalCase** is to be utilized for class names, methods, properties, and non-private fields.
* **camelCase** is to be used for local variables and private fields.
* Prefix private fields with `_`.
```C#
private int _myInteger;
public float AddFloats(float floatOne, float floatTwo) => ...
```
* Enums do not need to be prefixed with `E`. I'd like us to keep this consistent.
* Utilize existing common naming conventions from the codebase. 
  I don't want to see `SomeManager`, `SomeHandler`, `SomeSystem`, etc. throughout this codebase.
  See what is already in use naming-wise, and commit to it like everyone else.

## Access Modifiers
* Internal classes must be marked as `internal` to prevent confusion for modders.
* Modder-facing API classes, methods, properties, and fields should use `public`.
* The exception to this rule is when you want a property available for abstraction by modders, but not publicly accessible.
  * An example of this is the `NPC.cs`. We utilize the `protected` access modifier to allow them to override, but not access from outside the class. 
```C#
public static string GenerateString(int length) { ... }
```
* Explicit usage of access methods at all times.
* Arrow functions (`=>`) are used for simple methods and properties. They are to be placed below the declaration and indented once.
```C#
// property example
public string Name =>
    S1NPC.FullName;

// method example
public float AddNumbers(float a, float b) =>
    a + b;
```
* Use `readonly` or `const` for immutable values.
* Nullable variables should be declared as so using `?`.

## Documentation
* All modder-facing API declarations must have an associated summary.
```C#
/// <summary>
/// Destroys all game objects in the world.
/// </summary>
public void DestroyGameWorld() { ... }
```
* This is now enforced and **will** produce build warnings. Please keep your code documented.

## Conditional Build Compilation
* Use `#if (MONOMELON || MONOBEPINEX)` and `#elif (IL2CPPBEPINEX || MONOBEPINEX)` for platform-specific logic.
* Wrap and alias `using` statements to provide platform-agnostic support.


## What **NOT** to Do
* Do not leak Il2Cpp types across the API. 
  The API is intended to leave the modder in the native C# `System` environment.
* Utilize the tools present in our `Internal` namespace.
  They are there because we've collectively agreed on a better solution to a common problem.