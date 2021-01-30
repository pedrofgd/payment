# Contexto de pagamento com ASP.NET Core

Projeto desenvolvido durante um [curso de modelagem de domínios ricos](https://balta.io/cursos/modelando-dominios-ricos) do Balta.io. Nesse curso aprendi sobre:

* Domínios Ricos e Anêmicos
* Corrupções no código
* Recursos de POO
  * `interface`
* Value Objects
* Domain Driven Design
* Fail Fast Validations
* Repository Patern
* CQRS
* Testes unitários

Esse projeto não está conectado a um banco de dados e não faz requisições Http ao servidor já que não é esse o intuito do curso. Os domínios, commands, queries e handlers simulam o funcionamento de uma API real de pagamentos. Há, inclusive, recursos *fake* para a criação de entidades estáticas.

Em possíveis atualizações, poderia ser adicionado:
- [ ] Conexão com um banco de dados
- [ ] Autenticação dos estudantes
- [ ] Configuração do serviço de email (boas vindas e recuperação de senha)
- [ ] Conexão com as APIs de pagamento (PayPal...)


## Funcionalidades
Domínios
- [x] Students
- [x] Subscription (plano de assinatura)
- [x] Payment (opção por boleto, cartão de crédito e PayPal)

Para um `student` ser cadastrado ele precisa adicionar um `payment`, assim a `subscription` já é criada.

## Pacotes e tecnologias utilizadas

- .NET v5.0.102+
- [Flunt validations](https://github.com/andrebaltieri/flunt)
- MSTest



# 🌊 de conhecimento

### Domínios Ricos x Domínios Anêmicos

Um domínio anêmico é nada mais do que a representação de uma entidade no banco de dados - não expressivo e sem regras de negócio. Isso pode funcionar bem quando um código não tem muita complexidade.

Mas, como nesse caso, quando há muitas regras de negócio a serem implementadas, fica mais difícil manter o código organizado e funcional com modelo Data Driven, com domínios anêmicos.

Para isso existe o Domain Driven Design (projeto orientado a domínio), que incorpora no domínio o comportamento daquela entidade, utilizando recursos de programação orientada a objetos para controlar o acesso de outras partes do código à informações das entidades.

Uma maneira de fazer isso é aplicar `private setters` e construtores privados, assim as alterações deverão passar por um método dentro da classe, que cuidará das validações.

Outra coisa que particularmente me chamou atenção é aplicar esse controle ao usar o tipo `List` que, naturalmente, tem métodos públicos para manipular o conteúdo, que permite *corrupções no código*. Para combater isso, `List` foi substituido por `IReadOnlyCollection`, que tem um método específico para manipulá-lo. (Isso pode ser visto em `/PaymentContext.Domain/Entities/Studens.cs`, por exemplo)

É claro que a abordagem de desenvolvimento depende de aspectos individuais de cada projeto, por isso é importante para o desenvolvedor estar abituado com as ferramentas didponíveis para, independentemente do *design*, manter o código **o menor possível, funcional, de fácil manutenção e de fácil entendimento**.


### Value Objects e primitive obsession

Primitive obsession quer dizer obsessão por tipos primitivos. Ao invés de usar o tipo `string` para armazenar um *email* e ter que fazer uma validação sempre que for ser usado, podemos criar um "tipo" `Email` para economizar código e tempo de manutenção no futuro.

Para isso servem os `value objects`, criar tipos de dados característicos do código. Nesse projeto foi criado um tipo para **nome**, que tem primeiro e segundo nome, **email**, **endereço**, com todas as informações necessárias (rua, bairro, zipcode...) e **documento**.


### Command Query Responsability Segregation - CQRS

Esse é um padrão que diz que cada método deve ter seus próprio *commands* e *queries*.

`Commands` são para **escrita**. Quando um usuário novo quer se cadastrar em algo, por exemplo, ele deve preencher algum tipo de formulário com seus dados. Mas não são todos os campos de uma entidade que ele deve ter acesso. É como em uma prova do colégio: por mais que tivesse um campo escrito "Nota" no cabeçalho, o aluno não deveria preenche-lo. Só o professor depois que corrigisse a prova. **Commands** são mais ou menos isso. É uma "interface" com os dados necessários para cadastrar/alterar/escrever algo.

`Queries` são as requisições feitas no banco de dados. Uma query traz informações, portando são pra **leitura**. Alguns exemplos são: buscar todos os usuários cadastrados, buscar um usuário pelo nome etc.

De modo geral, então, são objetos de transporte.

A implementação do CQRS aumenta a disponibilidade e escalabilidade da aplicação. Dentre todas as maneiras que utilizar esse padrão beneficia o código, um exemplo é a `Fail Fast Validation`.

Ter um `Command`/`Query` para cada situação possível de leitura/escrita permite criar validações durante as chamadas ao banco de dados. Isto é, o usuário preenche um formulário de cadastro. Ao invés de já tentar cadastrá-lo, é feita uma verificação das informações e tentar encontrar qualquer erro o mais rápido possível - para economizar requisições - e, se tudo estiver correto, só então o usuário será cadastrado.


### Testes unitários

Os testes nesse projeto foram feitos com o pacote `MSTest`. Testes são simulações do comportamento da aplicação. Para cada possibilidade pode ser escrito um teste. Por exemplo, deve retornar um erro caso um usuário tente se cadastrar com um CPF inválido, ou deve retornar um erro caso um usuário tente comprar um produto sem realizar um pagamento.

Esses testes, então, garantirão o funcionamento da aplicação.


## Referências

* [Curso: Modelando de domínios ricos](https://balta.io/cursos/modelando-dominios-ricos)
* [Artigo: Guia para modelagem de domínios ricos](https://arleypadua.medium.com/guia-para-modelar-dom%C3%ADnios-ricos-15887b516c1b)
* [Artigo: CQRS – O que é? Onde aplicar?](https://www.eduardopires.net.br/2016/07/cqrs-o-que-e-onde-aplicar/)