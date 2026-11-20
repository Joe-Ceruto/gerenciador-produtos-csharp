Gerenciador de Produtos de Mercado

Aplicação de terminal em C# para gerenciar o cadastro de produtos de um mercado. Permite cadastrar, listar, editar e remover produtos, e salva tudo em um arquivo JSON, para que os dados continuem ali depois de fechar o programa.

Projeto de estudo, feito para praticar os fundamentos de C# e voltar a programar com uma base sólida.

Funcionalidades

Cadastrar produtos com nome, quantidade e preço. O código é gerado automaticamente.

Listar todos os produtos cadastrados, com preço formatado em moeda.

Editar um produto pelo código. Apenas os campos preenchidos são alterados, e deixar um campo em branco mantém o valor atual.

Remover um produto pelo código, com confirmação antes de apagar.

Persistência em JSON: os dados são carregados ao abrir o programa e salvos a cada alteração.

Validação de entradas: campos vazios, textos no lugar de números, valores negativos e códigos inexistentes são tratados com mensagens de erro, sem derrubar o programa.

Tecnologias

C#

.NET (11 ou superior)

`System.Text.Json` para serialização e desserialização

Como executar

Pré-requisito: ter o SDK do .NET instalado.

```bash
git clone <url-do-repositorio>
cd <pasta-do-projeto>
dotnet run
```

Como usar

Ao iniciar, o programa mostra o menu principal:

```
1- Create a product.
2- Consult the products.
3- Edit a product.
4- Delete a product.
5- Exit.
```

Opção	O que faz

1	Pede nome, quantidade e preço e cadastra o produto.

2	Lista todos os produtos com código, nome, quantidade e preço.

3	Pede o código do produto e os novos valores. Enter em branco mantém o valor atual.

4	Pede o código do produto e confirma com `Y` ou `N` antes de remover.

5	Encerra o programa.

No preço, tanto o ponto quanto a vírgula funcionam como separador decimal (`10.50` ou `10,50`).

Persistência dos dados

Os produtos ficam no arquivo `savedProducts.json`, criado automaticamente no diretório de execução na primeira vez que um produto é salvo. Exemplo do formato:

```json
\[
  {
    "code": 1,
    "name": "Arroz 5kg",
    "amount": 20,
    "price": 28.9
  }
]
```

Os códigos começam em 1. O próximo código é sempre maior que o maior código existente, então não se repetem, mesmo depois de remover produtos e reabrir o programa.

Se o arquivo não existir ou estiver vazio, o programa começa com a lista vazia.

Estrutura do projeto

```
├── Program.cs   # Menu, operações de CRUD, validações e leitura/gravação do JSON
└── Product.cs   # Classe Produto (código, nome, quantidade e preço)
```

Conceitos praticados

Programação orientada a objetos (classe `Produto`)

Listas genéricas (`List<T>`) e busca com expressões lambda

CRUD completo em aplicação de console

Validação de entrada com `TryParse` e tratamento de valores nulos (`string?`)

Serialização e desserialização de JSON

Separação de responsabilidades em métodos (validar, carregar, salvar)

Próximos passos

Padronizar todas as mensagens do programa em um único idioma

Tratar arquivo JSON corrompido sem encerrar o programa

Trocar `double` por `decimal` no preço

Adicionar busca de produtos por nome

Criar testes automatizados

Autor

JoeCeruto — GitHub

