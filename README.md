# LondonStockExchangeTrades

```md
# API Design

## Endpoints

### **Submit a New Trade**
**POST** `/api/trades`  
_Submits a new trade and triggers a real-time notification._

#### **Request Example:**
```json
{
  "tickerSymbol": "AAPL",
  "price": 153.21,
  "shareVolume": 20,
  "brokerId": "987645"
}
```

---

### **Get the Current Value of a Specific Stock**
**GET** `/api/stocks/{tickerSymbol}`  
_Retrieves the latest value of a stock using its ticker symbol._

#### **Request Example:**
```http
GET /api/stocks/AAPL
```

#### **Response Example:**
```json
{
  "tickerSymbol": "AAPL",
  "price": 153.21,
  "lastUpdated": "2024-02-26T12:34:56Z"
}
```

---

### **Get the Current Values of All Stocks**
**GET** `/api/stocks`  
_Returns the latest values of all available stocks._

#### **Response Example:**
```json
[
  { "tickerSymbol": "AAPL", "price": 153.21 },
  { "tickerSymbol": "MSFT", "price": 298.45 },
  { "tickerSymbol": "GOOGL", "price": 2803.32 }
]
```

---

### **Get the Current Values for a List of Ticker Symbols**
**POST** `/api/stocks/range`  
_Retrieves stock values for a specific set of ticker symbols._

#### **Request Example:**
```json
{
  "tickerSymbols": ["AAPL", "MSFT", "GOOGL"]
}
```

#### **Response Example:**
```json
{
  "stocks": [
    { "tickerSymbol": "AAPL", "price": 153.21 },
    { "tickerSymbol": "MSFT", "price": 298.45 },
    { "tickerSymbol": "GOOGL", "price": 2803.32 }
  ]
}
```
```

This README section:
- Uses **headers** for better structure.
- Provides **request and response examples**.
- Formats **code blocks** properly.

Let me know if you need any modifications! 🚀
