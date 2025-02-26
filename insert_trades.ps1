# Define the API URL
$apiUrl = "https://localhost:7160/api/Trades"

# Define trade data
$trades = @(
    @{ tickerSymbol = "AAPL"; price = 153.21; shareVolume = 20; brokerId = "987645" }
    @{ tickerSymbol = "MSFT"; price = 298.45; shareVolume = 50; brokerId = "567890" }
    @{ tickerSymbol = "GOOGL"; price = 2803.32; shareVolume = 10; brokerId = "123456" }
    @{ tickerSymbol = "AMZN"; price = 3450.75; shareVolume = 15; brokerId = "654321" }
    @{ tickerSymbol = "TSLA"; price = 725.50; shareVolume = 30; brokerId = "112233" }
)

# Loop through each trade and send a POST request
foreach ($trade in $trades) {
    $jsonBody = $trade | ConvertTo-Json -Depth 10
    Invoke-RestMethod -Uri $apiUrl -Method Post -Headers @{ "Content-Type" = "application/json" } -Body $jsonBody
    Write-Host "Inserted trade for $($trade.tickerSymbol)"
}

Write-Host "All trades inserted successfully!"
