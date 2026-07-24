# Product

<!-- impeccable:product-schema 1 -->

## Platform

web

## Users
Estudantes e concurseiros preparando-se para exames de alta concorrência (Concursos Públicos, ENEM, OAB, vestibulares). O usuário resolve baterias de questões e precisa registrar detalhadamente onde e por que errou para revisar futuramente e fixar o conteúdo.

## Product Purpose
Proporcionar um caderno digital centralizado para registro, categorização e acompanhamento de revisões de erros em questões de exames. O sucesso para o usuário significa identificar pontos fracos por matéria/assunto e transformar erros em aprendizado definitivo.

## Positioning
Diferente de simples blocos de notas ou bancos de questões genéricos, o Caderno de Erros é focado exclusivamente no ciclo pós-resolução: captura detalhada da questão, resposta do usuário vs. resposta correta, explicação do erro, observações pessoais e controle explícito do status/datas de revisão.

## Operating Context
Fluxo de estudos diários. O usuário acessa o sistema após resolver questões (em livros, simulados ou bancas), navega por Matérias e Assuntos, cadastra os erros cometidos e realiza sessões periódicas de revisão do seu histórico de erros.

## Capabilities and Constraints
- **Aba de barra de navegação superior**: As opções de Matérias, assuntos e erros, só devem aparecer quando o usuário estiver logado.
- **Hierarquia de Dados**: Matérias possuem Assuntos; Assuntos agrupam os Erros.
- **Estrutura do Erro**: Enunciado da questão, minha resposta, resposta correta, explicação, observações, data do erro, data de revisão e flag de revisado.
- **Autenticação e Segurança**: Fluxos de Login e Registro com proteção de rotas (`authGuard`).
- **Stack Técnica**: Frontend em Angular 21 (Stand-alone components, Signals/RxJS), SSR via `@angular/ssr`, Vitest e Tailwind CSS.

## Brand Commitments
- **Nome**: Caderno de Erros
- **Idioma Nativo**: Português (PT-BR)
- **Tom de Voz**: Prático, focado, instrutivo e livre de distrações, voltado a estudantes em rotina intensa de preparação.

## Evidence on Hand
- Código-fonte Angular estruturado em `src/app` com modelos (`materia`, `assunto`, `erro`, `auth`), componentes, serviços e guardas de rota.
- Configurações de rotas ativas (`/login`, `/register`, `/materias`, `/assuntos`, `/erros`).
- Ausência de assets falsos ou dados de teste fictícios.

## Product Principles
1. **Erro como ativo de aprendizado**: O registro minucioso (com explicação e observações) transforma falhas passadas em ganho de conhecimento.
2. **Navegação hierárquica e direta**: Acesso fluido na estrutura Matérias → Assuntos → Erros para diagnósticos precisos por tópico.
3. **Ergonomia visual de estudo**: Interface limpa e sem ruídos para sessões extensas de leitura e cadastro.
4. **Ciclo contínuo de revisão**: Clareza no status de revisão para garantir que nenhum erro fique esquecido.

## Accessibility & Inclusion
Navegação total por teclado, contraste rigoroso de texto para ergonomia visual prolongada e rótulos semânticos em formulários.
