# Contributing
Welcome potential contributor! 
I appreciate your interest in this project.
Please read over the below in full to help you get started and set expectations 😊

## Important!!!
- Please thoroughly read over [CODING_STANDARDS.md](CODING_STANDARDS.md) before contributing.
- Do **NOT** alter my GitHub actions unless you have a good reason. 
  I will close your PR and ban you from the project if malicious intent is found.

## How to Build the Project
1. Clone the project using `git clone https://github.com/KaBooMa/S1API`
2. Copy the `example.build.props` file to a new file named `local.build.props`. This file located in the base repository directory.
3. Update all properties in `local.build.props` to proper paths for your local system.
   - Personally, I have four copies of Schedule I locally. This way I can test all four builds independently. 
     You can swap between just one if you switch. It will just be a bit more of a hassle 😊.
4. If you're using a light IDE / editor, you will need to manually restore packages. 
   `dotnet restore` should get this done for you.
    - You also will need to manually build in this case. This is as simple as `dotnet build ./S1API.sln`. 
    - If you need to build just for `netstandard2.1` or `net6.0`, you can do so using `dotnet build ./S1API.sln -f netstandard2.1`.

## PR Preparations
Verify your changes will successfully build for all **four** build configurations prior to PR please.
Regardless, we have a GitHub action that will verify proper build before commit to `bleeding-edge`. 
Ultimately, this just saves you time and gets your changes into the API faster.

| Build Type    | Description                                    |
|---------------|------------------------------------------------|
| Il2CppMelon   | MelonLoader for Il2Cpp (base game) builds      |
| Il2CppBepInEx | BepInEx 6.0 for Il2Cpp (base game) builds      |
| MonoMelon     | MelonLoader for Mono (alternate branch) builds |
| MonoBepInEx   | MelonLoader for Mono (alternate branch) builds |

## Proper Contributing Channels
All pull requests **must** go into `bleeding-edge` before `stable`.
If you make a pull request for `stable`, I **will** be changing it to verify build.