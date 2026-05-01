CREATE DATABASE SALATIEL;
USE SALATIEL;

CREATE TABLE dbo.Aluno (
    AlunoId           INT IDENTITY(1,1) PRIMARY KEY,
    Nome              NVARCHAR(120) NOT NULL,
    Email             NVARCHAR(120) NULL UNIQUE,
    DataNascimento    DATE NULL,
    CreatedAt         DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME()
);

CREATE TABLE dbo.Professor (
    ProfessorId       INT IDENTITY(1,1) PRIMARY KEY,
    Nome              NVARCHAR(120) NOT NULL,
    Email             NVARCHAR(120) NULL UNIQUE,
    Titulo            NVARCHAR(80) NULL,
    CreatedAt         DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME()
);

CREATE TABLE dbo.Disciplina (
    DisciplinaId      INT IDENTITY(1,1) PRIMARY KEY,
    Codigo            VARCHAR(20) NOT NULL UNIQUE,
    Nome              NVARCHAR(120) NOT NULL,
    CargaHoraria      INT NOT NULL CHECK (CargaHoraria > 0),
    CreatedAt         DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME()
);

CREATE TABLE dbo.Produto (
    ProdutoId         INT IDENTITY(1,1) PRIMARY KEY,
    Nome              NVARCHAR(120) NOT NULL,
    Descricao         NVARCHAR(250) NULL,
    Preco             DECIMAL(10, 2) NOT NULL CHECK (Preco >= 0),
    QuantidadeEstoque INT NOT NULL DEFAULT 0,
    CreatedAt         DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME()
);

SELECT * FROM dbo.Produto;
SELECT * FROM dbo.Disciplina;
SELECT * FROM dbo.Aluno;
SELECT * FROM dbo.Professor;