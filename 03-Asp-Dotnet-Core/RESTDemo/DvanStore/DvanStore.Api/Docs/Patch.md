Use **PATCH** method to partially update the information of the specified product, only update the provided fields

## Features

- 🎯 **Optional update**: Only update the provided fields
- 🔒 **Security validation**: All fields are validated
- ⚡ **Performance optimization**: No need to provide complete data

## Request Examples

Only update the price:
```json
{
  "price": 29900
}
```

Update multiple fields:
```json
{
  "name": "iPhone 15 Pro Max",
  "price": 42900,
  "description": "### Upgraded iPhone\n\nBigger screen, better performance!"
}
```

## Field Reference

| Field | Type | Required | Description |
|-------|------|----------|-------------|
| name | string | ❌ | Product name |
| description | string | ❌ | Product description |
| price | decimal | ❌ | Product price |
| category | string | ❌ | Product category |
| isActive | boolean | ❌ | Is active |