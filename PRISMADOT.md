# PrismaDot Migration Documentation

## AOT Compilation Configuration
To enable AOT compilation in Godot (similar to IL2CPP), add the following to your `.csproj`:

```xml
<PropertyGroup>
  <PublishAot>true</PublishAot>
</PropertyGroup>
<ItemGroup>
  <TrimmerRootAssembly Include="GodotSharp" />
  <TrimmerRootAssembly Include="$(TargetName)" />
</ItemGroup>
```
*Note: This is similar to IL2CPP but currently less mature in the .NET ecosystem.*

## Plugin & Dependency Status

### To be Removed/Replaced (Unity-Specific)
- **UniTask (Cysharp.Threading.Tasks)**: Replace with standard `System.Threading.Tasks`.
- **Addressables (UnityEngine.AddressableAssets)**: Replace with Godot `ResourceLoader` or custom AssetBundle-like system.
- **TextMeshPro**: Replace with Godot `Label` / `RichTextLabel`.
- **VContainer**: Unity DI. Consider using `Microsoft.Extensions.DependencyInjection` or Godot-native patterns.
- **Firebase / TapSDK / Steamworks**: Need to find Godot-specific wrappers (e.g., GodotSteam) or implement via C# SDKs.

### Migration Progress
- [x] Clear Unity .meta files.
- [x] Global Namespace Rename: `PrismaFramework` -> `PrismaDot`.
- [ ] Remove Unity-specific Assembly Definitions.
- [ ] Replace `UnityEngine` with `Godot`.
- [ ] Create `PrismaDot.csproj`.
- [ ] Create `project.godot`.
