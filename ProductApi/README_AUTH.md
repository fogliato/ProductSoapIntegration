# JWT Authentication - Products API

## Overview

This API implements JWT (JSON Web Token) based authentication to protect product endpoints.

## How It Works

### 1. Get a JWT Token

Make a POST request to `/api/auth/login` with your credentials:

```http
POST /api/auth/login
Content-Type: application/json

{
  "username": "your_username",
  "password": "password123"
}
```

**Success Response (200 OK):**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "expiresAt": "2025-10-15T14:30:00Z",
  "tokenType": "Bearer",
  "username": "your_username"
}
```

### 2. Use the Token

Include the token in the `Authorization` header of your requests:

```http
GET /api/products/123
Authorization: Bearer your_token_here
```

### 3. Validate Token

You can validate if a token is still valid:

```http
GET /api/auth/validate
Authorization: Bearer your_token_here
```

## Configuration

JWT settings are in the `appsettings.json` file:

```json
{
  "JwtSettings": {
    "SecretKey": "MySecretKeyForJWTTokenGeneration2025!@#ProductAPI",
    "Issuer": "ProductAPI",
    "Audience": "ProductAPIClients",
    "ExpirationInMinutes": 60
  }
}
```

### Parameters:

- **SecretKey**: Secret key to sign the tokens (CHANGE IN PRODUCTION!)
- **Issuer**: Token issuer
- **Audience**: Token audience
- **ExpirationInMinutes**: Token expiration time in minutes

## Security

⚠️ **IMPORTANT - For Production:**

1. **Change the SecretKey**: Use a strong and secure key
2. **Use HTTPS**: Configure `RequireHttpsMetadata = true` in Program.cs
3. **Implement real validation**: The current AuthController accepts any user with password "password123". Integrate with a real user database.
4. **Add custom Claims**: Add roles, permissions, etc.
5. **Implement Refresh Tokens**: To renew tokens without logging in again

## Testing with Swagger

1. Run the API
2. Access Swagger UI (usually at `https://localhost:7000`)
3. Click the **"Authorize"** button at the top of the page
4. Paste the JWT token (token only, without "Bearer")
5. Click "Authorize"
6. Now you can test the protected endpoints

## Testing with cURL

```bash
# 1. Get token
curl -X POST https://localhost:7000/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"username":"admin","password":"password123"}'

# 2. Use token (replace YOUR_TOKEN)
curl -X GET https://localhost:7000/api/products/123 \
  -H "Authorization: Bearer YOUR_TOKEN"
```

## Protected Endpoints

- ✅ `GET /api/products/{productId}` - **REQUIRES AUTHENTICATION**

## Public Endpoints

- 🔓 `POST /api/auth/login` - Login to get token
- 🔓 `GET /api/auth/validate` - Validate token (no prior authentication required)

## HTTP Status Codes

| Code | Description |
|------|-------------|
| 200 | Success |
| 400 | Bad request |
| 401 | Unauthorized (missing, invalid or expired token) |
| 404 | Resource not found |

## 401 Error Example

When trying to access a protected endpoint without token:

```json
{
  "type": "https://tools.ietf.org/html/rfc7235#section-3.1",
  "title": "Unauthorized",
  "status": 401
}
```

## Test Credentials

**Username:** Any username  
**Password:** `password123`

> ⚠️ These are demonstration credentials. Implement real validation in production!

