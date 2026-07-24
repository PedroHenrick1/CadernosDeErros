---
name: Caderno de Erros
description: Sistema de design focado em captura, categorização e revisão de erros em exames e concursos públicos.
colors:
  primary: "#4f46e5"
  primary-hover: "#4338ca"
  primary-light: "#eef2ff"
  primary-focus: "#6366f1"
  success: "#16a34a"
  success-bg: "#dcfce7"
  success-border: "#86efac"
  danger: "#ef4444"
  danger-dark: "#dc2626"
  danger-bg: "#fef2f2"
  danger-border: "#fca5a5"
  warning: "#b45309"
  warning-bg: "#fef3c7"
  neutral-bg: "#f8fafc"
  neutral-surface: "#ffffff"
  neutral-text-main: "#0f172a"
  neutral-text-heading: "#1e293b"
  neutral-text-subtle: "#334155"
  neutral-text-muted: "#64748b"
  neutral-border: "#e2e8f0"
  neutral-border-input: "#cbd5e1"
typography:
  display:
    fontFamily: "-apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, sans-serif"
    fontSize: "3rem"
    fontWeight: 700
    lineHeight: "1.2"
    letterSpacing: "-0.02em"
  headline:
    fontFamily: "-apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, sans-serif"
    fontSize: "28px"
    fontWeight: 700
    lineHeight: "1.3"
    letterSpacing: "-0.01em"
  title:
    fontFamily: "-apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, sans-serif"
    fontSize: "18px"
    fontWeight: 600
    lineHeight: "1.4"
    letterSpacing: "normal"
  body:
    fontFamily: "-apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, sans-serif"
    fontSize: "15px"
    fontWeight: 400
    lineHeight: "1.6"
    letterSpacing: "normal"
  label:
    fontFamily: "-apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, sans-serif"
    fontSize: "13px"
    fontWeight: 600
    lineHeight: "1.4"
    letterSpacing: "0.5px"
rounded:
  sm: "6px"
  md: "8px"
  lg: "12px"
  full: "9999px"
spacing:
  xs: "4px"
  sm: "8px"
  md: "16px"
  lg: "24px"
  xl: "32px"
components:
  button-primary:
    backgroundColor: "{colors.primary}"
    textColor: "{colors.neutral-surface}"
    rounded: "{rounded.md}"
    padding: "8px 16px"
  button-primary-hover:
    backgroundColor: "{colors.primary-hover}"
  button-secondary:
    backgroundColor: "{colors.primary-light}"
    textColor: "{colors.primary}"
    rounded: "{rounded.md}"
    padding: "8px 16px"
  button-danger:
    backgroundColor: "{colors.danger-bg}"
    textColor: "{colors.danger}"
    rounded: "{rounded.md}"
    padding: "8px 16px"
  input-text:
    backgroundColor: "{colors.neutral-surface}"
    textColor: "{colors.neutral-text-main}"
    rounded: "{rounded.md}"
    padding: "10px 14px"
  card-surface:
    backgroundColor: "{colors.neutral-surface}"
    textColor: "{colors.neutral-text-main}"
    rounded: "{rounded.lg}"
    padding: "24px"
---

# Design System: Caderno de Erros

## Overview

**Creative North Star: "O Laboratório do Concurseiro"**

O Caderno de Erros é projetado como um ambiente de trabalho de alta precisão para estudantes e concurseiros em rotinas intensas de preparação. A interface busca eliminar distrações visuais e fadiga cognitiva durante sessões prolongadas de estudo, priorizando a organização hierárquica clara (Matérias → Assuntos → Erros) e a legibilidade máxima do conteúdo das questões e explicações.

A atmosfera visual combina superfícies brancas puras (`#ffffff`) elevadas sobre um plano de fundo suave em tom Slate (`#f8fafc`), demarcadas por bordas delicadas e cantos levemente arredondados (`8px`–`12px`). O tom da marca é ancorado pelo azul Índigo (`#4f46e5`), utilizado com parcimônia para sinalizar ações primárias, navegação ativa e estados focados, enquanto o código de cores de status (Verde para Revisado, Âmbar para Pendente, Vermelho para Alerta/Erro) traz diagnóstico imediato sem sobrecarregar a tela.

**Key Characteristics:**
- **Ergonomia de Estudo Prolongado:** Fundo claro de baixo contraste (`#f8fafc`) com tipografia Slate legível (`#1e293b` / `#334155`).
- **Hierarquia Visual Nítida:** Estrutura bem definida entre cabeçalhos de seções, dados de comparação e cartões de conteúdo.
- **Feedback Tátil e Previsível:** Microinterações suaves de elevação (-2px) nos cartões interativos e anéis de foco bem marcados em formulários.
- **Semântica de Status Funcional:** Badges e marcadores coloridos para distinção imediata entre matérias, assuntos e status de revisão.

