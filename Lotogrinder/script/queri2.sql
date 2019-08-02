select * from tbCombinacao where IdUltimo15 is not null
select * from tbCombinacao where IdUltimo14 is not null
select min(IdUltimo15) from tbCombinacao where IdUltimo15 is not null
select * from tbCombinacao where IdUltimo15 is not null order by IdUltimo15 desc


select * from tbConcurso where Id = 1837

select * from tbCombinacao where Id = 19554
select * from tbCombinacao where Id = 2252722




select * from tbCombinacao where Id = 132144



select * from tbCombinacao where Id = 260055



select * from tbCombinacao where Id in (1963386,
19554,
2185184,
2173413,
2862791)


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




