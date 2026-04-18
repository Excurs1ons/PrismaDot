$files = Get-ChildItem -Path "Scripts" -Filter "*.cs" -Recurse

foreach ($file in $files) {
    $content = Get-Content $file.FullName -Raw
    
    # 1. using UnityEngine; -> using Godot;
    $content = $content -replace 'using UnityEngine;', 'using Godot;'
    
    # 2. using UnityEngine.AddressableAssets;
    $content = $content -replace 'using UnityEngine.AddressableAssets;', '// using UnityEngine.AddressableAssets; (To be replaced with Godot ResourceLoader)'
    
    # 3. Debug.Log -> GD.Print
    $content = $content -replace 'Debug\.Log', 'GD.Print'
    
    # 4. MonoBehaviour -> Node
    $content = $content -replace 'MonoBehaviour', 'Node'
    
    # 5. ScriptableObject -> Resource
    $content = $content -replace 'ScriptableObject', 'Resource'
    
    # 6. Attributes
    # We need to be careful with attributes that might be on the same line or have arguments
    $content = $content -replace '\[(UnityEngine\.Scripting\.)?Preserve\]\s*', ''
    $content = $content -replace '\[SerializeField\]\s*', ''
    $content = $content -replace '\[Header\([^)]*\)\]\s*', ''
    $content = $content -replace '\[Tooltip\([^)]*\)\]\s*', ''
    $content = $content -replace '\[Range\([^)]*\)\]\s*', ''
    $content = $content -replace '\[CreateAssetMenu\([^)]*\)\]\s*', ''
    $content = $content -replace '\[RequiredMember\]\s*', ''
    $content = $content -replace '\[HideInInspector\]\s*', ''
    $content = $content -replace '\[AddComponentMenu\([^)]*\)\]\s*', ''
    
    # 7. using Cysharp.Threading.Tasks; -> using System.Threading.Tasks;
    $content = $content -replace 'using Cysharp\.Threading\.Tasks;', 'using System.Threading.Tasks;'
    
    # 8. UniTask -> Task
    $content = $content -replace 'UniTask', 'Task'
    
    # 9. .ToUniTask()
    $content = $content -replace '\.ToUniTask\([^)]*\)', ''
    
    Set-Content -Path $file.FullName -Value $content -NoNewline
}
