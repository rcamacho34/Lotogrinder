select * from tbCombinacao where Id = 207651

select * from tbCombinacaoAtraso where IdAtraso01 is not null

select * from tbConcurso where Id = 1839

update tbCombinacaoAtraso set IdAtraso01 = null, IdAtraso02 = null, IdAtraso03 = null, IdAtraso04 = null, IdAtraso05 = null where IdAtraso01 is not null

update tbCombinacaoAtraso
set IdAtrasoAtual = B.MaiorAtraso
from tbCombinacaoAtraso CA
join 
(

SELECT Id, IdUltimoConcurso11, IdUltimoConcurso12, IdUltimoConcurso13, IdUltimoConcurso14, IdUltimoConcurso15,
		Concursos11, Concursos12, Concursos13, Concursos14, Concursos15,
		(SELECT MIN(Col) FROM (VALUES (Concursos11), (Concursos12), (Concursos13), (Concursos14), (Concursos15)) AS X(Col)) AS MaiorAtraso
FROM
(
	SELECT 
		CB.Id, 
		CB.IdUltimoConcurso11, 
		CB.IdUltimoConcurso12,
		CB.IdUltimoConcurso13,
		CB.IdUltimoConcurso14,
		CB.IdUltimoConcurso15,
		(SELECT MAX(Id) FROM tbConcurso) - CB.IdUltimoConcurso11 As Concursos11,
		(SELECT MAX(Id) FROM tbConcurso) - CB.IdUltimoConcurso12 As Concursos12,
		(SELECT MAX(Id) FROM tbConcurso) - CB.IdUltimoConcurso13 As Concursos13,
		(SELECT MAX(Id) FROM tbConcurso) - CB.IdUltimoConcurso14 As Concursos14,
		(SELECT MAX(Id) FROM tbConcurso) - CB.IdUltimoConcurso15 As Concursos15
	FROM tbCombinacao CB
) A

) B on B.Id = CA.Id



select * From tbCombinacao where Id = 246


SELECT CA.Id, C.IdUltimoConcurso11, C.IdUltimoConcurso12, C.IdUltimoConcurso13, C.IdUltimoConcurso14, C.IdUltimoConcurso15, IdAtraso01, IdAtraso02, IdAtraso03, IdAtraso04, IdAtraso05, C.MaiorAtraso
FROM tbCombinacaoAtraso CA
JOIN 
(
	SELECT Id, IdUltimoConcurso11, IdUltimoConcurso12, IdUltimoConcurso13, IdUltimoConcurso14, IdUltimoConcurso15,
			Concursos11, Concursos12, Concursos13, Concursos14, Concursos15,
			(SELECT MIN(Col) FROM (VALUES (Concursos11), (Concursos12), (Concursos13), (Concursos14), (Concursos15)) AS X(Col)) AS MaiorAtraso
	FROM
	(
		SELECT 
			CB.Id, 
			CB.IdUltimoConcurso11, 
			CB.IdUltimoConcurso12,
			CB.IdUltimoConcurso13,
			CB.IdUltimoConcurso14,
			CB.IdUltimoConcurso15,
			(SELECT MAX(Id) FROM tbConcurso) - CB.IdUltimoConcurso11 As Concursos11,
			(SELECT MAX(Id) FROM tbConcurso) - CB.IdUltimoConcurso12 As Concursos12,
			(SELECT MAX(Id) FROM tbConcurso) - CB.IdUltimoConcurso13 As Concursos13,
			(SELECT MAX(Id) FROM tbConcurso) - CB.IdUltimoConcurso14 As Concursos14,
			(SELECT MAX(Id) FROM tbConcurso) - CB.IdUltimoConcurso15 As Concursos15
		FROM tbCombinacao CB
	) A
) C 
ON C.Id = CA.Id
WHERE CA.IdAtraso01 IS NOT NULL
AND CA.IdAtraso01 - CA.IdAtraso05 < 10
AND C.MaiorAtraso > CA.IdAtraso05
ORDER BY MaiorAtraso

select count(*) from tbCombinacao where IdUltimoConcurso11 = 1837
