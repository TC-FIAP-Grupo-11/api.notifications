# FCG.Api.Notifications

**Tech Challenge - Fase 3**
API de notificações — **deprecada em favor da `FCG.Lambda.Notification`**.

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

---

## Fase 3 — Novidades

### Deprecação
A lógica de notificação foi migrada para `FCG.Lambda.Notification`. Os consumers de `UserCreatedEvent` e `PaymentProcessedEvent` foram **removidos** desta API. O serviço permanece no cluster apenas para roteamento via AWS API Gateway.

### Observabilidade
- **AWS X-Ray**: middleware `app.UseXRay("fcg-notifications-api")` habilitado

### CI/CD (GitHub Actions)
- **CI** (`.github/workflows/ci.yml`): build em push/PR na `main`
- **CD** (`.github/workflows/cd.yml`): build Docker → push ECR → `kubectl set image` no EKS

**Secrets obrigatórios no repositório GitHub:**
- `AWS_ACCESS_KEY_ID`
- `AWS_SECRET_ACCESS_KEY`

### Kubernetes
Manifests em `k8s/`:
- `deployment.yaml` — Deployment
- `service.yaml` — Service NLB interno (integrado ao AWS API Gateway via VPC Link)
- `configmap.yaml` — Configurações mínimas
