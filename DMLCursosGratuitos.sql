 USE CursosGratuitos 

INSERT INTO Alunos (Usuario, Nome, CPF, Email, Senha) VALUES ('PalomaGS', 'Paloma Souza', '48723521813', 'paloma@email.com', 'linceps');
INSERT INTO Alunos (Usuario, Nome, CPF, Email, Senha) VALUES ('PriscilaES', 'Priscila Souza', '485656363', 'priscila@email.com', 'amblipidio');
INSERT INTO Alunos (Usuario, Nome, CPF, Email, Senha) VALUES ('MichelineDS', 'Micheline Souza', '756418656', 'mihdanny@email.com', 'sophie');
INSERT INTO Alunos (Usuario, Nome, CPF, Email, Senha) VALUES ('FernandoMN', 'Fernando Nakauti', '695268639', 'fmnakauti@email.com', 'omegahead');


INSERT INTO Professores (Nome, Email) VALUES ('Kakashi Hatake', 'khatake@email.com');
INSERT INTO Professores (Nome, Email) VALUES ('Iruka Umino', 'irukasensei@email.com');
INSERT INTO Professores (Nome, Email) VALUES ('Oru Maito', 'plusultra@email.com');


INSERT INTO Areas (Area) VALUES ('CorelDraw');
INSERT INTO Areas (Area) VALUES ('Insetologia');
INSERT INTO Areas (Area) VALUES ('C#');
INSERT INTO Areas (Area) VALUES ('English');


INSERT INTO Cursos (Nome, CargaHoraria, AreaId) VALUES ('Idiomas', 60, 4);
INSERT INTO Cursos (Nome, CargaHoraria, AreaId) VALUES ('Design Gráfico', 60, 1);
INSERT INTO Cursos (Nome, CargaHoraria, AreaId) VALUES ('TI', 120, 3);
INSERT INTO Cursos (Nome, CargaHoraria, AreaId) VALUES ('Biologia', 75, 2);


INSERT INTO AlunoCurso (AlunoRa, CursoId) VALUES (1, 3) , (2, 4) , (3, 1), (4, 2);


INSERT INTO Aulas (Titulo, Imenta, Duracao, CursoId, ProfessorCpf) VALUES ('Linguagem de Programacao C# completa', 'C# do basico ao avançado', 120, 3, 1),
																		  ('Introdução ao CorelDraw', 'Como aprender a usar CorelDraw', 60, 2, 3),
																		  ('Insetologia - Besouros', 'Como identificar insetos', 60, 4, 2),
																		  ('Learning English', 'Aprender ingles do zero', 120, 1, 3);



 UPDATE Professores SET Email = 'kakashisensei@email.com' WHERE CPF = 1;

 /* DELETE FROM Aulas WHERE Id = 4; */