## Colors

A paleta de cores é estruturada em torno de papéis semânticos estritos, priorizando a funcionalidade e o diagnóstico de estudo.

### Primary
- **Índigo Foco** (`#4f46e5` / `oklch(52.7% 0.22 265)`): Tom principal de marca, botões de ação primária e indicação de rotas ativas na navegação.
- **Índigo Hover** (`#4338ca`): Tom escurecido para estado de mouse over em botões primários.
- **Índigo Suave** (`#eef2ff`): Fundo de destaque para estados selecionados, botões de login e badges de matéria.

### Secondary (Status e Diagnóstico)
- **Verde Revisado** (`#16a34a` / fundo `#dcfce7`): Sinaliza erros concluídos e revisados com sucesso. Indicador visual de progresso.
- **Vermelho Erro / Alerta** (`#ef4444` / fundo `#fef2f2`): Destaca a resposta incorreta do usuário e ações destrutivas (exclusão).
- **Âmbar Pendente** (`#b45309` / fundo `#fef3c7`): Indica questões que ainda necessitam de revisão periódica.

### Neutral
- **Fundo da Aplicação** (`#f8fafc`): Slate ultra-claro para a tela geral, garantindo conforto visual prolongado.
- **Superfície de Cartões** (`#ffffff`): Branco puro para contêineres de formulários, cartões e barra de navegação.
- **Texto Principal** (`#0f172a` / `#1e293b`): Slate escuro com alto contraste e legibilidade ideal.
- **Texto Secundário / Muted** (`#475569` / `#64748b`): Slate médio para rótulos, datas, dicas e metadados.
- **Bordas e Divisores** (`#e2e8f0` / `#cbd5e1`): Linhas sutis de separação estrutural.

### Named Rules
**The 10% Primary Rule.** O tom Índigo primário é reservado estritamente para ações principais e pontos de navegação ativos, ocupando ≤10% da área útil da tela.

**The Status Clarity Rule.** Verde e Vermelho em blocos de comparação de respostas são usados de forma absoluta para eliminar qualquer ambiguidade entre a escolha do usuário e o gabarito.

## Typography

**Display & Body Font:** Stack nativa moderna sanitizada (`-apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, sans-serif`).

**Character:** Tipografia limpa, neutra e corporativa. Prioriza a legibilidade de longos enunciados de questões e notas pessoais sem distração tipográfica.

### Hierarchy
- **Display** (Bold 700, `3rem` / 48px, `line-height: 1.2`): Utilizado no Hero da landing page para impacto inicial.
- **Headline** (Bold 700, `28px`, `line-height: 1.3`): Título principal de telas operacionais (Ex: "Minhas Matérias", "Caderno de Erros").
- **Title** (Semibold 600, `18px`–`20px`, `line-height: 1.4`): Cabeçalhos de cartões, modais e seções de formulário.
- **Body** (Regular 400, `15px`–`16px`, `line-height: 1.6`): Texto principal para enunciados de questões, explicações de erros e observações.
- **Label** (Semibold 600, `13px`–`14px`, `letter-spacing: 0.5px`): Rótulos de campos de formulário, títulos de seções secundárias e badges em caixa alta.

### Named Rules
**The Readable Question Rule.** Textos de enunciado de questões (`.questao-texto`) utilizam `line-height: 1.6` e preservação de quebra de linha (`white-space: pre-line`) para facilitar a leitura de enunciados longos.

## Layout

O sistema adota um modelo de contêiner centralizado e responsivo, adaptando-se a diferentes tamanhos de tela.

- **Largura Máxima do Contêiner Operacional:** `900px` (focado em leitura fluida e formulários de cadastro).
- **Largura Máxima do Dashboard / Nav:** `1200px` (para visão panorâmica e barras superiores).
- **Ritmo Espacial (Spacing Grid):** Escala baseada em múltiplos de 4px/8px (`8px`, `12px`, `16px`, `24px`, `32px`).
- **Gaps e Margens de Cartão:** Separação padrão de `16px` a `24px` entre cartões e elementos de lista.
- **Breakpoints Responsivos:** Adaptação de grades e alinhamentos para telas móveis abaixo de `768px`.

## Elevation & Depth

O sistema de profundidade é predominantemente plano por padrão ("Flat with Tactile Elevate"), utilizando bordas finas para estrutura e sombras suaves apenas em interações e cartões destacados.

