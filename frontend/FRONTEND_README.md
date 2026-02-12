# Caderno de Erros - Frontend

Frontend Angular para a aplicação Caderno de Erros, uma ferramenta para concurseiros e estudantes organizarem seus erros de estudo.

## 🚀 Tecnologias

- **Angular 21** - Framework frontend
- **TypeScript** - Linguagem de programação
- **RxJS** - Programação reativa
- **TailwindCSS** - Estilização

## 📋 Pré-requisitos

- Node.js (versão 18 ou superior)
- npm (vem com o Node.js)
- Backend .NET rodando em `https://localhost:7030`

## 🔧 Instalação

1. Entre na pasta do frontend:
```bash
cd frontend/caderno-de-erros
```

2. Instale as dependências:
```bash
npm install
```

## 🏃 Executando a Aplicação

### Modo Desenvolvimento

```bash
npm start
```

ou

```bash
ng serve
```

A aplicação estará disponível em `http://localhost:4200`

### Build de Produção

```bash
npm run build
```

Os arquivos de produção serão gerados na pasta `dist/`

## 📁 Estrutura do Projeto

```
src/
├── app/
│   ├── components/        # Componentes da aplicação
│   │   ├── home/         # Página inicial
│   │   ├── materias/     # Gerenciamento de matérias
│   │   ├── assuntos/     # Gerenciamento de assuntos
│   │   └── erros/        # Gerenciamento de erros
│   ├── models/           # Interfaces TypeScript
│   │   ├── materia.model.ts
│   │   ├── assunto.model.ts
│   │   └── erro.model.ts
│   ├── services/         # Serviços HTTP
│   │   ├── materia.service.ts
│   │   ├── assunto.service.ts
│   │   └── erro.service.ts
│   ├── app.config.ts     # Configuração da aplicação
│   ├── app.routes.ts     # Rotas da aplicação
│   └── app.ts            # Componente raiz
├── environments/         # Configurações de ambiente
└── styles.css           # Estilos globais
```

## 🎯 Funcionalidades

### 1. Matérias
- ✅ Listar todas as matérias
- ✅ Criar nova matéria
- ✅ Excluir matéria
- ✅ Ver assuntos de uma matéria

### 2. Assuntos
- ✅ Listar assuntos (todos ou por matéria)
- ✅ Criar novo assunto
- ✅ Excluir assunto
- ✅ Ver erros de um assunto

### 3. Erros
- ✅ Listar erros (todos ou por assunto)
- ✅ Criar novo erro com:
  - Questão
  - Minha resposta
  - Resposta correta
  - Explicação
  - Observações
- ✅ Marcar erro como revisado
- ✅ Excluir erro
- ✅ Visualização organizada com cores

## 🌐 Rotas

- `/` - Página inicial
- `/materias` - Lista de matérias
- `/assuntos` - Lista de todos os assuntos
- `/assuntos/:materiaId` - Assuntos de uma matéria específica
- `/erros` - Lista de todos os erros
- `/erros/:assuntoId` - Erros de um assunto específico

## ⚙️ Configuração da API

A URL da API pode ser configurada em:

```typescript
// src/environments/environment.ts
export const environment = {
  production: false,
  apiUrl: 'https://localhost:7030'
};
```

## 🎨 Personalização

Os componentes utilizam CSS próprio para estilização. Você pode personalizar:

- Cores principais em cada arquivo `.css` dos componentes
- Estilos globais em `src/styles.css`
- Layout do navbar em `app.css`

## 📝 Observações

- Certifique-se de que o backend está rodando antes de iniciar o frontend
- O CORS já está configurado no backend para aceitar requisições de `http://localhost:4200`
- A aplicação usa standalone components (feature do Angular 14+)
- Todos os componentes são independentes e podem ser reutilizados

## 🤝 Como Usar

### 1. Primeiro Uso

1. Acesse a aplicação em `http://localhost:4200`
2. Clique em "Começar Agora" ou navegue para "Matérias"
3. Crie sua primeira matéria (ex: "Matemática")
4. Dentro da matéria, crie assuntos (ex: "Geometria", "Álgebra")
5. Em cada assunto, registre seus erros com detalhes

### 2. Registrando Erros

Ao criar um erro, você pode preencher:
- **Questão** (obrigatório): Descrição da questão
- **Minha Resposta**: O que você respondeu
- **Resposta Correta**: A resposta certa
- **Explicação**: Por que errou e o que aprendeu
- **Observações**: Notas adicionais

### 3. Revisão

- Marque erros como "Revisado" após estudá-los novamente
- Erros revisados aparecem com destaque verde
- Acompanhe seu progresso através dos contadores

## 🐛 Problemas Comuns

### Erro de CORS
Se encontrar erro de CORS, verifique se:
- O backend está rodando
- O CORS está configurado corretamente no `Program.cs`
- A URL da API no `environment.ts` está correta

### Erro ao conectar com a API
- Confirme que o backend está em `https://localhost:7030`
- Verifique se o certificado SSL está funcionando
- Tente acessar `https://localhost:7030/swagger` no navegador

## 📞 Suporte

Em caso de dúvidas ou problemas:
1. Verifique os logs do console do navegador (F12)
2. Verifique os logs do backend
3. Certifique-se de que todas as dependências foram instaladas

---

Desenvolvido para ajudar estudantes a transformarem seus erros em aprendizado! 📚✨
