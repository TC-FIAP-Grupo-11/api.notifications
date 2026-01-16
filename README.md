# FCG.Api.Notifications

API simples para logging de notificações por email no console.

## Propósito

Esta API simula o envio de emails de:
- Boas-vindas (Welcome)
- Confirmação de compra (PurchaseConfirmation)

Os emails **NÃO são enviados de verdade**. Apenas logados no console para fins de demonstração.

## Como executar

```bash
cd src/FCG.Api.Notifications
dotnet run
```

Acesse: http://localhost:5000/swagger

## Endpoint

**POST /api/notifications/send**

```json
{
  "email": "user@example.com",
  "subject": "Bem-vindo!",
  "body": "Obrigado por se cadastrar.",
  "type": "Welcome"
}
```

Os tipos disponíveis: `Welcome`, `PurchaseConfirmation`

## Docker

```bash
docker build -t fcg-notifications .
docker run -p 5000:80 fcg-notifications
```
