$outputFile = "./probabilities.txt"

# ===================================================================================

Write-Host "======================== FIXED VALUES FOR K ========================="

# Run tests for k (nb hash fct) fixed
$kValuesMax = 15
for ($i = 0; $i -lt $kValuesMax; $i++) {
    &"..\Assignment2\bin\Release\net6.0\Assignment2.exe" $i 1000 100
}

# ===================================================================================

Write-Host "======================== FIXED VALUES FOR M ========================="

$mValues = 500, 1000
foreach ($mValue in $mValues) {
    ..\Assignment2\bin\Release\net6.0\Assignment2.exe 10 $mValue 100
}

# ===================================================================================

Write-Host "======================== FIXED VALUES FOR M ========================="

$nValues = 20, 80, 100
foreach ($nValue in $nValues) {
    ..\Assignment2\bin\Release\net6.0\Assignment2.exe 10 1000 $nValue
}

Write-Host "====================================================================="

