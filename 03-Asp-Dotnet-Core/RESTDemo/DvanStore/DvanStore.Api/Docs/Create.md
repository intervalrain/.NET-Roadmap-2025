Create a new product record and return the product information after creation

## Request Example

```json
{
  "name": "iPhone 15 Pro",
  "description": "Latest iPhone phone Supporting 5G network with A17 Pro chip and 48MP main camera",
  "price": 35900,
  "category": "Smartphone",
  "isActive": true
}
```

## Validation Rules

- ✅ **Name**: 1-100 characters, required
- ✅ **Description**: Max 500 characters
- ✅ **Price**: 0.01 - 99999.99, required
- ✅ **Category**: 1-50 characters, required

> 💡 **Tips**: After successful creation, the URL of the new product will be returned in the `Location` header