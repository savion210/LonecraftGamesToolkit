# Changelog

All notable changes to this project will be documented in this file.  
This project adheres to [Semantic Versioning](https://semver.org/).

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

