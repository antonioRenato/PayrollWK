# 📊 Processador de Folha de Pagamento

Um aplicativo console desenvolvido em C# / .Net que processa arquivos CSV de folha de pagamento, realizando cálculos de imposto de renda e INSS para empregados.

## 📝 Descrição

Este aplicativo realiza a leitura de um arquivo CSV contendo registros de empregados e suas respectivas rubricas, calculando:

- ✅ Base de cálculo INSS
- ✅ Base de cálculo IR
- ✅ Desconto de dependentes
- ✅ Desconto INSS
- ✅ Desconto IR
- ✅ Valor líquido

O resultado é exibido em ordem crescente de CPF dos empregados e também salvo em um arquivo txt como um relatório.

## 📋 Formato do Arquivo

O aplicativo processa arquivos CSV com campos separados por ponto e vírgula (`;`), contendo dois tipos de registro:

### Registro de Empregado (EMP)

| Campo                   | Tipo  | Tamanho | Decimais | Valor              |
|-------------------------|-------|---------|----------|-------------------|
| Identificador do registro | Fixo  | 3       | -        | EMP               |
| CPF do empregado        | N     | 11      | -        |                   |
| Nome do empregado       | A     | 1-40    | -        |                   |
| Qtd Dependentes         | N     | 2       | 0        |                   |

### Registro de Rubrica (RUB)

| Campo                   | Tipo  | Tamanho | Decimais | Valor              |
|-------------------------|-------|---------|----------|-------------------|
| Identificação do registro | Fixo  | 3       | -        | RUB               |
| Código de Rubrica       | N     | 4       | 0        |                   |
| Descrição da rubrica    | A     | 1-40    | -        |                   |
| Tipo                    | A     | 1       | -        | P - Proventos / D - Descontos |
| Valor                   | N     | 1-14    | 2        |                   |

## 🧩 Exemplo de Arquivo

```
EMP;12345678900;João Silva;02
RUB;1001;Salário Base;P;5000.00
RUB;1002;Plano de Saúde;D;200.00
RUB;1003;Vale Transporte;D;300.00
EMP;98765432100;Maria Oliveira;01
RUB;1001;Salário Base;P;3500.00
RUB;1002;Plano de Saúde;D;150.00
```

## 🚀 Como Usar

### Modo Interativo

Ao executar o aplicativo, ele solicitará o caminho do arquivo:

```
Digite o caminho do arquivo .csv:
```

Digite o caminho completo incluindo o nome do arquivo, por exemplo:
```
C:\folha\funcionarios.csv
```

## 📊 Exemplo de Saída

### Processamento bem-sucedido

```
CPF: 32165498700
Nome: Ana Costa
Base INSS: R$ 6.750,00
Base IR: R$ 5.919,50
Desconto Dependentes: R$ 75,90
Desconto INSS: R$ 754,60
Desconto IR: R$ 295,98
Valor Líquido: R$ 5.699,43
-----------------------------
CPF: 45678912300
Nome: Carlos Pereira
Base INSS: R$ 5.580,00
Base IR: R$ 4.761,50
Desconto Dependentes: R$ 227,70
Desconto INSS: R$ 590,80
Desconto IR: R$ 238,08
Valor Líquido: R$ 4.751,13
-----------------------------

Relatório salvo com sucesso em: C:\folha\Relatorios-Folha\relatorio_20250410_153045.txt
```

### Erros de Validação

O sistema reportará erros de validação antes de exibir os resultados dos registros válidos:

```
Employee 1 - Erros encontrados:
  • CPF must be 11 digits long
  • Dependents count must be entered with at least 2 digits

Employee 3
Heading 4 - Erros encontrados:
  • Description is required

Employee 4
Heading 4 - Erros encontrados:
  • The type must be 'P' or 'D'

Employee 5 - Erros encontrados:
  • The name is required!

[Resultados dos registros válidos...]

Relatório salvo com sucesso em: C:\folha\Relatorios-Folha\relatorio_20250410_153045.txt
```

## 📄 Geração de Relatório

Além de exibir os resultados no console, o aplicativo também:

1. Cria uma pasta chamada `Relatorios-Folha` no mesmo diretório do arquivo CSV processado
2. Gera um arquivo de texto contendo todos os registros processados com sucesso
3. Exibe o caminho completo do relatório salvo ao final do processamento

## ✅ Regras de Validação

O aplicativo implementa as seguintes validações:

### Empregado
- CPF deve ter 11 dígitos
- Nome deve ter menos de 40 caracteres e não pode ser vazio
- Quantidade de dependentes deve ser um número de dois dígitos
### Rúbrica
- Código da rubrica deve ter pelo menos 4 dígitos
- Descrição deve ter menos de 40 caracteres e não pode ser vazia
- Tipo de rubrica deve ser 'P' ou 'D'
- Valor precisa ser maior que zero e ter no mínimo 14 dígitos (Contando os decimais)

## 🧮 Regras de Cálculo

### Desconto INSS

O cálculo do INSS segue as faixas progressivas definidas pela legislação:

| Faixa Salarial | Alíquota |
|----------------|----------|
| Até R$ 1.518,00 | 7,5%     |
| De R$ 1.518,01 até R$ 2.793,88 | 9%      |
| De R$ 2.793,89 até R$ 4.190,83 | 12%     |
| Acima de R$ 4.190,84 | 14%     |

### Desconto de Dependentes

Dedução de 5% em cima do salário mínimo (R$ 1.518,00) por dependente para cálculo do IR.

### Imposto de Renda

O IR é calculado após deduções de INSS e dependentes:

| Base de Cálculo IR | Alíquota |
|--------------------|----------|
| Até R$ 3.036,00 | Isento   |
| De R$ 3.036,01 até R$ 6.072,00 | 5%       |
| De R$ 6.072,01 até R$ 7.590,00 | 10%      |
| De R$ 7.590,01 até R$ 10.626,00 | 15%      |
| Acima de R$ 10.626,01 | 20%      |

## 🛠️ Desenvolvimento

Esta aplicação foi desenvolvida utilizando princípios de:

- ⚙️ Programação Orientada a Objetos
- 🏗️ Arquitetura DDD (Domain-Driven Design)
- 💉 Injeção de Dependência
- 🔧 Princípios SOLID
- 🧪 Testes Unitários
- 📂 Estruturação e organização de código
- 🐛 Tratamento de erros e validações usando FluentValidation
- 📊 Processamento e cálculos financeiros

A aplicação segue uma estrutura clara de camadas, separando responsabilidades e facilitando a manutenção e evolução do código.

Desenvolvido como parte de um projeto de avaliação técnica.
