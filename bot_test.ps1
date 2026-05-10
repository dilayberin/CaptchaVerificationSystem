$baseUrl = "http://localhost:5015/api/captcha"

for ($i = 1; $i -le 10; $i++) {

    $captcha = Invoke-RestMethod -Method Post -Uri "$baseUrl/generate-file"

    $challengeId = $captcha.challengeId
    $images = $captcha.images

    # Bot gibi: 5-9 arası rastgele görsel seç
    $selectionCount = Get-Random -Minimum 5 -Maximum 10
    $selectedImages = $images | Get-Random -Count $selectionCount | Select-Object -ExpandProperty imageId

    # Bot gibi: çok hızlı cevap süresi
    $responseTime = Get-Random -Minimum 100 -Maximum 500

    $body = @{
        challengeId = $challengeId
        selectedChallengeImageIds = @($selectedImages)
        responseTimeMs = $responseTime
    } | ConvertTo-Json -Depth 5

    $result = Invoke-RestMethod `
        -Method Post `
        -Uri "$baseUrl/verify" `
        -ContentType "application/json" `
        -Body $body

    Write-Host "`nBOT TEST #$i"
    Write-Host "Response Time: $responseTime ms"
    Write-Host ($result | ConvertTo-Json -Depth 5)

    Start-Sleep -Seconds 1
}