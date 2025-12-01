# Changelog

All notable changes to this project will be documented in this file.  
This project adheres to [Semantic Versioning](https://semver.org/).

---
## [1.1.4] - 2025-12-01
### Changed
- **Event Editor**
  - Editor of all event SO's in project and can create new ones and inspect where each is located.
  - Game state manager updated to handle mouse settings and an option to use timescale for pause menus.
---
## [1.1.3] - 2025-11-07
### Changed
- **Interaction System Refinement**
  - Replaced the simple `OverlapSphere` check in `PlayerInteract.cs` with a more robust, performant, and intuitive hybrid system.
  - The system now performs a "Crosshair Raycast" as its first priority, allowing for fast and precise selection of the object the player is aiming at.
  - If the crosshair raycast misses, the system falls back to a "Nearby Object" check.
  - This fallback check is now heavily filtered using `MathUtils` helpers to find the closest object that is **within the player's view angle (FOV)** and has a **clear line of sight (LOS)**, preventing interaction through walls or with objects behind the player.
  - Added `interactableLayerMask` and `obstacleLayerMask` fields to `PlayerInteract.cs` to dramatically optimize physics queries in both checks.

---

## [1.1.2] - 2025-11-05
### Changed
- **Save System Security Update**
  - Replaced old static encryption password approach with a new flexible `SetPassword()` method.
  - Added `_encryptionKey` private field to securely store the password used for encryption and decryption.
  - Updated `Save()` and `Load()` methods to check if `_encryptionKey` is null or empty before attempting encryption/decryption.
  - Logs a warning when a null or empty password is set, ensuring developer visibility while maintaining flexibility.

### Example
```csharp
// NEW: inside SaveSystem.cs
private string _encryptionKey = "";

/// <summary>
/// Sets the encryption key to be used for all save and load operations.
/// This MUST be called before any encryption/decryption is attempted.
/// </summary>
/// <param name="password">The password/key to use.</param>
public void SetPassword(string password)
{
    if (string.IsNullOrEmpty(password))
    {
        Debug.LogWarning("[SaveSystem] A null or empty password was set.");
    }
    _encryptionKey = password;
}
```

---

## [1.1.1] - 2025-10-29
### Changed
- **Dependency Reduction**
  - Removed all **DOTween** references from the toolkit to reduce external dependencies.
  - Replaced DOTween-based UI fading with a new custom coroutine system.
- `UIButtonAnimator` and related UI scripts now use the new fade routine instead of DOTween tweens.

### Added
- **PanelFadeRoutine.cs**
  - Introduced a lightweight fade utility for `CanvasGroup` components.
  - Supports smooth fade-in/out effects using Unity coroutines without requiring third-party libraries.

### Fixed
- Minor cleanup in UI animation scripts to ensure consistent fade transitions.

---

## [1.1.0] - 2025-10-27
### Added
- **Utility System Overhaul**
  - Added `ComponentUtils`, `GameObjectUtils`, `TransformUtils`, `CoroutineUtils`, `CollectionUtils`, `FileUtils`, `PlayerPrefsUtils`, `SceneUtils`, `EventUtils`, `LogUtils`, `UIUtils`, and `MathUtils` classes.
  - Introduced `EditorUtils` (editor-only utilities for folder creation, pinging assets, and marking objects dirty).
  - Added `MathUtils` with comprehensive gameplay-ready math helpers (angle, smoothing, randomization, falloff, line-of-sight, etc.).
  - Added `Oscillate()` function for flickering lights and organic animation.

### Changed
- Reorganized the toolkit into **modular utility classes** (separated by category) instead of a single monolithic `CommonUtils` file.
- Improved inline documentation and region headers for each file.
- Refined `EventUtils` to handle only event-safe invocations (moved logging methods to `LogUtils` for SRP compliance).
- Renamed `CommonUtils` → multiple single-responsibility files under `/Runtime/Utilities/`.

### Fixed
- Minor null-reference safety issues in `GetOrAddComponent` and `DestroyChildren`.
- Normalized function naming conventions across utilities (e.g., `SetActiveSafe`, `SmoothDampRotation`, `IsWithinViewAngle`).
- Improved JSON file handling safety in `FileUtils` to prevent empty file reads.

---

## [1.0.0] - 2025-10-11
### Added
- Initial release of the **Lonecraft Games Toolkit**.
- `Event Channel System` for ScriptableObject-based event-driven architecture.
- `UI Manager` for centralized menu and HUD handling.
- `Save/Load System` with JSON serialization.
- `Inventory Base System` (slot-based item management).

---

