# S - Single Responsibility Principle (SRP)

> "A class should have only **one reason to change**."
> - Robert C. Martin (Uncle Bob)

---

## 📌 What Is SRP?

SRP states that every class or module should be responsible for **one and only one** part of the functionality. If a class handles multiple concerns, any change to one concern risks breaking the others.

---

## ❌ The Problem - God Class

`BadOrderService` does **everything**:

| Concern | Should It Be Here? |
|---|---|
| Calculate order total | ✅ Yes - that's order logic |
| Save to database | ❌ No - belongs in a Repository |
| Send confirmation email | ❌ No - belongs in EmailService |
| Generate invoice | ❌ No - belongs in InvoiceService |
| Write logs | ❌ No - belongs in ILogger / LogService |

**Result:** 5 reasons to change this one class. A designer changes the invoice template → you're editing `BadOrderService`. A DevOps engineer changes the email provider → you're editing `BadOrderService`. Fragile. Untestable.

---

## ✅ The Solution - Focused Classes

```
OrderService        → Only order business logic (calculate totals, update status)
OrderRepository     → Only database persistence
EmailService        → Only email communication
InvoiceService      → Only invoice generation
ILogger<T>          → Only logging
```

Each class has **exactly one reason to change**. Change the email provider? Only `EmailService` changes. Switch databases? Only `OrderRepository` changes. `OrderService` doesn't even know.

---

## 🔁 Try It in Swagger

Run the project and open `https://localhost:{port}/swagger`

| Endpoint | What It Demonstrates |
|---|---|
| `POST /api/bad-order/process` | ❌ God class - one class doing everything |
| `POST /api/good-order/process` | ✅ SRP - focused, single-purpose classes |

**Sample Request Body:**
```json
{
  "id": 1,
  "customerEmail": "john@example.com",
  "customerName": "John Doe",
  "items": [
    { "productName": "Laptop", "quantity": 1, "unitPrice": 999.99 },
    { "productName": "Mouse", "quantity": 2, "unitPrice": 29.99 }
  ]
}
```

---

## 🧪 Why SRP Makes Testing Easy

```csharp
// ✅ GOOD - Test OrderService in complete isolation
var mockRepo    = new Mock<IOrderRepository>();
var mockEmail   = new Mock<IEmailService>();
var mockInvoice = new Mock<IInvoiceService>();
var mockLogger  = new Mock<ILogger<OrderService>>();

var service = new OrderService(mockRepo.Object, mockEmail.Object, mockInvoice.Object, mockLogger.Object);

// You can test JUST the order logic without any DB, email, or file I/O
var result = service.ProcessOrder(order);

mockRepo.Verify(r => r.Save(It.IsAny<Order>()), Times.Once);
mockEmail.Verify(e => e.SendOrderConfirmation(It.IsAny<Order>()), Times.Once);
```

With `BadOrderService`, you can't test order logic without triggering DB calls, emails, and file writes. SRP is what makes unit testing practical.

---

## 💡 Key Takeaway

> If you describe what a class does and you use the word **"and"** - it probably violates SRP.
> `OrderService` saves to DB **and** sends emails **and** generates invoices → ❌
> `OrderService` processes orders → ✅

---

📌 LinkedIn Post → [link-here]  
🔙 Back to main series → [SOLID.Principles.Demo](../README.md)
