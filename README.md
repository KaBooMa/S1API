# S1API - A Schedule One Mono / Il2Cpp Cross Compatibility Layer
S1API is an open source collaboration project to help standardize Schedule One modding processes.
The goal is to provide a standard place for common functionalities so you can focus on making content versus reverse engineering the game.

> **This GitHub repository is intended for developers.**
> If you are just looking to mod your game, please refer to the releases / mod repositories such as Thunderstore and Nexus Mods.

## What Does it Do?
* Allows creation of new game elements (quests, npcs, etc.)
* Provides an easy-to-use abstraction for save/load of class data
* Eases access to various common game functions without needing to import the assembly themselves, removing the Mono / Il2Cpp dependency for your mod.
* Allow modders to live in the Managed environment, regardless of Mono / Il2Cpp development
* Lower the bearer of entry into S1 modding, especially for Il2Cpp builds
* Support mod developers by allowing them to compile a single assembly that works across both builds
* Who knows what else? We will have to see who all is willing to collaborate on this ❤️

## What Are the Limitations?
* S1API will NOT be the be-all and end-all. It's just not possible.
* Handle Il2Cpp / Mono communication when utilizing game assemblies as dependencies
* Cover all modding needs. It will never be as flexible as writing your own mod referencing game assemblies.

## How It's Designed to Work
S1API is designed to compile for Mono and Il2Cpp separately.
Mod users don't need to worry about this though. 
The standard installation ships with all builds and a plugin to dynamically load the proper version!

Mod developers can develop their mods on whichever build, Mono or Il2Cpp, without having to step into the Il2Cpp environment.
Caveat: If you do utilize Il2Cpp functionality within your mod, you lose cross compatibility.
I can't think of why you would. I wanted to make sure that is clarified though.

S1API is designed to be a tag-along as well.
If you want to do custom content specific to Mono or Il2Cpp, S1API can still assist with some of the common repetitive tasks.

## Want to Contribute?
This is a massive project with so many different areas to specialize in.
If you're interested in contributing, please do!
Look over the [CONTRIBUTING.md](CONTRIBUTING.md) for guidance on code standards and the process.