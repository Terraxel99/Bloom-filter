$outputFile = "./probabilities.txt"

# ===================================================================================

Write-Host "======================== FIXED VALUES FOR K ========================="

$kValues = 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 15, 50, 100
foreach ($kValue in $kValues) {
    &"..\bin\Release\net6.0\Assignment2.exe" $kValue 1000 100
}

# ===================================================================================

Write-Host "======================== FIXED VALUES FOR M ========================="

$mValues = 10, 20, 50, 100, 200, 500, 1000, 1443, 2000, 4000
foreach ($mValue in $mValues) {
    &"..\bin\Release\net6.0\Assignment2.exe" 10 $mValue 100
}

# ===================================================================================

Write-Host "======================== FIXED VALUES FOR N ========================="

$nValues = 20, 40, 69, 80, 100, 200, 500
foreach ($nValue in $nValues) {
    &"..\bin\Release\net6.0\Assignment2.exe" 10 1000 $nValue
}

Write-Host "====================================================================="

