
SELECT * FROM Alunos;
GO

SELECT * FROM Professores ORDER BY Nome ASC;
GO

SELECT * FROM Cursos Where id > 1 AND Id < 3;
GO

SELECT * FROM Aulas Where Titulo = 'Insetologia - Besouros';
GO

SELECT * FROM Aulas
	INNER JOIN Cursos ON Cursos.Id = Aulas.CursoId;
GO