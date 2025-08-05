# Comandos Utilizados
Tabela referente aos comandos que podem ser utilzados com explicação ao lado.

| Comando         | Descrição |
|-----------------|-----------|
| `cmds`          | Mostra a lista de todos os comandos. |
| `zip`           | Zipa um arquivo ou pasta para um arquivo `.zip` de destino. |
| `move`          | Move um arquivo ou pasta para um diretório de destino. |
| `list`          | Lista o conteúdo de um diretório/caminho específico **[parâmetro opcional: `-e`]**. |
| `scan`          | Mostra o tamanho total de um caminho específico. |
| `open`          | Abre um arquivo ou pasta de um diretório específico **[parâmetro opcional: `--this`]**. |
| `exit`          | Fecha o programa. |
| `clear`         | Limpa o terminal. |
| `rename`        | Renomeia um arquivo ou pasta **(podendo alterar a extensão do arquivo)**. |
| `search`        | Busca por algo em um arquivo `.cs` para manipulá-lo **(DataAnnotations, palavras reservadas, etc...)** |
| `extract`       | Extrai o conteúdo de um arquivo zipado para um diretório específico. |
| `open-site`     | Abre um site através do nome base DNS (ex: `"github"`). |
| `create-file`   | Cria um ou mais arquivos em um diretório específico. |
| `cmds-sintaxe`  | Mostra a sintaxe de uso de todos os comandos. |
| `delete-files`  | Deleta um ou mais arquivos de um diretório específico. |
| `create-folder` | Cria uma ou mais pastas em um diretório específico. |
| `delete-folder` | Deleta uma ou mais pastas em um diretório específico. |
| `del-files`     | Deleta todos os arquivos de um diretório específico **(pode filtrar por extensão)**. |
| `del-folders`   | Deleta todas as pastas de um diretório específico. |
| `system-info`   | Fornece informações sobre o computador **(armazenamento, processador, placa de vídeo, etc...)** |

# Sintaxe de de Uso
Tabela referente a sintaxe de cada comando.

| Comando         | Sintaxe |
|-----------------|-----------|
| `cmds`          | `<cmds>`. |
| `zip`           | `[diretório]` `<zip>` `[arquivo/pasta]` `<to>` `[diretório de destino]`.|
| `move`          | `[diretório]` `<move>` `[arquivo/pasta]` `<to>` `[diretório de destino]`. |
| `list`          | `[diretório]` `<list>` `[parâmetro opcional: <-e> (tipo de extensão)]`. |
| `scan`          | `[diretório]` `<scan>`. |
| `open`          | `[diretório]` `<open>` `[arquivo/pasta]` ou `[--this]`. |
| `exit`          | `<exit>` |
| `clear`         | `<clear>` |
| `rename`        | `[diretório]` `<rename>` `[arquivo/pasta]` `<to>` `[nome final]`. |
| `search`        | `[diretório]` `<search>` `[DataAnnotation]` `<parâmetros e valores...>`.|
| `extract`       | `[diretório]` `<extract>` `[arquivo .zip]` `<to>` `[diretório de destino]`. |
| `open-site`     | `<open-site>` `[nome DNS, ex: "github"]`. |
| `create-file`   | `[diretório]` `<create-file>` `[arquivo] (pode especificar mais de um aqruivo)`. |
| `cmds-sintaxe`  | `<cmds-sintaxe>`. |
| `delete-files`  | `[diretório]` `<delete-files>` `[arquivo] (pode especificar mais de um arquivo)`.|
| `create-folder` | `[diretório]` `<create-folder>` `[pasta] (pode epecificar mais de uma pasta)`. |
| `delete-folder` | `[diretório]` `<delete-folder>` `[pasta] (pode epecificar mais de uma pasta)`. |
| `del-files`     | `[diretório]` `<del-files> (parâmetro de busca opcional: <-e> [extensão do arquivo])`. |
| `del-folders`   | `[diretório]` `<del-folders>`. |
| `system-info`   | `<system-info>`.|

# Exemplos de uso
Tabela referente a exemplos de como pode se utilizar os comandos.

| Comando         | Exemplo |
|-----------------|-----------|
| `cmds`          | cmds |
| `zip`           | D:\Test zip Pasta1 arquivo1.txt to arquivo_example.zip |
| `move`          | D:\Test move arquivo1.txt to Pasta1 |
| `list`          | D:\Test list |
| `scan`          | D:\Test scan |
| `open`          | D:\Test open Pasta1 `ou` D:\Test open --this |
| `exit`          | exit |
| `clear`         | clear |
| `rename`        | D:\Test rename Pasta1 to PastaRenomeada |
| `search`        | D:\Test search Authorize list-props |
| `extract`       | D:\Test extract arquivo_example.zip to Pasta1 |
| `open-site`     | open-site github |
| `create-file`   | D:\Test create-file arquivo1.txt arquivo2.txt arquivo3.html |
| `cmds-sintaxe`  | cmds-sintaxe |
| `delete-files`  | D:\Test delete-files arquivo1.txt arquivo2.txt |
| `create-folder` | D:\Test create-folder Pasta2 |
| `delete-folder` | D:\Test delete-folder Pasta2 |
| `del-files`     | D:\Test del-files `ou` D:\Test del-files .txt |
| `del-folders`   | D:\Test del-folders |
| `system-info`   | system-info |

# Parâmetros de Comandos
Tabela para os comandos que utilizam parâmetros (opcionais ou não).

| Comando         | Parâmetros |
|-----------------|-----------|
| `move`          | `[to]`, `[to-new]`  |
| `list`          | `[-e]` |
| `open`          | `[--this]`, `[<-f> valor]`, `[<-d> valor]` OBS: "-f" e "-d" especifica 'arquivo' ou 'diretório', e "valor" especifica a posição deles (use o comando 'list' no diretório para melhor entendimento). |
| `search`        | `[list-props]`, `[remove-all-props]`, `[remove-prop]`, `[add-prop]`, `[replace-prop]` |
| `extract`       | `[to]`, `[to-new]`, `[to-here]`|
| `del-files`     | `[<tipo de extensão>, ex: "D:\Test del-files .txt"]` |
