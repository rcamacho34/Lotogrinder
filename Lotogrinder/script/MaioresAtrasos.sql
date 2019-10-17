
select max(id) from tbConcurso


select * from tbCombinacao where Id = 49895
select * from tbCombinacao where Id = 234464







SELECT top 15 Id, IdUltimo11, IdUltimo12, IdUltimo13, IdUltimo14, IdUltimo15,
		Concursos11, Concursos12, Concursos13, Concursos14, Concursos15,
		(SELECT MIN(Col) FROM (VALUES (Concursos11), (Concursos12), (Concursos13), (Concursos14), (Concursos15)) AS X(Col)) AS MaiorAtraso
FROM
(
	SELECT 
		CB.Id, 
		CB.IdUltimo11, 
		CB.IdUltimo12,
		CB.IdUltimo13,
		CB.IdUltimo14,
		CB.IdUltimo15,
		(SELECT MAX(Id) FROM tbConcurso) - CB.IdUltimo11 As Concursos11,
		(SELECT MAX(Id) FROM tbConcurso) - CB.IdUltimo12 As Concursos12,
		(SELECT MAX(Id) FROM tbConcurso) - CB.IdUltimo13 As Concursos13,
		(SELECT MAX(Id) FROM tbConcurso) - CB.IdUltimo14 As Concursos14,
		(SELECT MAX(Id) FROM tbConcurso) - CB.IdUltimo15 As Concursos15
	FROM tbCombinacao CB
) A
ORDER BY MaiorAtraso DESC




