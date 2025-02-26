
# London Stock Exchange API - MVP

## System Design for MVP

### Architecture Overview
- **Event-Driven**: Trades trigger a `TradeProcessedEvent`, which updates the cache asynchronously.
- **Caching**: Stock values are cached (e.g., in-memory using `IMemoryCache`) to reduce database load.
- **Database**: SQL Server with a simple `Trades` table.
- **API**: RESTful endpoints served via **ASP.NET Core**.

# Scalability and Enhancements

## **Is it Scalable?**
### **Current Design**
- Works well for an **MVP** with moderate traffic.
- **Bottlenecks:**  
  - **In-memory caching** and a **single database** struggle under high load.

### **High Traffic Coping Strategies**
- **Event Bus:** Replace `InMemoryEventBus` with a distributed message broker (e.g., **RabbitMQ, Kafka**) to handle trade events asynchronously.
- **Caching:** Use a **distributed cache** (e.g., **Redis**) instead of in-memory to scale across multiple instances.
- **Database:**  
  - Shard the **Trades** table by `TickerSymbol`.  
  - Use a **NoSQL database** (e.g., **Cosmos DB**) for better write scalability.
- **Load Balancing:** Deploy multiple API instances behind a **load balancer** (e.g., **Azure Load Balancer**).

---

## **Bottlenecks**
| Component  | Issue |
|------------|------------------------------------------------|
| **Database**  | Single **SQL Server** instance struggles with high write throughput. |
| **Caching**   | **In-memory cache** does not scale across multiple servers. |
| **Event Handling** | **Synchronous event processing** delays responses. |

---

## **Improved Architecture**
### **Microservices Approach**
- **Trade Ingestion Service:** Handles trade writes.  
- **Stock Query Service:** Reads stock values separately.  

### **CQRS (Command Query Responsibility Segregation)**
- **Separate Write & Read Models:**  
  - **Write:** Trade ingestion.  
  - **Read:** Stock value queries.
- **Precomputed Stock Averages:** Store in a **read-optimized store** (e.g., **Redis**) for quick lookups.

### **Event Sourcing**
- Store trades as **events** in a stream (e.g., **Kafka**).
- **Replay events** to compute stock averages **on demand** or **periodically**.

### **Cloud Deployment**
- Deploy on **Azure/AWS** with:  
  - **Auto-scaling groups.**  
  - **Managed databases** (e.g., **Azure SQL**).  
  - **Serverless components** (e.g., **Azure Functions** for trade processing).

---


## API Design

### Endpoints

#### Submit a New Trade
**POST** `/api/trades`  
_Submits a new trade and triggers a real-time notification._

##### Request Example
```json
{
  "tickerSymbol": "AAPL",
  "price": 153.21,
  "shareVolume": 20,
  "brokerId": "987645"
}
```
##### Response
Status: 200 OK (on success)

#### Get the Current Value of a Specific Stock
**GET** `/api/stocks/{tickerSymbol}`
_Retrieves the latest value of a stock using its ticker symbol (average price across all trades)
.
##### Request Example
http
GET /api/stocks/AAPL

##### Response Example
```json
{
  "tickerSymbol": "AAPL",
  "averagePrice": 153.21
}
```

#### Get the Current Values of All Stocks
**GET** `/api/stocks`
_Returns the latest values of all available stocks.

##### Response Example
```json
[
  { "tickerSymbol": "AAPL", "averagePrice": 153.21 },
  { "tickerSymbol": "MSFT", "averagePrice": 298.45 },
  { "tickerSymbol": "GOOGL", "averagePrice": 2803.32 }
]
```

#### Get the Current Values for a List of Ticker Symbols
**POST** `/api/stocks/range`
_Retrieves stock values for a specific set of ticker symbols.
##### Request Example
```json
[
  "AAPL",
  "MSFT",
  "GOOGL"
]
```


##### Response Example

```json
[
  { "tickerSymbol": "AAPL", "averagePrice": 153.21 },
  { "tickerSymbol": "MSFT", "averagePrice": 298.45 },
  { "tickerSymbol": "GOOGL", "averagePrice": 2803.32 }
]
```


# Notes and Assumptions

- **No advanced security:** Authentication is assumed via **SDK/middleware**.  
- **Basic scalability considerations:** Includes **caching** but not a fully distributed system.  
- **Stock value calculation:**  
  - **Simple average** as specified.  
  - **Formula:** Stock value = **Average price** of all transactions for a given `tickerSymbol`.  
- **API uses DTOs:**  
  - Ensures clean data transfer.  
  - **DTOs used:** `TradeRequestDto` and `StockValueDto`.  
- **Caching is implemented:**  
  - Improves performance for **frequent stock value queries**.  




# Using `insert_trades.ps1` to Submit Trades  

## **Prerequisites**  
Before running the script, ensure you have:  
- **PowerShell** installed on your system.  
- The required **API endpoint** (`/api/trades`) available.  
- **Internet access** (or network access to the API server).  

---

## **How to Run the Script**  

### **Open PowerShell**  
- Press `Win + X` and select **PowerShell** (or **Terminal** on Windows 11).  
- Navigate to the folder containing `insert_trades.ps1`:  
  ```powershell
  cd C:\path\to\script
  Set-ExecutionPolicy RemoteSigned -Scope Process
  .\insert_trades.ps1
  ```
