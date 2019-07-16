select * from tbCombinacao where IdUltimoConcurso15 is not null
select * from tbCombinacao where IdUltimoConcurso14 is not null
select min(IdUltimoConcurso15) from tbCombinacao where IdUltimoConcurso15 is not null
select * from tbCombinacao where IdUltimoConcurso15 is not null order by IdUltimoConcurso15 desc


select * from tbConcurso where Id = 1837

select * from tbCombinacao where Id in (1963386,
19554,
2185184,
2173413,
2862791)


SELECT top 100 Id, IdUltimoConcurso11, IdUltimoConcurso12, IdUltimoConcurso13, IdUltimoConcurso14, IdUltimoConcurso15,
		Concursos11, Concursos12, Concursos13, Concursos14, Concursos15,
		(SELECT MIN(Col) FROM (VALUES (Concursos11), (Concursos12)) AS X(Col)) AS MaiorAtraso
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
ORDER BY MaiorAtraso DESC




