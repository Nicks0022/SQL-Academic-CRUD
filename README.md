# Sistema de Gerenciamento (C# / SQL)

Projeto desenvolvido para a disciplina de Programação Orientada a Objetos (POO) com foco em arquitetura em camadas e persistência de dados. O sistema gerencia o cadastro de alunos, professores, disciplinas e produtos, utilizando uma estrutura robusta dividida em Model, Data e Front-End.

---

## 🏗️ Arquitetura do Projeto (3-Tier)

O software foi estruturado seguindo o padrão de três camadas, garantindo a separação de responsabilidades e facilitando a manutenção do código: 

*   **Model**: Contém as classes que representam as entidades de negócio como `Aluno`, `Professor`, `Disciplina` e `Produto`.
*   **Data (Repository)**: Camada responsável por toda a comunicação com o SQL Server, utilizando ADO.NET para execução de comandos CRUD.
*   **FrontDesktop**: Interface gráfica desenvolvida em Windows Forms (WinForms) para interação com o usuário final.

---

## 🛠️ Tecnologias e Configurações

*   **Linguagem**: C# (.NET Framework).
*   **Banco de Dados**: SQL Server.
*   **Controle de Versão**: Git & GitHub.

---

## 📊 Estrutura de Dados

O sistema realiza o gerenciamento completo de:
*   **Alunos**: Cadastro de nome, e-mail e data de nascimento.
*   **Professores**: Registro de dados e titulação acadêmica.
*   **Disciplinas**: Gerenciamento de códigos e carga horária.
*   **Produtos**: Controle de estoque e preços para itens técnicos.
