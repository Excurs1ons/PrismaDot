$files = Get-ChildItem -Path "Scripts" -Filter "*.cs" -Recurse

foreach ($file in $files) {
    $content = Get-Content $file.FullName -Raw
    
    # 1. MonoBehaviour -> Node
    $content = $content -replace 'MonoBehaviour', 'Node'
    
    # 2. ScriptableObject -> Resource
    $content = $content -replace 'ScriptableObject', 'Resource'
    
    # 3. GameObject -> Node
    # Also handle <GameObject>
    $content = $content -replace 'GameObject', 'Node'
    
    # 4. Remove Unity-specific attributes
    $content = $content -replace '\[SerializeField\]\s*', ''
    $content = $content -replace '\[Header\([^)]*\)\]\s*', ''
    $content = $content -replace '\[CreateAssetMenu\([^)]*\)\]\s*', ''
    $content = $content -replace '\[UnityEngine\.Scripting\.Preserve\]\s*', ''
    $content = $content -replace '\[DisallowMultipleComponent\]\s*', ''
    $content = $content -replace '\[HideInInspector\]\s*', ''
    $content = $content -replace '\[Tooltip\([^)]*\)\]\s*', ''
    $content = $content -replace '\[Range\([^)]*\)\]\s*', ''
    $content = $content -replace '\[AddComponentMenu\([^)]*\)\]\s*', ''
    
    # 5. gameObject.SetActive(true/false) -> Visible = true/false
    # We use a regex to capture variable names if it's not just 'gameObject'
    # For 'gameObject.SetActive(val)', it becomes 'Visible = val'
    # For 'x.gameObject.SetActive(val)', it becomes 'x.Visible = val'
    # For 'x.SetActive(val)', it becomes 'x.Visible = val'
    $content = $content -replace '(?<!\.)\bgameObject\.SetActive\(([^)]+)\)', 'Visible = $1'
    $content = $content -replace '\b([^.\s]+)\.gameObject\.SetActive\(([^)]+)\)', '$1.Visible = $2'
    $content = $content -replace '\b([^.\s]+)\.SetActive\(([^)]+)\)', '$1.Visible = $2'

    # 6. Object.Destroy(x) -> x.QueueFree()
    $content = $content -replace 'Object\.Destroy\(([^)]+)\)', '$1.QueueFree()'
    
    # 7. using UnityEngine; -> using Godot;
    # Ensure Godot is added and UnityEngine is removed/commented
    if ($content -like "*using UnityEngine*") {
        if ($content -notlike "*using Godot;*") {
            $content = "using Godot;`r`n" + $content
        }
        $content = $content -replace 'using UnityEngine(\.[^;]+)?;', '// using UnityEngine$1;'
    }
    
    # Cleanup: remove VContainer.Unity if it exists (often paired with Unity projects)
    $content = $content -replace 'using VContainer\.Unity;', '// using VContainer.Unity;'
    
    Set-Content -Path $file.FullName -Value $content -NoNewline
}
