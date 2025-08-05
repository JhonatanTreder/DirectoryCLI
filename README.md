# CLI desenvolvida em C#
Projeto de uma CLI (Command Line Interface) sendo desenvolvida com a linguagem C#.

## Descrição
Este repositório contém o código fonte do projeto, bem como as suas funcionalidades e qual é o seu objetivo.
O projeto está sendo desenvolvido puramente com .NET 8.0 e a maiorida das coisas encontradas aqui oferecem suporte tanto para Windows quanto para Linux e MacOS.

## Objetivo
O objetivo desse projeto é ajudar desenvolvedores (e até mesmo usuários comuns) que precisam lidar com a manipulação de arquivos, diretórios e realizar alguns diagnósticos no Sistema Operacional. Pode servir também de cunho educacional por pessoas que desejam explicar para outras que não entendem do assunto sobre do que uma CLI se trata.

## Funcionalidades
Esse projeto de código aberto possui diversas funcionalidades básicas, como a manipulação de arquivos/diretórios em larga escala, realizar diagnósticos do Sistema Operacional, abrir sites, utilizar comandos de outras CLIs (como o docker, git, do próprio SO, etc...). Para mais detalhes, acesse: [Lista de Comandos](docs/Commands.md).

# Instrução de Instalação
**ATENÇÃO:** <br>
Esta CLI depende de algumas bibliotecas externas (como a Colorful.Console) para funcionar corretamente.
É essencial que essas dependências estejam no mesmo diretório do executável (.exe) para garantir o funcionamento correto da aplicação.

## Pré-requisitos Comuns

**Ferramentas Necessárias:** <br>

[**.NET 8.0 SDK**](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0) <br>

**-IDE/Editor de Código Compatível** <br>

#### Exemplos: <br>
[Visual Studio (IDE)](https://visualstudio.microsoft.com/pt-br/vs/community/) <br>
[Visual Studio Code (editor de código)](https://code.visualstudio.com/download) <br>

## Dependências Externas
A principal dependência utilizada no projeto é: **Colorful.Console**. <br>
Essa biblioteca deve ser incluída na build final da CLI. Certifique-se de publicar o executável com as dependências incluídas.

**Passo a Passo de Instalação** <br>

**1. Clonando o Projeto** <br>
Use o terminal para clonar o repositório no seu ambiente local:

```bash
git clone https://github.com/SEU_USUARIO/SEU_REPOSITORIO
```

Acesse a pasta clonada:
```bash
cd SEU_REPOSITORIO
```

OBS: Se você pretende contribuir com o projeto, recomendo que faça um fork antes de clonar. <br>

**2. Instalando a Biblioteca Colorful.Console** <br>

No terminal/prompt de comando, execute:

```bash
dotnet add package Colorful.Console
```

Isso adicionará a dependência ao projeto e ela será incluída automaticamente durante a build.

**3. Gerando o Executável com as Dependências** <br>

Para compilar o projeto e gerar o executável com todas as bibliotecas necessárias, execute:

```bash
dotnet publish -c Release -r win-x64 --self-contained false -o ./build
```
O comando acima compila em modo Release, gera build para Windows 64 bits (win-x64), define saída para a pasta ./build e não gera runtime junto (mas inclui as bibliotecas necessárias). <br>

Se quiser uma build independente do sistema, você pode adaptar a runtime daseguinte forma: <br>

win-x64 → para Windows <br>
linux-x64 → para Linux <br>
osx-x64 → para Mac <br>

**4. Executando a CLI** <br>
Após o publish, acesse a pasta de saída:

```bash
cd build
```
E execute a CLI com: <br>

```bash
./Executavel_da_CLI.exe
```

**IMPORTANTE:** garanta que o .exe e a DLL Colorful.Console.dll estejam no mesmo diretório. <br>

**5. Verificando que a CLI está funcionando** <br>

Você pode testar com: <br>

```bash
cmds
```
Escreva o comando acima (`cmds`) para testar. Se tudo estiver certo, será exibida a lista de comandos disponíveis.

**Observação Final** <br>
Se for compartilhar apenas o executável com outros usuários, envie também todas as DLLs da pasta build junto com ele. Apenas o .exe sozinho não vai funcionar.
