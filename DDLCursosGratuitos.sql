CREATE DATABASE CursosGratuitos;

USE CursosGratuitos; 


CREATE TABLE Alunos(
	RA INT PRIMARY KEY IDENTITY,
	Usuario NVARCHAR(MAX),
	Nome NVARCHAR(MAX),
	CPF NVARCHAR(MAX),
	Email NVARCHAR(MAX),
	Senha NVARCHAR(MAX)
);
GO

CREATE TABLE Professores(
	CPF INT PRIMARY KEY IDENTITY,
	Nome NVARCHAR(MAX),
	Email NVARCHAR(MAX)
);
GO

CREATE TABLE Areas(
	Id INT PRIMARY KEY IDENTITY,
	Area NVARCHAR(MAX)
);
GO


CREATE TABLE Cursos(
	Id INT PRIMARY KEY IDENTITY,
	Nome NVARCHAR(MAX),
	CargaHoraria INT,

	AreaId INT
	FOREIGN KEY (AreaId) REFERENCES Areas(id)

);
GO

/*CREATE TABLE Disciplinas*/
CREATE TABLE AlunoCurso(
	Id INT PRIMARY KEY IDENTITY,

	AlunoRa INT
	FOREIGN KEY (AlunoRa) REFERENCES Alunos(RA),

	CursoId INT
	FOREIGN KEY (CursoId) REFERENCES Cursos(id)	
);
GO

CREATE TABLE Aulas(
	Id INT PRIMARY KEY IDENTITY,
	Titulo NVARCHAR(MAX),
	Imenta NVARCHAR(MAX),
	Duracao INT,

	CursoId INT
	FOREIGN KEY (CursoId) REFERENCES Cursos(id),

	ProfessorCpf INT
	FOREIGN KEY (ProfessorCpf) REFERENCES Professores(CPF)

);
GO

/*Renomear uma tabela. A tabela Disciplinas(antiga) foi renomeada para AlunoCurso(atual)*/
sp_rename 'Disciplinas', 'AlunoCurso';

ALTER TABLE Areas ADD Imagem NVARCHAR(MAX);
ALTER TABLE Cursos ADD Imagem NVARCHAR(MAX);