### Shadow Vocabulary
- **Sombreamento de Cartão Estático** (`box-shadow: 0 1px 3px rgba(0, 0, 0, 0.05)`): Elevação discreta para separar cartões do fundo Slate.
- **Sombreamento de Formulário** (`box-shadow: 0 4px 6px -1px rgba(0, 0, 0, 0.05)`): Destaca caixas de formulário de cadastro.
- **Sombreamento Hover Interativo** (`box-shadow: 0 4px 12px rgba(0, 0, 0, 0.05)`): Aplicado ao passar o mouse em cartões interativos acompanhado de `transform: translateY(-2px)`.
- **Sombreamento de Cartão de Autenticação** (`box-shadow: 0 10px 25px rgba(0, 0, 0, 0.08)`): Elevação acentuada para focar o usuário no login/registro.

### Named Rules
**The Flat-Rest Tactile-Hover Rule.** Elementos descansam planos com borda sutil (`1px solid #e2e8f0`). Apenas ao interagir (hover/focus), o elemento eleva suavemente.

## Shapes

O sistema de formas combina cantos suavizados em raio contínuo com contornos bem definidos.

- **Raio Pequeno** (`6px`): Botões de ação secundária, campos compactos, tags e pills de filtro.
- **Raio Médio** (`8px`): Botões primários e campos de entrada de texto (`input`, `select`, `textarea`).
- **Raio Grande** (`10px`–`12px`): Cartões de matérias, assuntos, erros e contêineres de formulários.
- **Raio Pílula** (`9999px` / `20px`): Badges de identificação de usuário.
- **Bordas:** `1px solid #e2e8f0` por padrão em cartões; `1px solid #cbd5e1` em inputs de formulário.

## Components

### Buttons
- **Shape:** Cantos arredondados (`8px`), sem borda por padrão em variante primária.
- **Primary (`.btn-primary`):** Fundo Índigo (`#6366f1` / `#4f46e5`), texto branco, padding `8px 16px` ou `12px`. Hover: `#4338ca`.
- **Secondary (`.btn-secondary`):** Fundo Slate claro (`#f1f5f9`), texto Slate escuro (`#334155`). Hover: `#e2e8f0`.
- **Danger (`.btn-danger`):** Fundo vermelho suave (`#fef2f2`), texto vermelho (`#ef4444`), borda `#fca5a5`.
- **Success (`.btn-success`):** Fundo verde suave (`#dcfce7`), texto verde (`#15803d`), borda `#86efac`.

### Cards / Containers
- **Corner Style:** `10px` ou `12px` radius.
- **Background:** `#ffffff`.
- **Border:** `1px solid #e2e8f0`.
- **Interatividade:** Transição de `0.2s ease` para `transform` e `box-shadow`.
- **Destaque de Status (Erro Revisado):** Borda lateral esquerda de `4px solid #22c55e` com fundo `#fafafa`.

### Inputs / Fields
- **Style:** Fundo branco (`#ffffff`), borda `#cbd5e1`, raio `8px`, padding `10px 14px`.
- **Focus State:** Borda ajustada para `#6366f1` / `#4f46e5` com anel de foco `box-shadow: 0 0 0 3px rgba(99, 102, 241, 0.15)`.

### Tags & Badges
- **Matéria Tag:** Fundo `#e0e7ff`, texto `#4338ca`, fonte `12px`, peso `600`, raio `6px`.
- **Assunto Tag:** Fundo `#f1f5f9`, texto `#475569`, fonte `12px`, peso `500`, raio `6px`.
- **Pendente Tag:** Fundo `#fef3c7`, texto `#b45309`, fonte `12px`, peso `600`, raio `6px`.
- **Revisado Tag:** Fundo `#dcfce7`, texto `#15803d`, fonte `12px`, peso `600`, raio `6px`.

## Do's and Don'ts

### Do:
- **Do** usar o contêiner de `900px` em páginas operacionais para manter os enunciados das questões dentro de uma largura de linha confortável.
- **Do** indicar claramente o estado de revisão do erro utilizando a borda lateral verde e a badge correspondente.
- **Do** utilizar anéis de foco translúcidos (`rgba(99, 102, 241, 0.15)`) em todos os campos de formulário e botões interativos para garantir acessibilidade por teclado.
- **Do** manter a comparação visual explicita entre "Minha Resposta" (vermelho) e "Resposta Correta" (verde).

### Don't:
- **Don't** utilizar fundos de tela com alto nível de saturação ou gradientes agressivos dentro do fluxo de trabalho diário do aluno.
- **Don't** remover a borda sutil de contêineres e cartões para evitar que os elementos percam definição sobre o fundo Slate.
- **Don't** aplicar a cor primária Índigo em mais de 10% da tela para manter seu poder de direcionar a atenção do usuário.
- **Don't** alterar os tamanhos de fonte padrão dos enunciados para valores abaixo de `15px`.
