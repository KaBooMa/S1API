@echo off
setlocal enabledelayedexpansion

REM Base URL for raw files on GitHub (master branch)
set "BASE=https://raw.githubusercontent.com/xmusjackson/UnityEngine.BE.Il2CppAssetBundleManager/master"

REM List of files to download
set "FILES=Il2CppAssetBundle.cs Il2CppAssetBundleManager.cs Il2CppAssetBundleRequest.cs InteropUtils.cs README.md LICENSE.txt NOTICE.md"

for %%F in (%FILES%) do (
    echo --------------------------------------------------
    echo Downloading %%F...
    curl -sSL "%BASE%/%%F" -o "%%F"

    if errorlevel 1 (
        echo   [FAIL] %%F could not be downloaded.
    ) else (
        echo   [ OK ] %%F downloaded.

        REM Only wrap .cs files
        if /I "%%~xF"==".cs" (
            echo   ▶ Wrapping %%F with #if…#endif…

            REM Move original to a temp file
            move /Y "%%F" "%%F.tmp" >nul

            REM Recreate with wrapper
            (
                echo #if IL2CPPBEPINEX
                type "%%F.tmp"
                echo #endif
            ) > "%%F"

            del "%%F.tmp"
            echo   ✔ Wrapped %%F
        )
    )
)

echo.
echo All files processed.
pause
