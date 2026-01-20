# FCG.Api.Notifications

**Tech Challenge - Fase 2**  
API simples para logging de notificações por email no console.

> **⚠️ Este microsserviço faz parte de um sistema maior.**  
> Para executar toda a plataforma (Docker Compose ou Kubernetes), veja: [FCG.Infra.Orchestration](../FCG.Infra.Orchestration/README.md)

## Propósito

Esta API simula o envio de emails de:
- Boas-vindas (Welcome)
- Confirmação de compra (PurchaseConfirmation)

Os emails **NÃO são enviados de verdade**. Apenas logados no console para fins de demonstração.

## Variáveis de Ambiente

```bash
# RabbitMQ
Messaging__RabbitMQ__Host="localhost"
Messaging__RabbitMQ__Username="guest"
Messaging__RabbitMQ__Password="guest"
```

## Como Executar

### Localmente
```bash
cd src/FCG.Api.Notifications
dotnet run
```

Acesse: http://localhost:5004/health

### Docker
```bash
docker build -t fcg-notifications .
docker run -p 5004:80 fcg-notifications
```
